using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
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
}
