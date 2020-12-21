using UnityEngine;

public class Enemy : MonoBehaviour, ICombat
{
    [SerializeField] float maxHealth = 10f;

    float currentHealth;

    public bool IsAlive { get; private set; }

    void Start()
    {
        IsAlive = true;
        currentHealth = maxHealth;
    }

    public void DealDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage, current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            IsAlive = false;
            Destroy(gameObject);
        }
    }
}
