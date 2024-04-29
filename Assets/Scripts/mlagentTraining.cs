using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.Tilemaps;


public class mlagentTraining : Agent
{

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private paint paintScript;  // Reference to the paint script
    private int previousColorCount = 0;  // Store the count of tiles from the previous step


    public override void Initialize()
    {
        // Find the paint script on the same GameObject or another one
        paintScript = GetComponent<paint>();
        if (paintScript == null)
        {
            Debug.LogError("Paint script not found on the GameObject");
        }
    }


    public override void OnEpisodeBegin()
    {
        // Reset the agent's position or other states as needed
        transform.localPosition = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0);

        // Reset the tilemap and counts
        TilemapManager tilemapManager = FindObjectOfType<TilemapManager>();
        if (tilemapManager != null)
        {
            tilemapManager.ResetTilemap();
        }

        sumTiles counter = FindObjectOfType<sumTiles>();
        if (counter != null)
        {
            counter.ResetCounts();
        }
        previousColorCount = GetCurrentColorCount();  // Initialize at the start of an episode

    }


    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3Int agentGridPosition = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), 0);
        int gridRadius = 2; // For a 5x5 grid
        long binaryRepresentation = 0;
        int bitPosition = 0;
        var agentColor = paintScript.PaintColor;

        for (int dy = -gridRadius; dy <= gridRadius; dy++)
        {
            for (int dx = -gridRadius; dx <= gridRadius; dx++)
            {
                Vector3Int tilePos = agentGridPosition + new Vector3Int(dx, dy, 0);
                if (tilemap.HasTile(tilePos)) // Check if there is a tile at this position
                {
                    Color tileColor = tilemap.GetColor(tilePos);
                    bool isAgentColor = tileColor.Equals(agentColor); // Check if the tile color matches the agent's color
                    if (isAgentColor)
                    {
                        binaryRepresentation |= (1L << bitPosition); // Set the bit at the bitPosition if the tile is the agent's color
                    }
                }
                bitPosition++;
            }
        }

        // Observing the agent's position (normalized)
        BoundsInt bounds = tilemap.cellBounds;
        sensor.AddObservation((agentGridPosition.x - bounds.xMin) / (float)bounds.size.x);
        sensor.AddObservation((agentGridPosition.y - bounds.yMin) / (float)bounds.size.y);

        // Adding the single long binary number as one observation
        sensor.AddObservation(binaryRepresentation);
        
    }




    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        float movementSpeed = 5.0f;
        Vector2 newPosition = new Vector2(moveX * movementSpeed * Time.deltaTime, moveY * movementSpeed * Time.deltaTime);
        transform.Translate(newPosition);

        // Calculate and apply continuous reward based on the current state
        CalculateReward();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {

        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");

    }

    public void FinishEpisode()
    {
        base.EndEpisode(); // End the episode
    }

    private void CalculateReward()
    {
        int currentColorCount = GetCurrentColorCount();
        int colorChange = currentColorCount - previousColorCount;
        AddReward(colorChange);  // Reward is the change in number of tiles

        previousColorCount = currentColorCount;  // Update the count for the next step
    }

    private int GetCurrentColorCount()
    {
        sumTiles counter = tilemap.GetComponent<sumTiles>();
        if (counter != null)
        {
            // Retrieve the float count and convert to int properly
            float count = 0f;
            if (paintScript.PaintColor == Color.red) count = counter.redCount;
            else if (paintScript.PaintColor == Color.green) count = counter.greenCount;
            else if (paintScript.PaintColor == Color.blue) count = counter.blueCount;

            // Round the float to the nearest whole number before converting to int
            // This helps in reducing errors due to truncation of decimals
            return Mathf.RoundToInt(count);
        }
        return 0;
    }

    //venv\scripts\activate
    //mlagents-learn config.yaml --run-id=runID
}
