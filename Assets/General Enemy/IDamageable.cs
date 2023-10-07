using DamageDetails;
using UnityEngine;

public interface IDamageable
{
    [property: SerializeField]
    public string team { get; set; }
    int TakeDamage(int damageAmount, DamageSource sourceType, DamageSpot spotType, DamageElement element = DamageElement.Normal);
}

