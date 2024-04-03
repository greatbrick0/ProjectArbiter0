using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VinePatchLogic : MonoBehaviour
{

    public PlayerSpellNatureSpikes playerSpikeRef;

    public int abilityDamage;

    DamageNumberManager hitNumberRef;

    [SerializeField]
    private VisualEffect vfxRef;

    [SerializeField]
    private MeshCollider triggerBox;

    [SerializeField]
    private float lifespan;

     [SerializeField]
    private float spikeInterval;

      public List<Damageable> hitTargets;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(RequestDestroy),lifespan);
        Invoke(nameof(Trigger),spikeInterval/2f);
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
                Trigger();
                var spot = hitbox.GetSpotType();
                int hit = hitbox.GetOwner().TakeDamage(abilityDamage, DamageDetails.DamageSource.Ability, hitbox.GetSpotType(), DamageDetails.DamageElement.Ice);
                Vector3 location = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                hitNumberRef.CreateDamageNumber(hit, location, DamageDetails.DamageElement.Fire, spot);
                hitTargets.Add(hitbox.GetOwner());
            }
            
        }
        else
        {
            
        }
      
    }

    private void RequestDestroy()
    {

    }

    private void Trigger()
    {
        Debug.Log("TestTrigger");
        hitTargets.Clear();
        //triggerBox.setActive(false);
        vfxRef.SendEvent("Spikes");
        spikeInterval -= 0.2f;

        Invoke(nameof(disableTrigger),spikeInterval);
    }

    private void disableTrigger()
    {
        Invoke(nameof(Trigger),spikeInterval);
    }

}
