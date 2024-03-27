using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PlayerSpellNatureGrapple : Ability
{
    [SerializeField]
    GameObject hookEnd;

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
        RaycastHit hit;
        if (Physics.Raycast(weaponRef.cam.transform.position,weaponRef.cam.transform.forward, out hit, maxDistance, GrappleTargetLayer))
        {
            Debug.Log("RaycastHit");
            doLine = true;
            grapplePoint = hit.point;
            ChangeControls();
            rb.AddForce((grapplePoint - transform.position).normalized * launchForce, ForceMode.Impulse);
            hookRef = Instantiate(hookEnd, grapplePoint,Quaternion.identity);
            hookRef.transform.LookAt(hookRef.transform.position + hit.normal);
        }
    }

    public void ChangeControls()
    {
        motionVFXRef = Instantiate(dashMotionVFX, spellOrigin.transform);
        movementRef.SetEnabledControls(false, true);
        //GetComponentInParent<PlayerMovement>().SetEnabledControls(false, true);
        //GetComponentInParent<PlayerMovement>().SetPartialControl(0.1f);
        Invoke("ReturnControls", 2.0f);
    }

    public void ReturnControls()
    {
        Destroy(motionVFXRef.gameObject);
        movementRef.SetEnabledControls(true, true);
        //GetComponentInParent<PlayerMovement>().SetEnabledControls(true, true);
        doLine = false;
    }

    private void LateUpdate()
    {
        if (doLine)
        {
            rawLine.SetPosition(0, new Vector3(spellOrigin.transform.position.x,spellOrigin.transform.position.y-0.3f,spellOrigin.transform.position.z));
            rawLine.SetPosition(1, hookRef.transform.position);
        }
        else
        {
            rawLine.SetPosition(0, Vector3.zero);
            rawLine.SetPosition(1, Vector3.zero);
        }

    }
    public virtual IEnumerator Windup() //duration of the introduction decorations, followed by AbilityAction
    {
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.casting;
        yield return new WaitForSeconds(windupTime);
        doLine = true;
        AbilityAction();
        RemovePlayerCastMotion();
        movementRef.gravityEnabled = false;
        yield return new WaitForSeconds(0.3f);
        movementRef.gravityEnabled = true;
        yield return new WaitForSeconds(castSlowDuration - windupTime -0.3f);
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.idle;

    }

    public override void newDemonic() { }



}
