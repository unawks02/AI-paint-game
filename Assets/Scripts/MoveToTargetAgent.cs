using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToTargetAgent : Agent
{

    [SerializeField] private Transform env;
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;


    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(1.5f, 3.5f), Random.Range(-3.5f, 3.5f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((Vector2)transform.localPosition);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* replace with reward function
        if (collision.TryGetComponent(out Target target))
        {
            AddReward(2f);
            //backgroundSpriteRenderer.color = Color.green;
            EndEpisode();
        }
        */
        if (collision.TryGetComponent(out Wall wall))
        {
            AddReward(-2f);
            //backgroundSpriteRenderer.color = Color.red;
            EndEpisode();
        }
        
    }


}
