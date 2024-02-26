using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashHitBoxScipt : MonoBehaviour
{

    public PlayerSpellIceDash dashAbilityRef;
    public int abilityDamage;
    DamageNumberManager hitNumberRef;

    public List<Damageable> hitTargets;

    public float lifeSpan;

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
                var spot = hitbox.GetSpotType();
                int hit = hitbox.GetOwner().TakeDamage(abilityDamage, DamageDetails.DamageSource.Ability, hitbox.GetSpotType());
                Vector3 location = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                hitNumberRef.CreateDamageNumber(hit,location,DamageDetails.DamageElement.Ice, spot);
                hitTargets.Add(hitbox.GetOwner());
                dashAbilityRef.EndDash();
            }
        }
    }

    public void RequestDestroy() //Hello there!
    {
        Destroy(this.gameObject);
    }
}
