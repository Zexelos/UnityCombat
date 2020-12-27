using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, ICombat
{
    [SerializeField] float maxHealth = 10f;
    [SerializeField] float cooldown = 2f;
    [SerializeField] float damage = 2f;

    public bool IsAlive { get; private set; }

    bool canAttack;
    float currentHealth;

    void Start()
    {
        canAttack = true;
        IsAlive = true;
        currentHealth = maxHealth;
    }

    public void DealDamage(float damage)
    {

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

    public void DealDamageTo(Player player)
    {
        if (canAttack == true)
        {
            player.TakeDamage(damage);
            StartCoroutine(SetCooldown());
        }
    }

    public IEnumerator SetCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(cooldown);

        canAttack = true;
    }
}
