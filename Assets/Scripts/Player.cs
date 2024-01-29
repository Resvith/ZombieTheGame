using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action<int> OnHealthChanged;
    public event Action OnPlayerDead;

    private int healthPoints = 100;

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints < 0)
        {
            healthPoints = 0;
            OnPlayerDead?.Invoke();
        }

        OnHealthChanged?.Invoke(healthPoints);
    }
}
