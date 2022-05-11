using UnityEngine;

public class Player : MonoBehaviour
{
    private const int MinSetableHealth = 10;

    [Range(10,100)]
    [SerializeField] private int _maxHealth;

    public int Health { get; private set; }
    public int MaxHealth => _maxHealth;

    public void TakeDamage(int damage)
    {
        if (damage < Health)
        {
            Health -= damage;
        }
        else
        {
            Health = 0;
        }
    }

    public void TakeHealth(int health)
    {
        if (health > 0)
        {
            if (Health + health < _maxHealth)
            {
                Health += health;
            }
            else
            {
                Health = _maxHealth;
            }
        }
    }

    private void OnValidate()
    {
        if (_maxHealth < MinSetableHealth)
        {
            _maxHealth = MinSetableHealth;
        }

        Health = _maxHealth;
    }
}
