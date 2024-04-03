using Coherence;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellFireBomb : Ability
{
    [SerializeField]
    GameObject spellBomb;

    [SerializeField]
    GameObject dashMotionVFX;

    GameObject motionVFXRef;

    
    [HideInInspector]
    public GameObject bombRef;
    float bombActiveTimer;
    public bool bombActive;

    bool cancellable;
    public override void RecieveAbilityRequest()
    {

        if (bombActive )
        {
            if (bombActiveTimer <= 2.5)
                bombRef.GetComponent<BombLogic>().Trigger();

            return;
        }
        else
        Debug.Log("StartedFireBomb");
        GetNeededComponents();
        if (HasAuthority())
        {
            HUDRef.SetCooldownForIcon(tier, 0.5f);
            HUDRef.UseAbility(tier);
        }



        sanityRef.Sanity -= sanityCost;
        //StartCoroutine(Cooldown(false));

        StartAbility();
    }

    public override void RecieveDemonicAbilityRequest()
    {
        if (bombActive)
        {
            if (bombActiveTimer <= 2.5)
                bombRef.GetComponent<BombLogic>().Trigger();

            return;
        }
        Debug.Log("DemonicVarient-FireBomb-UNIMPLEMENTED");
    }

    public override void StartAbility()
    {
        //ApplyPlayerCastMotion(); //applies slow to player during casting.
        AbilityIntroductionDecorations();
        StartCoroutine(Windup());
    }

    public override void AbilityIntroductionDecorations()
    {
        //Hammer windup animations
        //hammer windup sfx
    }

    public override void AbilityAction() //begins at end of Windup (base ability)
    {
            bombRef = Instantiate(spellBomb, spellOrigin.transform.position, spellOrigin.transform.rotation);
            bombRef.GetComponent<BombLogic>().bombAbilityRef = this;
            bombActive = true;
        bombActiveTimer = 3.0f;
    }

    public void ExplosionOccured()
    {
        bombActive = false;
        if (HasAuthority())
        {
            HUDRef.SetCooldownForIcon(tier, maxCooldownTime - bombActiveTimer);
            HUDRef.UseAbility(tier);
        }
        StartCoroutine(Cooldown(false));
    }

    public override IEnumerator Windup() //duration of the introduction decorations, followed by AbilityAction
    {
        ApplyPlayerCastMotion();
        weaponRef.SetDefaultBehaviourEnabled(true, false);
        yield return new WaitForSeconds(windupTime);
        RemovePlayerCastMotion();
        AbilityAction();
        yield return new WaitForSeconds(castSlowDuration - windupTime);
        weaponRef.SetDefaultBehaviourEnabled(true, true);
        
    }

    public override void newDemonic() { }

    private void Update()
    {
        if (bombActive)
            bombActiveTimer -= Time.deltaTime;
        if (bombActiveTimer < 0)
            bombActiveTimer = 0;


        if (cancellable)
        {
            Debug.Log("Cancellable");
            if (movementRef.grounded)
                ReturnControls();
        }
    }

    public void ChangeControls()
    {
        motionVFXRef = Instantiate(dashMotionVFX, spellOrigin.transform);
        movementRef.SetEnabledControls(false, true);
        Invoke("SetCancellableTrue", 1.0f);
        GetComponentInParent<PlayerMovement>().SetEnabledControls(false, true);
        //GetComponentInParent<PlayerMovement>().SetPartialControl(0.1f);
        Invoke("ReturnControls",2.0f);
    }

    public void ReturnControls()
    {
        if (motionVFXRef != null) Destroy(motionVFXRef.gameObject);
        movementRef.SetEnabledControls(true, true);
        //GetComponentInParent<PlayerMovement>().SetEnabledControls(true, true);
        cancellable = false;
    }

    public void SetCancellableTrue()
    {
        cancellable = true;
    }
    public override IEnumerator Cooldown(bool demonic)
    {
        onCooldown = true;
        if (demonic)
            yield return new WaitForSeconds(maxCooldownTime/2);
        else
            yield return new WaitForSeconds(maxCooldownTime -  bombActiveTimer);
        onCooldown = false;
    }
}
