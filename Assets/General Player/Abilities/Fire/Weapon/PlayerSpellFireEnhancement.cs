using Coherence;
using Coherence.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerSpellFireEnhancement : Ability
{

    
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

    float storebaseMovementSpeed;

    bool enhancementActive = false;



    public override void RecieveAbilityRequest()
    {
        Debug.Log("Enhancment spell cast recieved");
        GetNeededComponents();

        sanityRef.Sanity -= sanityCost;
        HUDRef.SetCooldownForIcon(tier, maxCooldownTime);
        HUDRef.UseAbility(tier);
        StartCoroutine(Cooldown());

        StartAbility();

    }

    public override void RecieveDemonicAbilityRequest()
    {

    }

    public override void StartAbility()
    {
        AbilityIntroductionDecorations();
        AbilityAction();
    }

    public override void AbilityIntroductionDecorations()//usually the beginning of startAbility
    {
        //animation component
        //enhancement activate vfx
        //enhancement activate sfx
    }

    public override void AbilityAction()
    {
            Debug.Log("ApplyFireStim");
        Debug.Log(movementRef.GetMaxMoveSpeed());
            weaponStore = weaponRef.GetWeaponData();
            weaponRef.SetWeaponData(upgradedWeaponInfo);
            enhancementActive = true;
        storebaseMovementSpeed = movementRef.GetMaxMoveSpeed();
            stimBoostTimer = castSlowDuration *2;
            movementRef.ApplyExternalSpeedModification(stimBoostValue);
        Debug.Log(movementRef.GetMaxMoveSpeed());
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
                    movementRef.ApplyExternalSpeedModification(-0.01f);
                    if (movementRef.GetMaxMoveSpeed() < storebaseMovementSpeed)
                    {
                        movementRef.ApplyExternalSpeedModification
                    }
                }
            }
        }
        
    }

    public void ReturnNormalWeapon()
    {
        weaponRef.SetWeaponData(weaponStore);
        enhancementActive = false;
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(maxCooldownTime);
        onCooldown = false;
    }


    public override void newDemonic()
    {
       /* Debug.Log("WeaponEnhancement disabled due to becoming demonic");
        if (enhancementActive)
            StartAbility(); */
    }
}