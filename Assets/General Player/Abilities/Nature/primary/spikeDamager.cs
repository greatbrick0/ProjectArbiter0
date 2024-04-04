using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeDamager : MonoBehaviour
{
    
    DamageNumberManager hitNumberRef;
    public int abilityDamage;
      public List<Damageable> hitTargets;


    private void Awake()
    {
         hitNumberRef = DamageNumberManager.GetManager();
          hitTargets.Clear();
    }

    public void ClearTargets()
    {
        hitTargets.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggerstay!");
        var hitbox = other.GetComponent<Hitbox>();
        if (hitbox != null)
        {
            Debug.Log("Hitbox isnt null!");
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
                int hit = hitbox.GetOwner().TakeDamage(abilityDamage, DamageDetails.DamageSource.Ability, hitbox.GetSpotType(), DamageDetails.DamageElement.Eldritch);
                Vector3 location = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                hitNumberRef.CreateDamageNumber(hit, location, DamageDetails.DamageElement.Eldritch, spot);
                hitTargets.Add(hitbox.GetOwner());
            }
            
        }
        else
        {
            
        }
      
    }
}
