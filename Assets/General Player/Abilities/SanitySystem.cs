using Coherence.Toolkit;
using Coherence;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SanitySystem : MonoBehaviour
{

    HUDSanity sanityHUDRef;
    [SerializeField]
    AbilityInputSystem abilitySystemRef;

    [SerializeField]
    GameObject demonicVFX;
    GameObject auraRef;

    [SerializeField]
    CoherenceSync sync;



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
            if (sanityHUDRef != null && GetComponent<PlayerInput>().authority)
            sanityHUDRef.UpdateSanityHUD(currentSanity);

            if (!demonic && currentSanity <= 0.9) //nerf, but improves clarity
            
            {
                
                BecomeDemonic();
                
            }

            if (demonic && currentSanity >= maxSanity)
            {
                EndDemonic();
            }

        }
    }

    

    
    public void GetHUDReference()
    {
        if (!GetComponent<PlayerInput>().authority) return;
        sanityHUDRef = GameObject.Find("SanityHUDOverlay").GetComponent<HUDSanity>();
        
        sanityHUDRef.volume = GetComponent<PlayerInput>().cameraRef.GetComponent<Volume>();
        sanityHUDRef.exhaustVolume = GetComponent<PlayerInput>().cameraRef.GetComponent<MainCameraScript>().exhaustVolume;
        sync = GetComponent<CoherenceSync>(); //this really should be here. whatever.
    }

    private void Update()
    {
        if (!demonic)
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

    public void BecomeDemonic()
    {
        Debug.Log("Starting Demonic");
        currentSanity = 0;
        demonic = true;
        if (GetComponent<PlayerInput>().authority){
            abilitySystemRef.SetDemonic(true);
            sanityHUDRef.SetDemonic(true);
        }
        //sync.SendCommand<SanitySystem>(nameof(ShowAura), MessageTarget.All,true);
        
    }

    public void ShowAura(bool useAura)
    {
        Debug.Log("ShowAura");
        if (useAura)
            auraRef = Instantiate(demonicVFX, transform);
        else
            Destroy(auraRef.gameObject);
    }


    public void EndDemonic()
    {
        Debug.Log("ending demonic");
        demonic = false;
        if (GetComponent<PlayerInput>().authority)
        {
            abilitySystemRef.SetDemonic(false);
            sanityHUDRef.SetDemonic(false);
        }
        sync.SendCommand<SanitySystem>(nameof(ShowAura), MessageTarget.All, false);
    }
}
    