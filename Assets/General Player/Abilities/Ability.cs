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
    protected Animator animRef;

    [HideInInspector]
    public GameObject playerRef;


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

    public int tier { get; protected set; }




    public void RecieveHUDReference(HUDSystem HUD, int tier)
    {
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
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.casting;
        yield return new WaitForSeconds(windupTime);
        AbilityAction();
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.idle;
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
        Transform t = transform.parent.parent.parent;
        AbilityHoldRef = t.GetComponent<AbilityInputSystem>();
        sanityRef = t.GetComponent<SanitySystem>();
        movementRef = t.GetComponent<PlayerMovement>();
        rb = t.GetComponent<Rigidbody>();
        weaponRef = t.GetComponent<WeaponHolder>();
        spellOrigin = transform.parent.gameObject;
        animRef = movementRef.nudger.GetComponent<Animator>();
    }

    public abstract void newDemonic(); //if you become demonic while it is 'active'. only used by some though.

    public bool HasAuthority()
    {
        return playerRef.GetComponent<PlayerInput>().authority;
    }
}