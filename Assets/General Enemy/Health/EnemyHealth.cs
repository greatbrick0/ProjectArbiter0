using DamageDetails;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using FMODUnity;
using Coherence.Toolkit;
using Coherence;

public class EnemyHealth : Damageable
{
    [field: SerializeField]
    public int maxHealth { get; private set; } = 100;
    [field: SerializeField]
    public int health;

    [field: SerializeField] public EventReference deathSound { get; private set; }

    [SerializeField]
    private float weakSpotMult = 2.0f;
    [SerializeField]
    private float armourSpotMult = 0.5f;
    [SerializeField]
    public List<DamageElement> ignoredElements = new List<DamageElement>();
    [SerializeField]
    public List<DamageSource> ignoredSources = new List<DamageSource>();

    private CoherenceSync sync;


    public delegate void EnemyDied();
    public event EnemyDied enemyDied;

    private void Start()
    {
        health = maxHealth;
        sync = GetComponent<CoherenceSync>();
    }

    public override int TakeDamage(int damageAmount, DamageSource sourceType, DamageSpot spotType, DamageElement element = DamageElement.Normal)
    {
        int prevHealth = health;

        if (ignoredElements.Contains(element) || ignoredSources.Contains(sourceType)) damageAmount = 0;
        else BroadcastMessage("HurtAnim");
        if (spotType == DamageSpot.Body)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.bodyHit, gameObject);
        }
        else if (spotType == DamageSpot.Head)
        {
            damageAmount = Mathf.FloorToInt(damageAmount * weakSpotMult);
            FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.citicalHit, gameObject);
        }
        else if (spotType == DamageSpot.Armour)
        {
            damageAmount = Mathf.FloorToInt(damageAmount * armourSpotMult);
            FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.bodyHit, gameObject);
        }

        health -= damageAmount;

        if (health <= 0) sync.SendCommand<EnemyHealth>(nameof(Die), MessageTarget.All);

        health = Mathf.Max(health, 0);

        return prevHealth - health;
    }

    [Command]
    public void Die()
    {
        RuntimeManager.PlayOneShotAttached(deathSound, gameObject);
        print("dead");
        if(enemyDied != null) enemyDied();
        Destroy(this.gameObject);
    }
}