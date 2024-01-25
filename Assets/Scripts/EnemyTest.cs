using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public Transform player; // Przypisz w inspektorze transformacj� gracza
    public float attackRange = 5f; // Zakres, w kt�rym wr�g b�dzie atakowa�
    public Transform enemy;
    public int enemyHp = 5;

    public void Damage(int damageAmount)
    {
        enemyHp -= damageAmount;
        print(enemyHp);
        if (enemyHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {

    }
}
