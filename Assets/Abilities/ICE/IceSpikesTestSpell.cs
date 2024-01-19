using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikesTestSpell : Ability
{

    Rigidbody rb;
    AbilityInputSystem AbilityHoldRef;
    [SerializeField]
    SanitySystem sanityRef;
    PlayerMovement movementRef;
    [SerializeField]
    GameObject spellOrigin;

    [SerializeField]
    GameObject iceSpike;

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
        Instantiate(iceSpike, spellOrigin.transform.position, spellOrigin.transform.rotation);

        sanityRef.Sanity -= sanityCost;

    }

    public override void DemonicStartAbility()
    {

    }
}
