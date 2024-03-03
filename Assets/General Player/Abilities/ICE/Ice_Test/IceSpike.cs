using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : MonoBehaviour
{
    public int abilityDamage;
    DamageNumberManager hitNumberRef;

    public List<Damageable> hitTargets;

    private void Start()
    {
        hitNumberRef = DamageNumberManager.GetManager();
    }
    private void OnTriggerEnter(Collider other)

    {
        var hitbox = other.GetComponent<Hitbox>();
        if (hitbox != null)
        {
            foreach (Damageable ii in hitTargets)
            {
                if (hitbox.GetOwner() == ii)
                {
                    Debug.Log("Already hit this enemy");
                    return;
                }
            }
            if (hitbox.GetOwner().team == "Enemy")
            {
                int hit = hitbox.GetOwner().TakeDamage(abilityDamage, DamageDetails.DamageSource.Ability, hitbox.GetSpotType(), DamageDetails.DamageElement.Ice);
                Vector3 location = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                hitNumberRef.CreateDamageNumber(hit, location, DamageDetails.DamageElement.Ice, hitbox.GetSpotType());
                hitTargets.Add(hitbox.GetOwner());
            }
        }
    }
}
