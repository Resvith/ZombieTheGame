using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshLadder : MonoBehaviour
{
    public Transform player; // Referencja do transformacji gracza.
    private NavMeshAgent agent;
    private Ladder[] ladders;
    private Ladder closest;
    private Vector3 goal;
    private bool isLadderGoal;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ladders = FindObjectsOfType<Ladder>();
        print(ladders.Length);
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
            goal = FindNearestLadder();
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

    private Vector3 FindNearestLadder()
    {
        float closestDistance = Mathf.Infinity;
        foreach (Ladder ladder in ladders)
        {
            float distance = Vector3.Distance(transform.position, ladder.transform.position);
            if (distance < closestDistance)
            {
                closest = ladder;
                closestDistance = distance;
            }
        }
        print(closest.transform.position);
        return closest.transform.position;
    }
}