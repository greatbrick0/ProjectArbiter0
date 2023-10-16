using DamageDetails;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Damageable
{
    [SerializeField]
    int maxHealth = 100;
    [SerializeField]
    int health;

    private void Start()
    {
        health = maxHealth;
    }

    public override int TakeDamage(int damageAmount, DamageSource sourceType, DamageSpot spotType, DamageElement element = DamageElement.Normal)
    {
        int prevHealth = health;
        health -= damageAmount;
        if(health <= 0)
        {
            Die();
            health = Mathf.Max(health, 0);
        }
        return prevHealth - health;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
