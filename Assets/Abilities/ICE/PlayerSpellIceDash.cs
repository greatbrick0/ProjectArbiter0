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

    private void GetNeededComponents()
    { 
        AbilityHoldRef = GetComponent<AbilityInputSystem>();
        sanityRef = GetComponent<SanitySystem>();
        movementRef = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        sync = GetComponent<CoherenceSync>();
    }

    public override void StartAbility()
    {
        Debug.Log("StartedAbility");
        GetNeededComponents();
        movementRef.canJump = false;

        sanityRef.Sanity -= sanityCost;
        HUDRef.UseAbility(tier);
        StartCoroutine(Cooldown());

        sync.SendCommand<PlayerSpellIceDash>(nameof(PhaseOneDash), MessageTarget.All);

    }

    public override void DemonicStartAbility()
    {

    }

    public void PhaseOneDash()
    {
        Debug.Log("BeginPause");
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

    public void DashForward()
    {
        rb.drag = 1;
        rb.AddForce(spellOrigin.transform.forward * forwardVelocity, ForceMode.Impulse);
        movementRef.SetPartialControl(0.1f);
        //Instantiate(collideHitboxObject, spellOrigin.transform);
        StartCoroutine(EndDash());
    }

    IEnumerator EndDash()
    {
        yield return new WaitForSeconds(slideDuration);
        rb.drag = 0;
        movementRef.SetDefaultMovementEnabled(true);
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(maxCooldownTime);
        onCooldown = false;
    }

}
