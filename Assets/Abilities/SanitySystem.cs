using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanitySystem : MonoBehaviour
{

    HUDSanity sanityHUDRef;
    [SerializeField]
    AbilityInputSystem abilitySystemRef;


    [SerializeField]
    int maxSanity;

    [SerializeField]
    int currentSanity;


    [SerializeField]
    float sanityRecoveryPause;

    private float sanityPauseTimer;


    public bool demonic { get; private set; }

    public int Sanity
    {
        get
        {
            return currentSanity;
        }
        set
        {
            currentSanity = value;
            sanityHUDRef.UpdateSanityHUD(currentSanity);
            if (currentSanity <= 0)
            {
                demonic = true;
            }

        }
    }

    

    private void Start()
    {
        currentSanity = maxSanity;
    }
    public void GetHUDReference()
    {
        sanityHUDRef = GameObject.Find("SanityHUDOverlay").GetComponent<HUDSanity>();
    }

    private void Update()
    {
        
    }

}
