using Coherence;
using Coherence.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInputSystem : MonoBehaviour
{
    //Gotta build some shizz first :)

    enum CastingState //wether the player is casting or not. used for allow/lock casting logic.
    { 
        idle, //not casting at the moment
        casting, //currently casting
        exhausted, //after demonic, you will be locked from spells for a while
        locked, //no spells imputs allowed! (cutscene?)
    }

    //Variables
    [SerializeField]
    public Ability[] AbilityList;
    [SerializeField]
    private CastingState playerState = CastingState.idle;

    private HUDSystem HUDRef;

    public bool demonic { get; private set; }

    [SerializeField]
    private float exhaustTime;
    
 

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
                    if (demonic)
                        AbilityList[tier].StartAbility();
                    else
                        AbilityList[tier].DemonicStartAbility();
                    break;
                }
            case CastingState.casting: //if you are currently casting something, you gotta wait!
                break;
            
            case CastingState.exhausted:
                {
                    //invalid cast feedback
                    break;
                }
            case CastingState.locked:
                {
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
    
    public void SetDemonic(bool setDemonic)
    {
        if (setDemonic)
            demonic = true;
        else
        {
            demonic = false;;
            playerState = CastingState.exhausted;
            StartCoroutine(ExhaustedTimer());
        }
    }

    IEnumerator ExhaustedTimer()
    {
        yield return new WaitForSeconds(exhaustTime);
    }
    IEnumerator TimeCasting(float duration)
    {
        playerState = CastingState.casting;
        yield return new WaitForSeconds(duration);
        playerState = CastingState.exhausted;

    }

}
