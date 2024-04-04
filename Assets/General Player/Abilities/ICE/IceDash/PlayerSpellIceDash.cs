using Coherence;
using Coherence.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using FMODUnity;
using FMOD.Studio;
using Unity.VisualScripting;
using JetBrains.Annotations;
using Coherence.Runtime;

public class PlayerSpellIceDash : Ability
{
    [SerializeField]
    float backVelocity;

    bool shouldRepeatAction = false;
    [SerializeField]
    float repeatActionInterval;
    [SerializeField]
    float speedMax;
    float actionIntervalTimer;

    [SerializeField]
    float slideDuration;

    

    [SerializeField]
    GameObject collideHitboxObject;

    GameObject collideHitboxRef;

    EventInstance dashSoundInstance;

    bool cancellable;
    public override void RecieveAbilityRequest()
    {
        Debug.Log("StartedIceDashAbility");
        GetNeededComponents();
        movementRef.canJump = false;



        sanityRef.Sanity -= sanityCost;
        if (HasAuthority())
        {
            HUDRef.SetCooldownForIcon(tier, maxCooldownTime);
            HUDRef.UseAbility(tier);
        }
        StartCoroutine(Cooldown());

        StartAbility();

    }

    public override void RecieveDemonicAbilityRequest()
    {

    }




    public override void StartAbility()
    {
        AbilityIntroductionDecorations();
        StartCoroutine(Windup());
    }
    public override void AbilityIntroductionDecorations()
    {
        animRef.SetTrigger("Charge");
        movementRef.SetEnabledControls(false, true);
        sanityRef.GetComponent<PlayerInput>().mouseXSens *= 0.3f;
        rb.drag = 3;
        rb.AddForce(-(spellOrigin.transform.forward * backVelocity + (-spellOrigin.transform.up * backVelocity / 5)), ForceMode.Impulse);

    }
    
    public override void AbilityAction()
    {
        cancellable = false;

        dashSoundInstance = RuntimeManager.CreateInstance(FMODEvents.instance.iceDashBeginning);
        RuntimeManager.AttachInstanceToGameObject(dashSoundInstance, transform);
        dashSoundInstance.start();
        dashSoundInstance.release();
        healthRef.ObtainShield(100);
        rb.drag = 0;
        shouldRepeatAction = true;
        actionIntervalTimer = repeatActionInterval;

        //rb.AddForce(spellOrigin.transform.forward * forwardVelocity, ForceMode.Impulse);
        //movementRef.SetPartialControl(0.1f);
        collideHitboxRef = Instantiate(collideHitboxObject, spellOrigin.transform);
        collideHitboxRef.GetComponent<DashHitBoxScipt>().dashAbilityRef = this;
        collideHitboxRef.GetComponent<DashHitBoxScipt>().lifespan = slideDuration;
    }

    private void Update()
    {
        if (shouldRepeatAction)
        {
            if (cancellable && inputRef.GetJumpInput())
            {
                EndDash(false);
            }
            if (actionIntervalTimer <= 0)
            {
                Vector3 direction = new Vector3(gameObject.transform.forward.x, -0.25f, gameObject.transform.forward.z);
                rb.AddForce(direction * 8, ForceMode.Impulse);
                if (rb.velocity.magnitude > speedMax)

                    rb.velocity = Vector3.ClampMagnitude(rb.velocity, speedMax);
                actionIntervalTimer = repeatActionInterval;
            }
            else
            {
                actionIntervalTimer -= Time.deltaTime;
            }
        }
    }

    public void EndDash(bool collide)
    {
        dashSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        RuntimeManager.PlayOneShotAttached(FMODEvents.instance.iceDashEnd, gameObject);


        Debug.Log("Dash completed");
        movementRef.SetEnabledControls(true, true);
        shouldRepeatAction = false;
        actionIntervalTimer = repeatActionInterval;
        rb.drag = 0;
        EndShield();
        sanityRef.GetComponent<PlayerInput>().mouseXSens /= 0.3f;
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.idle;
        if (collideHitboxRef != null)
            collideHitboxRef.GetComponent<DashHitBoxScipt>().RequestDestroy();
        if (collide)
        {
            Debug.Log("Collision Caused Dash to End");
            rb.AddForce(-(spellOrigin.transform.forward * backVelocity + (-spellOrigin.transform.up * backVelocity / 5)), ForceMode.Impulse);
            animRef.Play("Base Layer.IceCharge4", 0);
        }
        else
        {
            Debug.Log("Dash Expired Naturally");
            animRef.Play("Base Layer.IceCharge3", 0);
        }
    }   

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(maxCooldownTime);
        onCooldown = false;
    }

    public override IEnumerator Windup() //duration of the introduction decorations, followed by AbilityAction
    {
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.casting;
        yield return new WaitForSeconds(windupTime);
        AbilityAction();
        yield return new WaitForSeconds(1f);
        cancellable = true;

    }

    public void EndShield()
    {
        healthRef.ObtainShield(-healthRef.GetShield());
    }

    public override void newDemonic()
    {

    }
}
