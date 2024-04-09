using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PlayerSpellNatureGrapple : Ability
{
    [SerializeField]
    GameObject hookEnd;

    [SerializeField]
    GameObject hookRef;

    public LayerMask GrappleTargetLayer;

    [SerializeField]
    GameObject dashMotionVFX;

    GameObject motionVFXRef;


    Vector3 grapplePoint;

    [SerializeField]
    float maxDistance;
    [SerializeField]
    float launchForce;

    [SerializeField]
    LineRenderer rawLine;

    bool doLine = false; //should we be rendering line

    bool cancellable;
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
        animRef.SetTrigger("Vine");
    }

    public override void AbilityAction()
    {
        Debug.Log("AbilityAction");
        RaycastHit hit;
        if (Physics.Raycast(inputRef.head.transform.position, inputRef.head.transform.forward, out hit, maxDistance, GrappleTargetLayer))
        {
            Debug.Log("RaycastHit");
            doLine = true;
            grapplePoint = hit.point;
            hookRef = Instantiate(hookEnd, spellOrigin.transform.position, Quaternion.identity);
            hookRef.GetComponent<HookLogic>().target = grapplePoint;
            hookRef.GetComponent<HookLogic>().grappleSpellRef = this;
            hookRef.transform.LookAt(hookRef.transform.position + hit.normal);
        }
    }

    public void Launch()
    {
        ChangeControls();
        rb.velocity = Vector3.zero;
        rb.AddForce((grapplePoint - transform.position).normalized * launchForce, ForceMode.Impulse);
    }


    public void ChangeControls()
    {
        motionVFXRef = Instantiate(dashMotionVFX, spellOrigin.transform);
        movementRef.SetEnabledControls(false, true);
        //GetComponentInParent<PlayerMovement>().SetEnabledControls(false, true);
        Invoke("SetCancellableTrue", 1.0f);
        //GetComponentInParent<PlayerMovement>().SetPartialControl(0.1f);
        Invoke("ReturnControls", 2.0f);
    }

    public void ReturnControls()
    {
        if (motionVFXRef != null) Destroy(motionVFXRef.gameObject);
        movementRef.SetEnabledControls(true, true);
        //GetComponentInParent<PlayerMovement>().SetEnabledControls(true, true);
        doLine = false;
    }

    private void LateUpdate()
    {
        if (doLine)
        {
            rawLine.enabled = true;
            rawLine.widthMultiplier = 0.5f;
            rawLine.SetPosition(0, new Vector3(spellOrigin.transform.position.x,spellOrigin.transform.position.y-0.5f,spellOrigin.transform.position.z));
            rawLine.SetPosition(1, hookRef.transform.position);
        }
        else
        {
            rawLine.enabled = false;
            rawLine.SetPosition(0, Vector3.zero);
            rawLine.SetPosition(1, Vector3.zero);
            rawLine.widthMultiplier = 0;
        }

        if (cancellable)
        {
            if (movementRef.grounded)
            {
                ReturnControls();
                doLine = false;
                cancellable = false;
            }
        }

    }
    public virtual IEnumerator Windup() //duration of the introduction decorations, followed by AbilityAction
    {
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.casting;
        weaponRef.SetDefaultBehaviourEnabled(true, false);
        yield return new WaitForSeconds(windupTime);
        doLine = true;
        AbilityAction();
        RemovePlayerCastMotion();
        movementRef.gravityEnabled = false;
        yield return new WaitForSeconds(0.3f);
        movementRef.gravityEnabled = true;
        yield return new WaitForSeconds(castSlowDuration - windupTime -0.3f);
        yield return new WaitForSeconds(0.2f);
        weaponRef.SetDefaultBehaviourEnabled(true, true, true);
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.idle;

    }

    public void SetCancellableTrue()
    {
        cancellable = true;
    }
    public override void newDemonic() { }

    void GetCaast()
    {
        RaycastHit hit;
        if (Physics.Raycast(weaponRef.cam.transform.position, weaponRef.cam.transform.forward, out hit, maxDistance, GrappleTargetLayer))
        {
            Debug.Log("RaycastHit");
            doLine = true;
            grapplePoint = hit.point;
            ChangeControls();
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce((grapplePoint - transform.position).normalized * launchForce, ForceMode.Impulse);
            hookRef = Instantiate(hookEnd, spellOrigin.transform.position, Quaternion.identity);
            hookRef.GetComponent<HookLogic>().target = grapplePoint;
            hookRef.transform.LookAt(hookRef.transform.position + hit.normal);
        }
    }

    public override void EmergencyCancel()
    {
        GetNeededComponents();
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.idle;

        weaponRef.SetDefaultBehaviourEnabled(true, true,true);
        ReturnControls();
        doLine = false;
    }

}
