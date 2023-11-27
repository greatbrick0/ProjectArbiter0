using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public abstract class Ability : MonoBehaviour
{
    //This is the ability logic. It will be attached to the ability/projectile.

    //All the fun stuff
    public int abilityDamage = 0; //damage. if it doesnt change damage then uhhhhh.xd
    public float lifetime = 0f; //functions as duration for enhancments.
    public float speed = 0f; //projectile speed if applicable

    public bool cancellable = false;



    //These used to be enums, and I cannot for the life of me serialize it correctly.
    public string Character; //character to use this power (ice,fire,mutant,test)
    public string tier; //primary, secondary, enhancement
    public GameObject caster { get; private set; } //Reference to player who used the ability.


    //protected Collider collideRef;
    //protected Rigidbody rb;

    public bool hasBody; //is it a projectile? does something need to be instantiated

    //Demon logic modifications
    public bool demonic; //followed by example demon changes
    public int abilityDamageDemon;
    public float lifetimeDemon;
    public float speedDemon;
    //public Collider demonColliderExtra;

    private void Awake()
    {
        Debug.Log("Awake");
        AssignAbilityComponents();

        DoExpire();
    }

    protected abstract void AssignAbilityComponents();

    public virtual void SetDemonic()
    {
        demonic= true; 
    }

    public void SetCaster(GameObject player)
    {
        Debug.Log("SetCaster");
        caster = player;
        if (caster.GetComponent<PlayerAbilitySystem>().inDemonicState)
            SetDemonic();
    }

    public virtual void DoExpire() //Ends spell after intended duration
    {
        Destroy(this.gameObject, lifetime);
    }




}
