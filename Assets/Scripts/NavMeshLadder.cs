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
    private bool isOnLadder = false;
    private bool isAcctualGoalReached = true;

    Queue<Vector3> subGoals = new Queue<Vector3>();
    Vector3 acctualGoal = new Vector3();


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
        CheckIfAcctualGoalIsReached();
        if (!isAcctualGoalReached)
        {
            return;
        }
        // If agent is bellow player find ladder
        if (agent.transform.position.y + 1 < player.transform.position.y)
        {
            FindNearestLadder();
            print("Searching ladders");
        }
        if (subGoals is null)
        {
            print("Player is a goal");
            acctualGoal = player.transform.position;
        }
        else if (subGoals.Count > 0)
        {
            print("Setting subgoal");
            acctualGoal = subGoals.Dequeue();
        }
        isAcctualGoalReached = false;
    }

    private void CheckIfAcctualGoalIsReached()
    {

    }

    //private void SetGoal()
    //{
    //    if (isLadderGoal) return;
    //    if (agent.transform.position.y + 2 < player.transform.position.y)
    //    {
    //        closestLadder = FindNearestLadder();
    //        FindWaypoint();
    //        isLadderGoal = true;
    //        print("Cel: drabina");
    //    }
    //    else
    //    {
    //        goal = player.position;
    //        isLadderGoal = false;
    //        print("Cel: Gracz");
    //    }
    //    print(goal);
    //    agent.SetDestination(goal);
    //}

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
        print("Enemy touched ladder");
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