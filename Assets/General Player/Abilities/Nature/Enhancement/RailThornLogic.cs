using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailThornLogic : MonoBehaviour
{

      [SerializeField]
    private float lifespan;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,lifespan);
    }

    
    DamageNumberManager hitNumberRef;
    public int abilityDamage;
    
    public List<Damageable> hitTargets;


    private void Awake()
    {
         hitNumberRef = DamageNumberManager.GetManager();
    }

    public void ClearTargets()
    {
        hitTargets.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter");
        var hitbox = other.GetComponent<Hitbox>();
        if (hitbox != null)
        {
            Debug.Log("HitSomething");
            foreach (Damageable ii in hitTargets)
            {
                if (hitbox.GetOwner() == ii)
                {
                    return;
                }
            }
            if (hitbox.GetOwner().team == "Enemy")
            {
                Debug.Log("HitEnemy");
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
