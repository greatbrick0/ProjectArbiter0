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

    protected override void GetNeededComponents()
    {
        AbilityHoldRef = GetComponent<AbilityInputSystem>();
        sanityRef = GetComponent<SanitySystem>();
        movementRef = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        sync = GetComponent<CoherenceSync>();
        weaponRef = GetComponent<WeaponHolder>();
    }


    public override void RecieveAbilityRequest()
    {
        Debug.Log("StartedHammerSwing");
        GetNeededComponents();
        HUDRef.SetCooldownForIcon(tier, maxCooldownTime);



        sanityRef.Sanity -= sanityCost;
        HUDRef.UseAbility(tier);
        StartCoroutine(Cooldown(false));

        sync.SendCommand<PlayerSpellHammerSwing>(nameof(StartAbility), MessageTarget.All);
    }

    public override void RecieveDemonicAbilityRequest()
    {
        Debug.Log("DemonicVarient-HammerSwing-UNIMPLEMENTED");
        GetNeededComponents();
        HUDRef.SetCooldownForIcon(tier, maxCooldownTime / 2);
    }

    public override void StartAbility()
    {
        CastSlow(); //applies slow to player during casting.
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
        yield return new WaitForSeconds(1.0f);
        weaponRef.SetDefaultBehaviourEnabled(true, true);
    }

    public override void newDemonic()
    {

    }
}