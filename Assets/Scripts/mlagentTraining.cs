using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.Tilemaps;


public class mlagentTraining : Agent
{

    //moved your old version to "oldMLAgentProcGenBug"
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap collidableTilemap;  // Assign this in the Unity Editor

    [SerializeField] private paint paintScript;


    public override void OnEpisodeBegin()
    {
        // Reset the agent's position or other states as needed
        transform.localPosition = new Vector3(Random.Range(-5.0f, 15.0f), Random.Range(-5.0f, -15.0f), 0);

        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3Int agentGridPosition = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), 0);
        int gridRadius = 1;  // For a 3x3 grid

        // Iterate through a 3x3 grid centered on the agent
        for (int dy = -gridRadius; dy <= gridRadius; dy++)
        {
            for (int dx = -gridRadius; dx <= gridRadius; dx++)
            {
                Vector3Int tilePos = agentGridPosition + new Vector3Int(dx, dy, 0);

                // Check if the tile is walkable
                bool isWalkable = collidableTilemap != null && !collidableTilemap.HasTile(tilePos);
                sensor.AddObservation(isWalkable);  // Add walkable status as a separate input (1 for walkable, 0 for not)

                // Check if the tile is painted in the agent's color
                bool isPainted = false;
                if (tilemap.HasTile(tilePos))
                {
                    Color tileColor = tilemap.GetColor(tilePos);
                    isPainted = tileColor.Equals(paintScript.PaintColor);  // Check if the tile color matches the agent's color
                }
                sensor.AddObservation(isPainted);  // Add painted status as a separate input (1 for painted, 0 for not)
            }
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        float movementSpeed = 5.0f;
        Vector2 newPosition = new Vector2(moveX * movementSpeed * Time.deltaTime, moveY * movementSpeed * Time.deltaTime);
        transform.Translate(newPosition);

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {

        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");

    }

}
