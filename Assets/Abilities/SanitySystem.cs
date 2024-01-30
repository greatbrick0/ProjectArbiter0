using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SanitySystem : MonoBehaviour
{

    HUDSanity sanityHUDRef;
    [SerializeField]
    AbilityInputSystem abilitySystemRef;


    [SerializeField]
    float maxSanity;

    float currentSanity = 100;

    [SerializeField]
    float sanityRecoverySpeedModifier;
    [SerializeField]
    float demonicRecoverySpeedModifier;

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
            if (demonic && value < 0)
                return;
            if (value < currentSanity)
                sanityPauseTimer = sanityRecoveryPause;

            currentSanity = value;
            if (sanityHUDRef != null)
            sanityHUDRef.UpdateSanityHUD(currentSanity);

            if (!demonic && currentSanity <= 0.9) //nerf, but improves clarity
            
            {
                currentSanity = 0;
                demonic = true;
                abilitySystemRef.SetDemonic(true);
            }

            if (demonic && currentSanity >= maxSanity)
            {
                demonic = false;
                abilitySystemRef.SetDemonic(false);
            }

        }
    }

    

    
    public void GetHUDReference()
    {
        sanityHUDRef = GameObject.Find("SanityHUDOverlay").GetComponent<HUDSanity>();
    }

    private void Update()
    {
        if (demonic)
        {
            if (sanityPauseTimer > 0)
                sanityPauseTimer -= Time.deltaTime;

            if (sanityPauseTimer <= 0)
            {
                if (currentSanity < maxSanity)
                    Sanity += Time.deltaTime * sanityRecoverySpeedModifier;
            }
        }
        else
        {
            Sanity += Time.deltaTime * demonicRecoverySpeedModifier;
        }
    }

}
    