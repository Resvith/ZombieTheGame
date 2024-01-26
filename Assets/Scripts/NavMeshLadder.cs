using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshLadder : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Ladder[] ladders;
    private Ladder closestLadder;
    private Ladder lastUsedLadder;

    Vector3 acctualGoal = new Vector3();

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ladders = FindObjectsOfType<Ladder>();
        print(ladders.Length);
    }

    void Update()
    {
        SetGoal();
    }

    private void SetGoal()
    {
        // agent bellow player
        if (player.transform.position.y > agent.transform.position.y)
        {
            closestLadder = FindNearestLadder(isPlayerBellow: false);
            acctualGoal = FindWaypoint(isPlayerBellow: false);
        }
        else if (player.transform.position.y < agent.transform.position.y)
        {
            closestLadder = FindNearestLadder(isPlayerBellow: false);
            acctualGoal = FindWaypoint(isPlayerBellow: true);
        }
        else
        {
            acctualGoal = player.transform.position;
        }
        agent.SetDestination(acctualGoal);
        print("Player Y:" + player.transform.position.y);
        print("Enemy Y:" + agent.transform.position.y);
        print("Actual Goal" + acctualGoal);

    }


    private Vector3 FindWaypoint(bool isPlayerBellow)
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform waypoint in closestLadder.transform)
        {
            waypoints.Add(waypoint);
        }
        if (isPlayerBellow) return waypoints[0].position;
        else return waypoints[1].position;
        
    }

    private Ladder FindNearestLadder(bool isPlayerBellow)
    {
        float closestDistance = Mathf.Infinity;
        foreach (Ladder ladder in ladders)
        {
            if (lastUsedLadder != null)
            {
                if (lastUsedLadder == ladder) continue;
            }
            if (isPlayerBellow && ladder.transform.position.y < player.transform.position.y) continue;
            if (!isPlayerBellow && ladder.transform.position.y > player.transform.position.y) continue;
            float distance = Vector3.Distance(transform.position, ladder.transform.position);
            if (distance < closestDistance)
            {
                closestLadder = ladder;
                closestDistance = distance;
            }
        }
        lastUsedLadder = closestLadder;
        return closestLadder;
    }
}