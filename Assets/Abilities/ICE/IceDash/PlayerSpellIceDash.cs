using Coherence;
using Coherence.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellIceDash : Ability
{
    [SerializeField]
    float pauseDuration;

    [SerializeField]
    float backVelocity;
    [SerializeField]
    float forwardVelocity;
    [SerializeField]
    float slideDuration;

    [SerializeField]
    GameObject collideHitboxObject;

    GameObject collideHitboxRef;

    protected override void GetNeededComponents()
    { 
        AbilityHoldRef = GetComponent<AbilityInputSystem>();
        sanityRef = GetComponent<SanitySystem>();
        movementRef = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        sync = GetComponent<CoherenceSync>();
    }

    public override void StartAbility()
    {
        Debug.Log("StartedIceDashAbility");
        GetNeededComponents();
        movementRef.canJump = false;

        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.iceCharge, gameObject);

        sanityRef.Sanity -= sanityCost;
        HUDRef.UseAbility(tier);
        StartCoroutine(Cooldown());

        PhaseOneDash();

    }

    public override void DemonicStartAbility()
    {

    }

    public void PhaseOneDash()
    {

        movementRef.SetDefaultMovementEnabled(false);
        rb.drag = 3;
        rb.AddForce(-spellOrigin.transform.forward * backVelocity, ForceMode.Impulse);
        StartCoroutine(WindPause());
    }

    IEnumerator WindPause()
    {
        yield return new WaitForSeconds(pauseDuration);
        DashForward();

    }

    public void CreateVFX()
    {
        collideHitboxRef = Instantiate(collideHitboxObject, spellOrigin.transform);
        collideHitboxRef.GetComponent<DashHitBoxScipt>().dashAbilityRef = this;
    }

    public void DashForward()
    {
        rb.drag = 1;
        rb.AddForce(spellOrigin.transform.forward * forwardVelocity, ForceMode.Impulse);
        movementRef.SetPartialControl(0.1f);
        sync.SendCommand<PlayerSpellIceDash>(nameof(CreateVFX), MessageTarget.All);
        
        StartCoroutine(DurationDash());
    }

    public void EndDash()
    {
        movementRef.SetDefaultMovementEnabled(true);
        rb.drag = 0;
        sync.SendCommand<PlayerSpellIceDash>(nameof(DashFinished), MessageTarget.All);
       
    }

    public void DashFinished()
    {
        if (collideHitboxRef != null)
        collideHitboxRef.GetComponent<DashHitBoxScipt>().RequestDestroy();
    }
    IEnumerator DurationDash()
    {
        yield return new WaitForSeconds(slideDuration);
        EndDash();
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(maxCooldownTime);
        onCooldown = false;
    }

    public override void newDemonic()
    {

    }
}
