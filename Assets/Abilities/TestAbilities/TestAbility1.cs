using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestAbility1 : Ability
{

    protected Collider collideRef;
    protected Rigidbody rb;

    protected Vector3 direction;

    protected override void AssignAbilityComponents()
    {
        collideRef = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    public override void SetDemonic()
    {
        throw new System.NotImplementedException();
    }
    private void OnEnable() //this plays after AssignAbilityComponents.
    {
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        Debug.Log(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        var hitbox = other.GetComponent<Hitbox>();
        if (hitbox != null)
        {
            if (hitbox.GetOwner().team == "Enemy")
                hitbox.GetOwner().TakeDamage(abilityDamage, DamageDetails.DamageSource.Ability, hitbox.GetSpotType());
        }
    }


}
