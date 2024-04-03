using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PlayerSpellNatureSpikes : Ability
{
    [SerializeField]
    GameObject spikeSeed;

    GameObject seedRef;

    [SerializeField]
    float launchForce;

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
        throw new System.NotImplementedException();
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
        Debug.Log("AbilityAction");
            seedRef = Instantiate(spikeSeed, spellOrigin.transform.position, Quaternion.identity);
            seedRef.GetComponent<SpikeSeedLogic>().playerSpikeRef = this;

        
    }

    

    

    public virtual IEnumerator Windup() //duration of the introduction decorations, followed by AbilityAction
    {
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.casting;
        yield return new WaitForSeconds(windupTime);
        AbilityAction();
        RemovePlayerCastMotion();
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.idle;
    }

    public override void newDemonic() { }


}
