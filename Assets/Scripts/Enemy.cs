using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] int attackDamage = 1;
    [SerializeField] int enemyHp = 5;
    [SerializeField] float attackCooldown = 1.5f;

    private Player player;
    private bool canAttack = true;

    public void TakeDamage(int damageAmount)
    {
        enemyHp -= damageAmount;
        if (enemyHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange && canAttack)
        {
            StartCoroutine(AttackPlayer(attackDamage));
        }
    }

    IEnumerator AttackPlayer(int attackDamage)
    {
        if (canAttack)
        {
            canAttack = false;
            player.TakeDamage(attackDamage);
        }
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
