using Coherence;
using Coherence.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikesTestSpell : Ability
{
    CoherenceSync sync;
    Rigidbody rb;
    AbilityInputSystem AbilityHoldRef;
    [SerializeField]
    SanitySystem sanityRef;
    PlayerMovement movementRef;
    [SerializeField]
    GameObject spellOrigin;

    [SerializeField]
    GameObject iceSpike;

    private void GetNeededComponents()
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
        StartCoroutine(Cooldown());
        sync.SendCommand<IceSpikesTestSpell>(nameof(CastSpikes), MessageTarget.All);
    }

    public override void DemonicStartAbility()
    {

    }

    public void CastSpikes()
    {
        Instantiate(iceSpike, spellOrigin.transform.position, spellOrigin.transform.rotation);
    }


    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(maxCooldownTime);
        onCooldown = false;
    }
}
