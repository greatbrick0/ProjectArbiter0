using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellIceDash : Ability
{
    [SerializeField]
    float pauseDuration;

    

    [SerializeField]
    float backVelocity;
    [SerializeField]
    float forwardVelocity;

    Rigidbody rb;
    AbilityInputSystem AbilityHoldRef;
    SanitySystem sanityRef;
    PlayerMovement movementRef;

    private void GetNeededComponents()
    { 
        AbilityHoldRef = GetComponent<AbilityInputSystem>();
        sanityRef = GetComponent<SanitySystem>();
        movementRef = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    public override void StartAbility()
    {
        GetNeededComponents();

        sanityRef.Sanity -= sanityCost;

        rb.AddForce(Vector3.back * backVelocity);

        
    }

    public override void DemonicStartAbility()
    {

    }
        
    
}
