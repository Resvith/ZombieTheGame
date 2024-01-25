using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;

public class MoveToPlayerBasic : Agent
{
    private Transform player;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private Material winMaterial;
    [SerializeField] private MeshRenderer groundMeshRenderer;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Transform playerTransform;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-3f, 3f), 1, Random.Range(0.3f, 3f));
        playerTransform.localPosition = new Vector3(Random.Range(-3f, 3f), 1, Random.Range(-3f, -0.3f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(playerTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            SetReward(+1f);
            groundMeshRenderer.material = winMaterial;
            EndEpisode();
        }
        else if (collider.CompareTag("Wall"))
        {
            SetReward(-1f);
            groundMeshRenderer.material = loseMaterial;
            EndEpisode();
        }
    }
}
