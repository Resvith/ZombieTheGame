using UnityEngine;
using UnityEngine.AI;

public class NavMeshLadder : MonoBehaviour
{
    [SerializeField] private float climbingSpeed = 1f;
    [SerializeField] private float fallingSpeed = 10f;

    private Player _player;
    private NavMeshAgent _agent;
    private float _basicSpeed;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _agent = GetComponent<NavMeshAgent>();
        _basicSpeed = _agent.speed;
    }

    void Update()
    {
        _agent.SetDestination(_player.transform.position);
        SetFallingClimbingSpeed();
        TurnToPlayerWhileAttacking();
    }

    void SetFallingClimbingSpeed()
    {
        if (_agent.isOnOffMeshLink)
        {
            if (_agent.currentOffMeshLinkData.startPos.y < _agent.currentOffMeshLinkData.endPos.y)
                _agent.speed = climbingSpeed;
            else
                _agent.speed = fallingSpeed;
        }
        else
            _agent.speed = _basicSpeed;
    }

    void TurnToPlayerWhileAttacking()
    {
        if (_player.transform.position.y - transform.position.y < 1f)
            transform.LookAt(_player.transform.position);
    }
}