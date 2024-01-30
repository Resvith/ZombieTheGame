using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action OnEnemyKilled;

    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private int _attackDamage = 2;
    [SerializeField] private int _enemyHp = 5;
    [SerializeField] private float _attackCooldown = 1.5f;
    [SerializeField] private int _scoreForKill = 10;

    private Player _player;
    private Animator _animator;
    private UITextChanger _textChanger;
    private bool _canAttack = true;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _textChanger = FindObjectOfType<UITextChanger>();
    }

    void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, _player.transform.position);
        float playerHeightDifference = Math.Abs(_player.transform.position.y - transform.position.y);
        if (playerDistance < _attackRange && _canAttack || playerDistance - playerHeightDifference < 0.2 && _canAttack)
            StartCoroutine(AttackPlayer(_attackDamage));
    }

    public void TakeDamage(int damageAmount)
    {
        _enemyHp -= damageAmount;
        if (_enemyHp <= 0)
            EnemyDie();
    }

    public void EnemyDie()
    {
        OnEnemyKilled?.Invoke();
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
        _textChanger.IncreaseScore(_scoreForKill);
    }


    IEnumerator AttackPlayer(int attackDamage)
    {
        if (_canAttack)
        {
            _canAttack = false;
            _player.TakeDamage(attackDamage);
            _animator.SetTrigger("EnemyAttack");
        }
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }
}
