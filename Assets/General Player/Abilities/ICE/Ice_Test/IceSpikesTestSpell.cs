using Coherence;
using Coherence.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikesTestSpell : Ability
{
    

    [SerializeField]
    GameObject iceSpike;

    GameObject newSpike;

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
        sync.SendCommand<IceSpikesTestSpell>(nameof(CastSpikes), MessageTarget.All, false);
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

    public void CastSpikes(bool demonic)
    {
        newSpike = Instantiate(iceSpike, spellOrigin.transform.position, spellOrigin.transform.rotation);
        newSpike.GetComponent<IceShatter>().demonic = true;
    }


    IEnumerator Cooldown(bool demonic)
    {
        onCooldown = true;
        if (demonic)
        yield return new WaitForSeconds(maxCooldownTime/2);
        else
        yield return new WaitForSeconds(maxCooldownTime);
        onCooldown = false;
    }

    public override void newDemonic()
    {

    }
}
