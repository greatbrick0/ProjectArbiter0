using Coherence;
using Coherence.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using FMOD.Studio;
using FMODUnity;

public class PlayerSpellIceWeaponEnhancement : Ability
{

    //this spell reduces sanity in intervals while active.
    [SerializeField]
    private float sanityCostInterval;
    private float sanityCostTimer = 2.0f;



    //Weapon
    [SerializeField]
    WeaponData upgradedWeaponInfo;

    [SerializeField]
    WeaponData weaponStore;

    [SerializeField]
    GameObject gunVFX;

    [SerializeField]
    GameObject gunVFXRef;
    //muzzle
    [SerializeField]
    VisualEffect muzzleFlash;

    [SerializeField]
    Color newmuzzleColor;

    Color muzzleStore;

    bool enhancementActive = false;

    EventInstance iceEnhancementSound;

    public override void RecieveAbilityRequest()
    {
        Debug.Log("Enhancment spell cast recieved");
        GetNeededComponents();
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
        ApplyPlayerCastMotion(); //applies slow to player during casting.
        AbilityIntroductionDecorations();
        StartCoroutine("Windup");

    }

    public override void AbilityIntroductionDecorations()//usually the beginning of startAbility
    {
         animRef.SetTrigger("enhancement");
        //animation component
        //enhancement activate vfx
        //enhancement activate sfx
    }

    public override void AbilityAction()
    {
        if (!enhancementActive)
        {
            enhancementActive = true;
            Debug.Log("ApplyEnhancement");
            sanityCostTimer = sanityCostInterval;
            weaponRef.SetWeaponData(upgradedWeaponInfo);
            weaponRef.MaxOutAmmo();
            enhancementActive = true;
            //gunVFXRef = Instantiate(gunVFX, )
            //muzzleStore = muzzleFlash.GetColor("Color01");
            //  muzzleFlash.SetColor("Color01", newmuzzleColor);

            iceEnhancementSound = RuntimeManager.CreateInstance(FMODEvents.instance.iceEnhancement);
            RuntimeManager.AttachInstanceToGameObject(iceEnhancementSound, transform);
            iceEnhancementSound.start();
            iceEnhancementSound.release();
        }
        else
        {
            Debug.Log("RemoveEnhancement");
            enhancementActive = false;
            sanityCostTimer = sanityCostInterval;
            weaponRef.SetWeaponData(weaponStore);
            enhancementActive = false;
            
            //gunVFXRef.SetActive(false);
            //    muzzleFlash.SetColor("Color01", muzzleStore);

            iceEnhancementSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        RemovePlayerCastMotion();
    }

    void Update()
    {
        if (AbilityHoldRef != null)
        {
            if (AbilityHoldRef.demonic)
            {

            }
            else if (enhancementActive)
                sanityCostTimer -= Time.deltaTime;
            if (sanityCostTimer <= 0)
            {
                if (sanityRef != null)
                    sanityRef.Sanity -= sanityCost;
                sanityCostTimer = sanityCostInterval;
            }
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
        if (AbilityHoldRef.playerState<= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.casting;
        weaponRef.SetDefaultBehaviourEnabled(true, false);
        yield return new WaitForSeconds(windupTime);
        Debug.Log("Finished baseAbility internal Windup");
        AbilityAction();
        weaponRef.SetDefaultBehaviourEnabled(true, true);
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.idle;
    }

    public override void newDemonic()
    {
        Debug.Log("WeaponEnhancement disabled due to becoming demonic");
        if (enhancementActive)
            StartAbility();
    }

    public override void EmergencyCancel()
    {

        if (enhancementActive)
        {
            GetNeededComponents();
            AbilityAction();
        }
    }
}