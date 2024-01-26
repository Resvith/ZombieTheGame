using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshLadder : MonoBehaviour
{
    public Transform player;

    [SerializeField] private float climbingSpeed = 1f;
    [SerializeField] private float fallingSpeed = 10f;

    private NavMeshAgent agent;
    private float speed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
    }

    void Update()
    {
        agent.SetDestination(player.transform.position);
        print(agent.isOnOffMeshLink);
        if (agent.isOnOffMeshLink)
        {
            if (agent.currentOffMeshLinkData.startPos.y < agent.currentOffMeshLinkData.endPos.y)
            {
                agent.speed = climbingSpeed;
            }
            else
            {
                agent.speed = fallingSpeed;
            }
        }
        else
        {
            agent.speed = speed;
        }
    }

}