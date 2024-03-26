using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class HUDSanity : MonoBehaviour
{
    [SerializeField]
    GameObject sanityBar;
    [SerializeField]
    GameObject secondaryBar;


    [SerializeField]
    GameObject sanityWarning;

    [SerializeField]
    GameObject sanityWarping;

    public Volume volume;

    public float sanityTarget;

    [SerializeField]
    float age;

    private bool barAtRest = true;

    private bool demonic = false;
    private bool exhausted = false;

    private bool doLerp = true;

    public void UpdateSanityHUD(float sanity)
    {
        

        if (sanityTarget - sanity < 0.5 && sanityTarget - sanity > -0.5) //Fixes the 'buffering' bar when regenning
        {
            doLerp = false; 
        }
        else
            doLerp = true;
        sanityTarget = sanity;


        age = 0;
        barAtRest = false;


    }

    public void SanityBarColourChange()
    {
        if (demonic)
            sanityBar.GetComponent<Image>().color = Color.red;
        else if (exhausted)
            sanityBar.GetComponent<Image>().color = Color.grey;
        else
            sanityBar.GetComponent<Image>().color = Color.green;
    }

    private void Update()
    {
        if (barAtRest)
            return;
        if (age < 1)
            age += Time.deltaTime / 4;
        if (age > 1)
            age = 1;

        sanityBar.transform.localScale = new Vector3(sanityTarget / 100, 1, 1);
        if (doLerp)
            secondaryBar.transform.localScale = new Vector3(Mathf.Lerp(secondaryBar.transform.localScale.x, sanityTarget / 100, age), 1, 1);
        else
            secondaryBar.transform.localScale = new Vector3(sanityTarget / 100, 1, 1);


        if (age == 1)
            barAtRest = true;
        

    }

    public void SetDemonic(bool newDemonic)
    {
        demonic = newDemonic;
        Debug.Log(":::" + demonic);
        SanityBarColourChange();

        if (demonic)
        {
            if (volume.profile.TryGet(out Bloom bloom))
            {
                bloom.intensity.value = 8f;
            }
            if (volume.profile.TryGet(out Vignette vignette))
            {
                vignette.intensity.value = 0.5f;
            }
            if (volume.profile.TryGet(out ColorAdjustments colorAdjust))
            {
                colorAdjust.saturation.value = -50;
            }
            if (volume.profile.TryGet(out ShadowsMidtonesHighlights SMH))
            {
                SMH.shadows.value = new Vector4(0,0,0,1);
                SMH.midtones.value = new Vector4(0, 0, 0, 1);
            }
        }
        else
        {
            if (volume.profile.TryGet(out Bloom bloom))
            {
                bloom.intensity.value = 4f;
            }
            if (volume.profile.TryGet(out Vignette vignette))
            {
                vignette.intensity.value = 0.25f;
            }
            if (volume.profile.TryGet(out ColorAdjustments colorAdjust))
            {
                colorAdjust.saturation.value = 0;
            }
            if (volume.profile.TryGet(out ShadowsMidtonesHighlights SMH))
            {
                SMH.shadows.value = new Vector4(0, 0, 0, 0);
                SMH.midtones.value = new Vector4(0, 0, 0, 0);
            }
        }
    }

}

