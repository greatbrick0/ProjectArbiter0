using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageDetails;

public abstract class Damageable : MonoBehaviour
{
    [field: SerializeField]
    public string team { get; set; }

    //returns the amount of damage done (useful for lifesteal, vampirism, awarding points) 
    public abstract int TakeDamage(int damageAmount, DamageSource sourceType, DamageSpot spotType, DamageElement element = DamageElement.Normal);
}
