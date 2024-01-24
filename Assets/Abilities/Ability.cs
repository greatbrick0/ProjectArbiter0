using Coherence.Toolkit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{

    [Header("References")]
    public CoherenceSync sync;
    public Rigidbody rb;
    public AbilityInputSystem AbilityHoldRef;
    [SerializeField]
    public SanitySystem sanityRef;
    public PlayerMovement movementRef;
    [SerializeField]
    public GameObject spellOrigin;

    [SerializeField]
    public float maxCooldownTime;

    public bool onCooldown = false;

    [SerializeField]
    public float activeTime { get; private set; } //how long the spell is doing 'it's thing', preventing other spells/shooting.

    public bool isActive = false;

    [SerializeField]
    public int sanityCost;

    public HUDSystem HUDRef;

    public int tier;




    public void RecieveHUDReference(HUDSystem HUD,int tier)
    {
        HUDRef = HUD;
        this.tier = tier;
        HUDRef.SetCooldownForIcon(tier, maxCooldownTime);
    }

    public abstract void StartAbility();

    public abstract void DemonicStartAbility(); //new function > passing demonic bool. fite me

}
