using Coherence;
using Coherence.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInputSystem : MonoBehaviour
{
    //Gotta build some shizz first :)
    public class AbilitySlot
    {
        public Ability ability { get;private set;}

        public enum AbilityState //state of the ability/slot. not the player
        {
            ready, //can be used
            active, //is being used
            cooldown, //cannot be used due to cooldown
            locked, //cannot be used because NO!
        }

        public AbilityState state = AbilityState.ready;

    }

    enum CastingState //wether the player is casting or not. used for allow/lock casting logic.
    { 
        idle, //not casting at the moment
        casting, //currently casting
        demonic, //currently DEMONIC!!!
        exhausted, //after demonic, you will be locked from spells for a while
        locked, //no spells imputs allowed! (cutscene?)
    }

    //Variables
    [SerializeField]
    public Ability[] AbilityList;
    [SerializeField]
    private CastingState playerState = CastingState.idle;

    private HUDSystem HUDRef;

 

    private void Start()
    {
              
        AbilityList = GetComponents<Ability>();
        Debug.Log("Grabbed Abilities?");

        
        
        
    }

    void Update()
    {


    }

    
    public void AttemptCast(int tier) //Recieved input by player input
    {
       
        if (AbilityList[tier].onCooldown) //is that ability on cooldown? if so, end process.
            return;


        switch (playerState)
        {
            case CastingState.idle: //if player is allowed to cast spells right now.
                {
                    
                    AbilityList[tier].StartAbility();
                    break;
                }
        }

    }

    public void GetHUDReference()
    {
        HUDRef = GameObject.Find("PlayerHUD").GetComponent<HUDSystem>();
        Debug.Log("AbilityInputSystem: " + nameof(HUDRef));
        for (int i = 0; i < AbilityList.Length; i++)
        {
            AbilityList[i].RecieveHUDReference(HUDRef, i);
        }
    }
    

}
