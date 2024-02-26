using Coherence.Toolkit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{

    protected CoherenceSync sync;
    protected Rigidbody rb;
    protected AbilityInputSystem AbilityHoldRef;
    protected SanitySystem sanityRef;
    protected PlayerMovement movementRef;
    [SerializeField]
    protected GameObject spellOrigin;

    [SerializeField]
    public int sanityCost;

    [HideInInspector]
    public bool onCooldown = false;
    public float maxCooldownTime;
    

    [SerializeField]
    public float activeTime { get; private set; } //how long the spell is doing 'it's thing', preventing other spells/shooting.

    

    public HUDSystem HUDRef;

    protected int tier;




    public void RecieveHUDReference(HUDSystem HUD,int tier)
    {
        HUDRef = HUD;
        this.tier = tier;
        HUDRef.SetCooldownForIcon(tier, maxCooldownTime);
    }

    public abstract void StartAbility();

    public abstract void DemonicStartAbility(); //new function > passing demonic bool. fite me

    protected abstract void GetNeededComponents();

    public abstract void newDemonic(); //if you become demonic while it is 'active'. only used by some though.

}
