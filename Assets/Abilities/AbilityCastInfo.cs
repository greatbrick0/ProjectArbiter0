using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells")]
public class AbilityCastInfo : ScriptableObject
{
    //This is the information on how to properly cast an ability. The PlayerAbilitySystem uses these.
    //This is NOT the actual ability/projectile logic...
    [SerializeField]
    public GameObject abilityRef;

    //Cooldowns
    public bool hasCooldown;
    public float cooldownTime;


    public bool chargable = false;
    //If charge is true, additional functionality:

   
    public float castSlowDuration; //how long to slow player for when cast;



    //If you can/need to cancel the ability.
    public bool cancellable = false;


    //Put any post-fire information here
    public bool canRecast;


    //Im not sure if there are any abilities taht need to be toggled, but ill put this here too
    public bool canToggle;

    //Sanity
    public float sanCost;
    public float sanVar; //I keep finding reasons i need another random sanity-related variable, soo.......
    public float sanRecoverMod = 1; //if you want to balance a spell by changing how long before you start getting sanity back after cast

    //Demon modifications
    public bool demonic;


    public virtual void PlayerEffects(GameObject player) //terrible name, ik. Anything in the player that is UNRELATED to the projectile/physical component.
    {
        //Im moving the player slowing down part since it has some problems with the scriptableobject.
        
    }

    public virtual void ToggleOff() //only applied if canToggle. ends the toggle.
    {

    }

    
}
