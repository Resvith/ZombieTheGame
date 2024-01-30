using UnityEngine;

public class ShootableBox : MonoBehaviour
{
    [SerializeField]
    private int _currentHealth = 3;

    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
