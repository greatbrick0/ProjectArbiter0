using Coherence;
using Coherence.Toolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikesTestSpell : Ability
{
    

    [SerializeField]
    GameObject iceSpike;

    GameObject newSpike;

    protected override void GetNeededComponents()
    {
        AbilityHoldRef = GetComponent<AbilityInputSystem>();
        sanityRef = GetComponent<SanitySystem>();
        movementRef = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        sync = GetComponent<CoherenceSync>();
        
        
    }


    public override void RecieveAbilityRequest()
    {
        Debug.Log("StartedAbility");
        GetNeededComponents();
        HUDRef.SetCooldownForIcon(tier, maxCooldownTime);

        sanityRef.Sanity -= sanityCost;
        HUDRef.UseAbility(tier);
        StartCoroutine(Cooldown(false));
        sync.SendCommand<IceSpikesTestSpell>(nameof(StartAbility), MessageTarget.All, false);
    }

    public override void RecieveDemonicAbilityRequest()
    {
        Debug.Log("DemonicVarient-StartIceSpike");
        GetNeededComponents();
        HUDRef.SetCooldownForIcon(tier, maxCooldownTime / 2);

        //Skip sanitycost, you are DEMONIC!!
        HUDRef.UseAbility(tier);
        StartCoroutine(Cooldown(true));
    }

    public override void StartAbility()
    {
        AbilityIntroductionDecorations();
    }

    public override void AbilityIntroductionDecorations()
    {
    }

    public override void AbilityAction()
    {
        newSpike = Instantiate(iceSpike, spellOrigin.transform.position, spellOrigin.transform.rotation);
        newSpike.GetComponent<IceShatter>().demonic = true;
    }

    public override void newDemonic()
    {

    }
}
