using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashHitBoxScipt : MonoBehaviour
{

    public PlayerSpellIceDash dashAbilityRef;
    public int abilityDamage;
    DamageNumberManager hitNumberRef;

    public List<Damageable> hitTargets;

    public float lifespan;

    private void Start()
    {   
        hitNumberRef = DamageNumberManager.GetManager();
        StartCoroutine("DurationDash");
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
                    return;
                }
            }
            if (hitbox.GetOwner().team == "Enemy")
            {
                var spot = hitbox.GetSpotType();
                int hit = hitbox.GetOwner().TakeDamage(abilityDamage, DamageDetails.DamageSource.Ability, hitbox.GetSpotType());
                Vector3 location = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                hitNumberRef.CreateDamageNumber(hit, location, DamageDetails.DamageElement.Ice, spot);
                hitTargets.Add(hitbox.GetOwner());
                dashAbilityRef.EndDash(true);
            }
        }
    }

    public IEnumerator DurationDash()
    {
        yield return new WaitForSeconds(lifespan);
        dashAbilityRef.EndDash(false);
    }

    public void RequestDestroy() //Hello there!
    {
        Destroy(this.gameObject);
    }
}
