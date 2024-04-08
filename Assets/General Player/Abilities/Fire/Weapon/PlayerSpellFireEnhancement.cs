using Coherence;
using Coherence.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerSpellFireEnhancement : Ability
{
    [SerializeField]
    GameObject dashMotionVFX;

    GameObject motionVFXRef;

    [SerializeField]
    WeaponData upgradedWeaponInfo;

    WeaponData weaponStore;
    
    [SerializeField]
    GameObject gunVFXRef;
    //muzzle
    [SerializeField]
    VisualEffect muzzleFlash;

    [SerializeField]
    Color newmuzzleColor;

    Color muzzleStore;

    [SerializeField]
    float stimBoostValue;

    float stimBoostTimer;
    float lerpTime;


    bool enhancementActive = false;



    public override void RecieveAbilityRequest()
    {
        Debug.Log("Enhancment spell cast recieved");
        GetNeededComponents();

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

    public override void AbilityIntroductionDecorations()//usually the beginning of startAbility
    {
        //animation component
        animRef.SetTrigger("Enhance");
        //enhancement activate vfx
        //enhancement activate sfx
    }

    public override void AbilityAction()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.fireEnhancement, gameObject);
            Debug.Log("ApplyFireStim");
        Debug.Log(movementRef.GetMaxMoveModify());
            weaponStore = weaponRef.GetWeaponData();
            weaponRef.SetWeaponData(upgradedWeaponInfo);
            enhancementActive = true;
        weaponRef.MaxOutAmmo();

            stimBoostTimer = castSlowDuration *2;
        lerpTime = 0;
            movementRef.ApplyExternalSpeedModification(stimBoostValue);
        Debug.Log(movementRef.GetMaxMoveModify());
        motionVFXRef = Instantiate(dashMotionVFX, spellOrigin.transform);
    }

    void Update()
    {
        if (enhancementActive)
        {

            if (stimBoostTimer > 0)
            {

                stimBoostTimer -= Time.deltaTime;
                if (stimBoostTimer < castSlowDuration)
                {

                    lerpTime += Time.deltaTime;
                    if (lerpTime > 1)
                        lerpTime = 1;
                    movementRef.ApplyExternalSpeedModification(-0.01f);
                    if (movementRef.GetMaxMoveModify() > 0)
                    {
                        movementRef.SetMoveModify(Mathf.Lerp(movementRef.GetMaxMoveModify(), 0, lerpTime));
                    }
                }
            }
            else
            {
                
                ReturnNormalWeapon();
                
            }
        }
        
    }

    public void ReturnNormalWeapon()
    {
        weaponRef.SetWeaponData(weaponStore);
        enhancementActive = false;
                if (motionVFXRef != null)
                Destroy(motionVFXRef.gameObject);
        movementRef.SetMoveModify(0);
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
        weaponRef.SetDefaultBehaviourEnabled(true, false);
        AbilityAction();
        yield return new WaitForSeconds(0.5f);
        weaponRef.SetDefaultBehaviourEnabled(true, true);
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.idle;

    }

    public override void newDemonic()
    {
       /* Debug.Log("WeaponEnhancement disabled due to becoming demonic");
        if (enhancementActive)
            StartAbility(); */
    }

    public override void EmergencyCancel()
    {
        GetNeededComponents();
        if (enhancementActive)
        ReturnNormalWeapon();
    }
}