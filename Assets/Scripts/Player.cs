using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action<int> OnHealthChanged;
    public event Action OnPlayerDead;

    private int _healthPoints = 100;

    public void TakeDamage(int damage)
    {
        _healthPoints -= damage;
        if (_healthPoints < 0)
        {
            _healthPoints = 0;
            OnPlayerDead?.Invoke();
        }

        OnHealthChanged?.Invoke(_healthPoints);
    }
}
