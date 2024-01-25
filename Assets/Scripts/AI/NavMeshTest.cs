using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
    public Transform player; // Referencja do transformacji gracza.
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(player.position); // Ustaw nowy punkt docelowy na pozycjê gracza
    }
}