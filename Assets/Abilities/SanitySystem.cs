using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanitySystem : MonoBehaviour
{

    HUDSanity sanityHUDRef;
    [SerializeField]
    AbilityInputSystem abilitySystemRef;


    [SerializeField]
    float maxSanity;

    [SerializeField]
    float currentSanity;

    [SerializeField]
    float sanityRecoverySpeedModifier;

    [SerializeField]
    float sanityRecoveryPause;

    private float sanityPauseTimer;


    public bool demonic { get; private set; }

    public float Sanity
    {
        get
        {
            return currentSanity;
        }
        set
        {
            if (value < currentSanity)
                sanityPauseTimer = sanityRecoveryPause;

            currentSanity = value;
            sanityHUDRef.UpdateSanityHUD(currentSanity);

            if (!demonic && currentSanity <= 0.9) //nerf, but improves clarity
            
            {
                currentSanity = 0;
                demonic = true;
            }

        }
    }

    

    
    public void GetHUDReference()
    {
        sanityHUDRef = GameObject.Find("SanityHUDOverlay").GetComponent<HUDSanity>();
    }

    private void Update()
    {
        if (sanityPauseTimer > 0)
            sanityPauseTimer -= Time.deltaTime;
        
        if (sanityPauseTimer <=0)
        {
            if (currentSanity <  maxSanity)
            Sanity += Time.deltaTime * sanityRecoverySpeedModifier;
        }
    }

}
    