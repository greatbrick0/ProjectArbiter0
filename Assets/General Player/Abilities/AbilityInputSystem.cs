using Coherence;
using Coherence.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInputSystem : MonoBehaviour
{
    //Gotta build some shizz first :)

    public enum CastingState //wether the player is casting or not. used for allow/lock casting logic.
    {
        idle, //not casting at the moment
        casting, //currently casting
        exhausted, //after demonic, you will be locked from spells for a while
        locked, //no spells imputs allowed! (cutscene?)
    }

    //Variables
    [SerializeField]
    public Ability[] AbilityList; //not a list...
    [SerializeField]
    public CastingState playerState = CastingState.idle;

    [SerializeField]
    public bool[] abilityLocks = {true,true,true};

    private HUDSystem HUDRef;

    public bool demonic { get; private set; }

    [SerializeField]
    private float exhaustTime;

    [SerializeField]
    public float castSlowAmount = 2.0f;

    [SerializeField]
    CoherenceSync sync;

    [SerializeField]
    GameObject spellOrigin;
    private void Start()
    {
        RegisterAbilities();
    }

    public void RegisterAbilities()
    {
        AbilityList = null;
        AbilityList = GetComponentsInChildren<Ability>();
        GetHUDReference();
    }
    

    public void AttemptCast(int tier) //Recieved input by player input
    {

        if (AbilityList[tier].onCooldown) //is that ability on cooldown? if so, end process.
            return;
        if (!abilityLocks[tier])
            return;

        switch (playerState)
        {
            case CastingState.idle: //if player is allowed to cast spells right now.
                {
                    if (!demonic)
                        sync.SendCommand<AbilityInputSystem>(nameof(SendCastToAbility), MessageTarget.All, tier, false);
                    else
                        sync.SendCommand<AbilityInputSystem>(nameof(SendCastToAbility), MessageTarget.All, tier, true);
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

    public void SendCastToAbility(int tier, bool doDemon)
    {
        if (doDemon)
        AbilityList[tier].RecieveDemonicAbilityRequest();
        else
        AbilityList[tier].RecieveAbilityRequest();

    }

    public void GetHUDReference()
    {
        HUDRef = GameObject.Find("PlayerHUD").GetComponent<HUDSystem>();
        for (int i = 0; i < AbilityList.Length; i++)
        {
            //Debug.Log(i);
            AbilityList[i].RecieveHUDReference(HUDRef, i);
        }
    }

    public void SetDemonic(bool setDemonic)
    {
        if (setDemonic)
            demonic = true;
        else
        {
            demonic = false; ;
            playerState = CastingState.exhausted;
            StartCoroutine(ExhaustedTimer());
        }

        if (demonic)
        {
            foreach (Ability a in AbilityList)
            {
                a.newDemonic();
            }
        }
    }

    IEnumerator ExhaustedTimer()
    {
        yield return new WaitForSeconds(exhaustTime);
        playerState = CastingState.idle;
    }
    IEnumerator TimeCasting(float duration)
    {
        playerState = CastingState.casting;
        yield return new WaitForSeconds(duration);
        playerState = CastingState.exhausted;

    }

}