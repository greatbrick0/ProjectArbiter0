using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : MonoBehaviour
{
    public int abilityDamage;
    private void OnTriggerEnter(Collider other)
    {
        var hitbox = other.GetComponent<Hitbox>();
        Debug.Log(other.name);
        if (hitbox != null)
        {
            if (hitbox.GetOwner().team == "Enemy")
                hitbox.GetOwner().TakeDamage(abilityDamage, DamageDetails.DamageSource.Ability, hitbox.GetSpotType());
        }
    }
}
