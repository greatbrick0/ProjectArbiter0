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
    
    public override void RecieveAbilityRequest()
    {
        Debug.Log("StartedAbility");
        GetNeededComponents();
        HUDRef.SetCooldownForIcon(tier, maxCooldownTime);

        sanityRef.Sanity -= sanityCost;
        HUDRef.UseAbility(tier);
        StartCoroutine(Cooldown(false));
        StartAbility();
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
