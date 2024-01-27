using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action<int> OnHealthChanged;

    private int healthPoints = 100;

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints < 0)
            healthPoints = 0;

        OnHealthChanged?.Invoke(healthPoints);
    }
}
