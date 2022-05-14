using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(MinSetableHealth,MaxSetableHealth)]
    [SerializeField] private int _maxHealth;
    
    private const int MinSetableHealth = 10;
    private const int MaxSetableHealth = 100;
    
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
        _maxHealth = Mathf.Clamp(_maxHealth,MinSetableHealth,MaxSetableHealth );
        Health = _maxHealth;
    }
}
