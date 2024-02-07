using Coherence.Toolkit;
using Coherence;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellHammerSwing : Ability
{


    [SerializeField]
    GameObject IceHammer;

    GameObject newHammer;

    protected override void GetNeededComponents()
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
        HUDRef.SetCooldownForIcon(tier, maxCooldownTime);

        sanityRef.Sanity -= sanityCost;
        HUDRef.UseAbility(tier);
        StartCoroutine(Cooldown(false));
        sync.SendCommand<PlayerSpellHammerSwing>(nameof(BeginSwing), MessageTarget.All);
    }

    public override void DemonicStartAbility()
    {
        Debug.Log("DemonicVarient-StartIceSpike");
        GetNeededComponents();
        HUDRef.SetCooldownForIcon(tier, maxCooldownTime / 2);

        //Skip sanitycost, you are DEMONIC!!
        HUDRef.UseAbility(tier);
        StartCoroutine(Cooldown(true));
    }

    public void BeginSwing()
    {
        newHammer = Instantiate(IceHammer, spellOrigin.transform);
        
    }


    IEnumerator Cooldown(bool demonic)
    {
        onCooldown = true;
        if (demonic)
            yield return new WaitForSeconds(maxCooldownTime / 2);
        else
            yield return new WaitForSeconds(maxCooldownTime);
        onCooldown = false;
    }

    public override void newDemonic()
    {

    }
}
