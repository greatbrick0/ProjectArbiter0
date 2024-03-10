using Coherence.Toolkit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{

    protected Rigidbody rb;
    protected AbilityInputSystem AbilityHoldRef;
    protected SanitySystem sanityRef;
    protected PlayerMovement movementRef;
    protected WeaponHolder weaponRef;
    protected GameObject spellOrigin;

    [SerializeField]
    public int sanityCost;

    [HideInInspector]
    public bool onCooldown = false;
    public float maxCooldownTime;

    [SerializeField]
    protected float windupTime;
    [SerializeField]
    protected float castSlowDuration;

    [SerializeField]
    public float activeTime { get; private set; } //how long the spell is doing 'it's thing', preventing other spells/shooting.


    [HideInInspector]
    public HUDSystem HUDRef;

    protected int tier;




    public void RecieveHUDReference(HUDSystem HUD, int tier)
    {
        Debug.Log("testmessage-RecieveHUDReference");
        HUDRef = HUD;
        this.tier = tier;
        HUDRef.SetCooldownForIcon(tier, maxCooldownTime);
    }

    public abstract void RecieveAbilityRequest();

    public abstract void RecieveDemonicAbilityRequest(); //new function > passing demonic bool. fite me

    public abstract void StartAbility(); //begins the spell itself, what is portrayed to all players. 
    public abstract void AbilityIntroductionDecorations();//usually the beginning of startAbility, also calls Windup for it's duration

    public virtual IEnumerator Windup() //duration of the introduction decorations, followed by AbilityAction
    {
        yield return new WaitForSeconds(windupTime);
        AbilityAction();
    }

    public virtual void ApplyPlayerCastMotion()
    {
        movementRef.ApplyExternalSpeedModification(AbilityHoldRef.castSlowAmount);
    }

    public virtual void RemovePlayerCastMotion()
    {
        movementRef.ApplyExternalSpeedModification(-AbilityHoldRef.castSlowAmount);
    }

    public virtual IEnumerator Cooldown(bool demonic)
    {
        onCooldown = true;
        if (demonic)
            yield return new WaitForSeconds(maxCooldownTime / 2);
        else
            yield return new WaitForSeconds(maxCooldownTime);
        onCooldown = false;
    }

    public virtual IEnumerator WholeSpellDuration()
    {
        AbilityHoldRef.playerState = AbilityInputSystem.CastingState.casting;
        yield return new WaitForSeconds(activeTime);
    }
    public abstract void AbilityAction(); //the 'fun' part of the spell

    protected virtual void GetNeededComponents()
    {
        AbilityHoldRef = transform.parent.parent.parent.parent.GetComponent<AbilityInputSystem>();
        sanityRef = transform.parent.parent.parent.parent.GetComponent<SanitySystem>();
        movementRef = transform.parent.parent.parent.parent.GetComponent<PlayerMovement>();
        rb = transform.parent.parent.parent.parent.GetComponent<Rigidbody>();
        weaponRef = transform.parent.parent.parent.parent.GetComponent<WeaponHolder>();
        spellOrigin = transform.parent.gameObject;
    }

    public abstract void newDemonic(); //if you become demonic while it is 'active'. only used by some though.

}