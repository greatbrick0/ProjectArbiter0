using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookLogic : MonoBehaviour
{
    public Vector3 target;

    public PlayerSpellNatureGrapple grappleSpellRef;
    public int abilityDamage;
    DamageNumberManager hitNumberRef;

    public float speed;

    public float lifespan;

    int count;


    public List<Damageable> hitTargets;


    bool reachedTarget = false;
    private void Start()
    {
        hitNumberRef = DamageNumberManager.GetManager();
        Invoke(nameof(RequestDestroy), lifespan);
        //GetComponent<Rigidbody>().AddForce(transform.forward * 4 + transform.up * 3, ForceMode.Impulse);
    }
private void Update()
    {
        if (target != null)
        {
            if (!reachedTarget)
            GetComponent<Rigidbody>().AddForce((target - transform.position).normalized * speed, ForceMode.Force);
        }

        if ((target - transform.position).magnitude < 1)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            LockAndPull();
            reachedTarget = true;

        }
    }
private void OnTriggerEnter(Collider other)
    {

        var hitbox = other.GetComponent<Hitbox>();
        if (hitbox != null)
        {
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

    }
public void LockAndPull()
    {
        if (!reachedTarget)
        {
            transform.position = target;
            grappleSpellRef.Launch();
        }
    }

    public void RequestDestroy() //Hello there!
    {
        Destroy(this.gameObject);
    }

    public void Trigger()
    {

    }



}