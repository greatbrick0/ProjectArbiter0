using DamageDetails;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Damageable
{
    [field: SerializeField]
    public int maxHealth { get; private set; } = 100;
    [field: SerializeField]
    public int health { get; private set; }

    [SerializeField]
    private float weakSpotMult = 2.0f;
    [SerializeField]
    private float armourSpotMult = 0.5f;
    [SerializeField]
    public List<DamageElement> ignoredElements = new List<DamageElement>();
    [SerializeField]
    public List<DamageSource> ignoredSources = new List<DamageSource>();


    public delegate void EnemyDied();
    public event EnemyDied enemyDied;

    private void Start()
    {
        health = maxHealth;
    }

    public override int TakeDamage(int damageAmount, DamageSource sourceType, DamageSpot spotType, DamageElement element = DamageElement.Normal)
    {
        int prevHealth = health;

        if (ignoredElements.Contains(element) || ignoredSources.Contains(sourceType)) damageAmount = 0;
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
