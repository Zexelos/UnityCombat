using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICombat
{
    [SerializeField] float maxHealth = 10f;
    [SerializeField] float damage = 2f;
    [SerializeField] Hitbox hitbox = default;

    List<ICombat> currentEnemies;

    float currentHealth;

    public bool IsAlive => currentHealth > 0;

    public void DealDamage(float damage)
    {
        if (currentEnemies != null)
        {
            foreach (var item in currentEnemies)
                item.TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    void Start()
    {
        currentEnemies = new List<ICombat>();

        hitbox.OnEnemyListUpdate += GetCurrentEnemiesList;

        currentHealth = maxHealth;
    }

    public void GetCurrentEnemiesList()
    {
        Debug.Log("GetCurrentEnemiesList event fired");
        currentEnemies = hitbox.SubscribedEnemies;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DealDamage(damage);
        }

        #region Prototype
        /*
		if (Input.GetMouseButtonDown(0))
        {
            RaycastHit[] whatPlayerHit = Physics.CapsuleCastAll(hitbox.bounds.min, hitbox.bounds.max, hitbox.radius, Vector3.forward, 1.5f, 6);
        
            if (whatPlayerHit != null)
            {
                Debug.Log("You hit something");
                foreach (var item in whatPlayerHit)
                {
                    Debug.Log($"You hit {item.transform.gameObject.name}");
                    ICombat combat = item.transform.gameObject.GetComponent<ICombat>();
        
                    if (combat != null)
                    {
                        combat.TakeDamage(damage);
                        Debug.Log($"combat interface detected");
                    }
                }
            }
        } 
        */
        #endregion
    }
}