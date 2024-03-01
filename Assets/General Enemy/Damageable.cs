using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageDetails;

public abstract class Damageable : MonoBehaviour
{
    [field: SerializeField]
    public string team { get; set; }

    /// <summary>
    /// Attempts to deal damage to a target.
    /// </summary>
    /// <param name="damageAmount">The amount of damage that will be processed. </param>
    /// <param name="sourceType"></param>
    /// <param name="spotType">The place on the body this damage was applied to. </param>
    /// <param name="element">The element of the damage.</param>
    /// <returns>Returns the amount of damage done (useful for lifesteal, vampirism, awarding points). </returns>
    public abstract int TakeDamage(int damageAmount, DamageSource sourceType, DamageSpot spotType, DamageElement element = DamageElement.Normal);
}
