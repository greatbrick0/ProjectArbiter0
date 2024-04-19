using Coherence;
using Coherence.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerSpellNatureEnhancement : Ability
{

    //this spell reduces sanity in intervals while active.
    [SerializeField]
    private float sanityCostInterval;
    private float sanityCostTimer = 2.0f;


    [SerializeField]
    private GameObject Railbeam;


    private GameObject RailRef;
    //Weapon
    [SerializeField]
    WeaponData upgradedWeaponInfo;

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

    bool inWindup = false;

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
        animRef.SetTrigger("Enhance");
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
            weaponStore = weaponRef.GetWeaponData();
            weaponRef.SetWeaponData(upgradedWeaponInfo);
            enhancementActive = true;
            weaponRef.enhancedMuzzleFlash.gameObject.SetActive(true);
            weaponRef.enhanced = true;
            weaponRef.shotEvent += Rail;
            //gunVFXRef = Instantiate(gunVFX, )
            //muzzleStore = muzzleFlash.GetColor("Color01");
            //  muzzleFlash.SetColor("Color01", newmuzzleColor);
        }
        else
        {
            Debug.Log("RemoveEnhancement");
            enhancementActive = false;
            sanityCostTimer = sanityCostInterval;
            weaponRef.SetWeaponData(weaponStore);
            enhancementActive = false;
            weaponRef.enhancedMuzzleFlash.gameObject.SetActive(false);
            weaponRef.enhanced = false;
            weaponRef.shotEvent -= Rail;
            //gunVFXRef.SetActive(false);
            //    muzzleFlash.SetColor("Color01", muzzleStore);
        }
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

    void Rail(RaycastHit h)
    {
        Debug.Log("rail");
        RailRef = Instantiate(Railbeam, h.point, Quaternion.identity);
        Debug.Log(movementRef.transform.position);
        Debug.Log("Spike"+RailRef.transform.position);
        RailRef.transform.LookAt(RailRef.transform.position - (inputRef.head.transform.position - RailRef.transform.position));
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(maxCooldownTime);
        onCooldown = false;
    }

    public override IEnumerator Windup() //duration of the introduction decorations, followed by AbilityAction
    {
        inWindup = true;
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.natureEnhancement, gameObject);

        if (AbilityHoldRef.playerState<= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.casting;
        weaponRef.SetDefaultBehaviourEnabled(true, false);
        yield return new WaitForSeconds(windupTime);
        Debug.Log("Finished baseAbility internal Windup");
        AbilityAction();
        weaponRef.SetDefaultBehaviourEnabled(true, true,true);
        if (AbilityHoldRef.playerState <= AbilityInputSystem.CastingState.casting)
            AbilityHoldRef.playerState = AbilityInputSystem.CastingState.idle;
        inWindup = false;
        RemovePlayerCastMotion();
    }

    public override void newDemonic()
    {
        Debug.Log("WeaponEnhancement disabled due to becoming demonic");
        if (inWindup)
        {
            RemovePlayerCastMotion();
            StopCoroutine(nameof(Windup));
            weaponRef.SetDefaultBehaviourEnabled(true, true, true);
            Debug.Log("inWindup");
        }
        if (enhancementActive)
            AbilityAction();
    }

    public override void EmergencyCancel()
    {
        if (enhancementActive)
        {
            AbilityAction();
        }
    }
}