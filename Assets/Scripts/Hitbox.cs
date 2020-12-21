using System;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public List<ICombat> SubscribedEnemies { get; private set; }

    public Action OnEnemyListUpdate;

    void Start()
    {
        SubscribedEnemies = new List<ICombat>();
    }

    void Update()
    {
        for (int i = SubscribedEnemies.Count - 1; i >= 0; i--)
        {
            if (SubscribedEnemies[i].IsAlive == false)
            {
                SubscribedEnemies.Remove(SubscribedEnemies[i]);
                OnEnemyListUpdate?.Invoke();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        ICombat tempEnemy = other.GetComponent<ICombat>();
        if (tempEnemy != null && !SubscribedEnemies.Contains(tempEnemy))
        {
            //Debug.Log($"Subbed: {other.gameObject.name}");
            SubscribedEnemies.Add(tempEnemy);
            OnEnemyListUpdate?.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        ICombat tempEnemy = other.GetComponent<ICombat>();
        if (tempEnemy != null && !SubscribedEnemies.Contains(tempEnemy))
        {
            //Debug.Log($"Unsubbed: {other.gameObject.name}");
            SubscribedEnemies.Remove(tempEnemy);
            OnEnemyListUpdate?.Invoke();
        }
    }
}
