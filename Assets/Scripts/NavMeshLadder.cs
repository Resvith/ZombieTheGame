using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshLadder : MonoBehaviour
{
    public Transform player; // Referencja do transformacji gracza.
    private NavMeshAgent agent;
    private Ladder[] ladders;
    private Ladder closestLadder;
    private Vector3 goal;
    private bool isLadderGoal;
    private bool isOnLadder = false;
    



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ladders = FindObjectsOfType<Ladder>();
        print(ladders.Length);
        foreach (Ladder ladder in ladders)
        {
            ladder.EnemyOnLadder += EnemyClimbing;
        }
    }

    void Update()
    {
        //agent.SetDestination(player.position)
        SetGoal();
    }

    private void SetGoal()
    {
        if (isLadderGoal) return;
        if (agent.transform.position.y + 2 < player.transform.position.y)
        {
            closestLadder = FindNearestLadder();
            FindWaypoint();
            isLadderGoal = true;
            print("Cel: drabina");
        }
        else
        {
            goal = player.position;
            isLadderGoal = false;
            print("Cel: Gracz");
        }
        print(goal);
        agent.SetDestination(goal);
    }

    private void FindWaypoint()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform waypoint in closestLadder.transform)
        {
            waypoints.Add(waypoint);
        }
        goal = waypoints[0].transform.position;
    }

    private Ladder FindNearestLadder()
    {
        float closestDistance = Mathf.Infinity;
        foreach (Ladder ladder in ladders)
        {
            float distance = Vector3.Distance(transform.position, ladder.transform.position);
            if (distance < closestDistance)
            {
                closestLadder = ladder;
                closestDistance = distance;
            }
        }
        print(closestLadder.transform.position);
        return closestLadder;
    }

    private void EnemyClimbing()
    {
        //agent.enabled = false;
        isOnLadder = true;
/*        while (isLadderGoal) 
        {
            float x = transform.position.x;
            float y = transform.position.y + 1;
            float z = transform.position.z;
            Vector3 moveVector = new Vector3(x, y, z);
            agent.Move(moveVector);
        }*/
        
    }
}