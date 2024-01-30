using UnityEngine;
using UnityEngine.AI;

public class NavMeshLadder : MonoBehaviour
{
    [SerializeField] private float climbingSpeed = 1f;
    [SerializeField] private float fallingSpeed = 10f;

    private Player player;
    private NavMeshAgent agent;
    private float basicSpeed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
        basicSpeed = agent.speed;
    }

    void Update()
    {
        agent.SetDestination(player.transform.position);
        SetFallingClimbingSpeed();
        TurnToPlayerWhileAttacking();
    }

    void SetFallingClimbingSpeed()
    {
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
            agent.speed = basicSpeed;
        }
    }

    void TurnToPlayerWhileAttacking()
    {
        if (player.transform.position.y - transform.position.y < 1f)
        {
            transform.LookAt(player.transform.position);
        }
    }
}