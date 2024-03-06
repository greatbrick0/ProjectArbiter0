using Coherence;
using Coherence.Toolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellFire_Shot : Ability
{


    [SerializeField]
    GameObject flareObj;

    GameObject flareRef;

    List<GameObject> enemyHitList;
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
        sync.SendCommand<PlayerSpellFire_Shot>(nameof(StartAbility), MessageTarget.All);
    }

    public override void RecieveDemonicAbilityRequest()
    {
        Debug.Log("DemonicVarient-StartFireShot");
        GetNeededComponents();
        HUDRef.SetCooldownForIcon(tier, maxCooldownTime / 2);

        //Skip sanitycost, you are DEMONIC!!
        HUDRef.UseAbility(tier);
        StartCoroutine(Cooldown(true));
    }

    public override void StartAbility()
    {
        ApplyPlayerCastMotion(); //applies slow to player during casting.
        AbilityIntroductionDecorations();
        StartCoroutine(Windup());
    }

    public override void AbilityIntroductionDecorations()
    {

    }

    public override void AbilityAction()
    {
        Debug.Log("FireFlare - AbilityAction");
        flareRef = Instantiate(flareObj, spellOrigin.transform);
        //FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.FireCone, gameObject);
        RemovePlayerCastMotion();
    }



    public override void newDemonic()
    {

    }
}
