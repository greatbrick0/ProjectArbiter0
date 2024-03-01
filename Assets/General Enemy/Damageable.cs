using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageDetails;

public abstract class Damageable : MonoBehaviour
{
    [field: SerializeField]
    public string team { get; set; }

    /// <summary>
    /// Attempts to do 
    /// </summary>
    /// <param name="damageAmount">The amount of damage </param>
    /// <param name="sourceType"></param>
    /// <param name="spotType"></param>
    /// <param name="element"></param>
    /// <returns>Returns the amount of damage done (useful for lifesteal, vampirism, awarding points). </returns>
    public abstract int TakeDamage(int damageAmount, DamageSource sourceType, DamageSpot spotType, DamageElement element = DamageElement.Normal);
}
