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
        HUDRef.SetCooldownForIcon(tier, maxCooldownTime);



        sanityRef.Sanity -= sanityCost;
        HUDRef.UseAbility(tier);
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
        //Hammer windup animations
        //hammer windup sfx
    }

    public override void AbilityAction() //begins at end of Windup (base ability)
    {
        Debug.Log("hammer- AbilityAction");
        newHammer = Instantiate(IceHammer, spellOrigin.transform);
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.iceSpikes, gameObject);
    }

    public override IEnumerator Windup() //duration of the introduction decorations, followed by AbilityAction
    {
        weaponRef.SetDefaultBehaviourEnabled(true, false);
        yield return new WaitForSeconds(windupTime);
        AbilityAction();
        yield return new WaitForSeconds(castSlowDuration - windupTime);
        weaponRef.SetDefaultBehaviourEnabled(true, true);
        RemovePlayerCastMotion();
    }

    public override void newDemonic()
    {

    }
}