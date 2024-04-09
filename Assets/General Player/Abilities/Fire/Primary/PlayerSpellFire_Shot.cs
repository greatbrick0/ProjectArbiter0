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
        if (HasAuthority())
        {
            HUDRef.SetCooldownForIcon(tier, maxCooldownTime);
            HUDRef.UseAbility(tier);
        }

        sanityRef.Sanity -= sanityCost;
        StartCoroutine(Cooldown(false));
        StartAbility();
    }

    public override void RecieveDemonicAbilityRequest()
    {
        throw new NotImplementedException();
    }

    public override void StartAbility()
    {
        ApplyPlayerCastMotion(); //applies slow to player during casting.
        AbilityIntroductionDecorations();
        StartCoroutine(Windup());
    }

    public override void AbilityIntroductionDecorations()
    {
        animRef.SetTrigger("CastFlames");
    }

    public override void AbilityAction()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.fireCone, gameObject);
        Debug.Log("FireFlare - AbilityAction");
        flareRef = Instantiate(flareObj, spellOrigin.transform);
        RemovePlayerCastMotion();
    }


    public override IEnumerator Windup() //duration of the introduction decorations, followed by AbilityAction
    {
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.casting;
        weaponRef.SetDefaultBehaviourEnabled(true, false);
        yield return new WaitForSeconds(windupTime);
        AbilityAction();
        yield return new WaitForSeconds(0.5f);
        weaponRef.SetDefaultBehaviourEnabled(true, true, true);
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.idle;

    }


    //I dont need these lmaaaaaao
    public override void newDemonic()
    {

    }

    public override void EmergencyCancel()
    {

    }

}
