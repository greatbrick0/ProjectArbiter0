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
    WeaponHolder weaponRef;

    [SerializeField]
    WeaponData upgradedWeaponInfo;

    WeaponData weaponStore;
    
    //muzzle
    [SerializeField]
    VisualEffect muzzleFlash;

    [SerializeField]
    Color newmuzzleColor;

    Color muzzleStore;

    bool enhancementActive;
   
    

    

    protected override void GetNeededComponents()
    {
        AbilityHoldRef = GetComponent<AbilityInputSystem>();
        sanityRef = GetComponent<SanitySystem>();
        movementRef = GetComponent<PlayerMovement>();
        sync = GetComponent<CoherenceSync>();
        weaponRef = GetComponent<WeaponHolder>();
        
        
    }


    public override void StartAbility()
    {
        Debug.Log("Enhancment spell cast recieved");
        GetNeededComponents();

        HUDRef.SetCooldownForIcon(tier, maxCooldownTime);

        if (!enhancementActive)
        sync.SendCommand<PlayerSpellIceWeaponEnhancement>(nameof(ApplyEnhancement), MessageTarget.All);
        else
        sync.SendCommand<PlayerSpellIceWeaponEnhancement>(nameof(RemoveEnhancement), MessageTarget.All);
       
        
        
        

        
        HUDRef.UseAbility(tier);
        StartCoroutine(Cooldown());
        
    }

    public override void DemonicStartAbility()
    {

    }

    public void ApplyEnhancement()
    {
        Debug.Log("ApplyEnhancement");
        sanityCostTimer = sanityCostInterval;
        weaponStore = weaponRef.GetWeaponData();
        weaponRef.SetWeaponData(upgradedWeaponInfo);
        enhancementActive = true;
        //muzzleStore = muzzleFlash.GetColor("Color01");
      //  muzzleFlash.SetColor("Color01", newmuzzleColor);
    }

    public void RemoveEnhancement()
    {
        sanityCostTimer = sanityCostInterval;
        weaponRef.SetWeaponData(weaponStore);
        enhancementActive = false;
    //    muzzleFlash.SetColor("Color01", muzzleStore);
    }

    void Update()
    {
        if (enhancementActive)
            sanityCostTimer -= Time.deltaTime;
        if (sanityCostTimer <= 0)
        {
            if (sanityRef != null)
            sanityRef.Sanity -= sanityCost;
            sanityCostTimer = sanityCostInterval;
        }
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(maxCooldownTime);
        onCooldown = false;
    }
}
