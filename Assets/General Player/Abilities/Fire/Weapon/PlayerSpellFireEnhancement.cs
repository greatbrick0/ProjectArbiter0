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

    float storebaseMovementSpeed;

    bool enhancementActive = false;



    public override void RecieveAbilityRequest()
    {
        Debug.Log("Enhancment spell cast recieved");
        GetNeededComponents();

        sanityRef.Sanity -= sanityCost;
        if (GetComponent<PlayerInput>().authority)
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
        weaponRef.MaxOutAmmo();
        storebaseMovementSpeed = movementRef.GetMaxMoveSpeed();
            stimBoostTimer = castSlowDuration *2;
        lerpTime = 0;
            movementRef.ApplyExternalSpeedModification(stimBoostValue);
        Debug.Log(movementRef.GetMaxMoveSpeed());
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
                    if (movementRef.GetMaxMoveSpeed() > storebaseMovementSpeed)
                    {
                        Debug.Log(movementRef.GetMaxMoveSpeed());
                        movementRef.SetMoveSpeed(Mathf.Lerp(movementRef.GetMaxMoveSpeed(), storebaseMovementSpeed, lerpTime));
                    }
                }
            }
            else
            {
                enhancementActive = false;
                Destroy(motionVFXRef.gameObject);
                ReturnNormalWeapon();
                movementRef.SetMoveSpeed(storebaseMovementSpeed);
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