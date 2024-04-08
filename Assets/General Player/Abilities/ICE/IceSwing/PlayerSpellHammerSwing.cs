using Coherence.Toolkit;
using Coherence;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellHammerSwing : Ability
{


    [SerializeField]
    GameObject IceHammer;

    GameObject newHammer;

    


    public override void RecieveAbilityRequest()
    {
        Debug.Log("StartedHammerSwing");
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
        Debug.Log("DemonicVarient-HammerSwing-UNIMPLEMENTED");
        GetNeededComponents();
        HUDRef.SetCooldownForIcon(tier, maxCooldownTime / 2);
    }

    public override void StartAbility()
    {
        ApplyPlayerCastMotion(); //applies slow to player during casting.
        AbilityIntroductionDecorations();
        StartCoroutine(Windup());
    }

    public override void AbilityIntroductionDecorations()
    {
        animRef.SetTrigger("Hammer");
        //hammer windup sfx
    }

    public override void AbilityAction() //begins at end of Windup (base ability)
    {
        Debug.Log("hammer- AbilityAction");
        newHammer = Instantiate(IceHammer, spellOrigin.transform);
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.hammerSwing, gameObject);
    }

    public override IEnumerator Windup() //duration of the introduction decorations, followed by AbilityAction
    {
        healthRef.ObtainShield(100);
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.casting;
        weaponRef.SetDefaultBehaviourEnabled(true, false);
        yield return new WaitForSeconds(windupTime);
        AbilityAction();
        yield return new WaitForSeconds(castSlowDuration - windupTime);
        weaponRef.SetDefaultBehaviourEnabled(true, true);
        RemovePlayerCastMotion();
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.idle;
        healthRef.ObtainShield(-healthRef.GetShield());
    }

    public override void newDemonic()
    {
        
    }

    public override void EmergencyCancel()
    {
        GetNeededComponents();
        weaponRef.SetDefaultBehaviourEnabled(true, true);
        healthRef.ObtainShield(-healthRef.GetShield());
    }
}