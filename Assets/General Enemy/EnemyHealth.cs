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
    [SerializeField]
    float weakSpotMult = 2.0f;
    [SerializeField]
    float armourSpotMult = 0.5f;

    public delegate void EnemyDied();
    public event EnemyDied enemyDied;

    private void Start()
    {
        health = maxHealth;
    }

    public override int TakeDamage(int damageAmount, DamageSource sourceType, DamageSpot spotType, DamageElement element = DamageElement.Normal)
    {
        int prevHealth = health;

        if (spotType == DamageSpot.Armour) damageAmount = Mathf.CeilToInt(damageAmount * armourSpotMult);
        else if (spotType == DamageSpot.Head) damageAmount = Mathf.FloorToInt(damageAmount * weakSpotMult);

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
        if(enemyDied != null) enemyDied();
        Destroy(this.gameObject);
    }
}
