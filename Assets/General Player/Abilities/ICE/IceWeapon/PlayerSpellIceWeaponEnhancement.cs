using Coherence;
using Coherence.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerSpellIceWeaponEnhancement : Ability
{

    //this spell reduces sanity in intervals while active.
    [SerializeField]
    private float sanityCostInterval;
    private float sanityCostTimer = 2.0f;



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
            //gunVFXRef.SetActive(false);
            //    muzzleFlash.SetColor("Color01", muzzleStore);
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
        weaponRef.SetDefaultBehaviourEnabled(true, false);
        yield return new WaitForSeconds(windupTime);
        Debug.Log("Finished baseAbility internal Windup");
        AbilityAction();
        weaponRef.SetDefaultBehaviourEnabled(true, true);
    }

    public override void newDemonic()
    {
        Debug.Log("WeaponEnhancement disabled due to becoming demonic");
        if (enhancementActive)
            StartAbility();
    }
}