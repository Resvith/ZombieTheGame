using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action OnEnemyKilled;

    [SerializeField] float attackRange = 1.5f;
    [SerializeField] int attackDamage = 1;
    [SerializeField] int enemyHp = 5;
    [SerializeField] float attackCooldown = 1.5f;
    [SerializeField] int scoreForKill = 10;

    private Player player;
    private bool canAttack = true;
    private Animator animator;
    private UITextChanger textChanger;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        textChanger = FindObjectOfType<UITextChanger>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange && canAttack)
        {
            StartCoroutine(AttackPlayer(attackDamage));
        }
    }

    public void TakeDamage(int damageAmount)
    {
        enemyHp -= damageAmount;
        if (enemyHp <= 0)
        {
            EnemyDie();
        }
    }

    public void EnemyDie()
    {
        OnEnemyKilled?.Invoke();
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
        textChanger.IncreaseScore(scoreForKill);
    }


    IEnumerator AttackPlayer(int attackDamage)
    {
        if (canAttack)
        {
            canAttack = false;
            player.TakeDamage(attackDamage);
            animator.SetTrigger("EnemyAttack");
        }
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
