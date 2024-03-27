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
    public Volume volume;

    [SerializeField]
    public Volume exhaustVolume;

    public float sanityTarget;

    [SerializeField]
    float age;

    private bool barAtRest = true;


    [SerializeField]
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
        if (true)
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
        
        if (exhausted)
        {
            if (volume.weight > 0)
            {
                volume.weight -= 1.5f * Time.deltaTime;
                exhaustVolume.weight += 1.5f * Time.deltaTime;
            }
            if (volume.weight < 0)
            {
                volume.weight = 0;
                exhaustVolume.weight = 1;
            }


        }
        else if (volume.weight < 1)
        {
            volume.weight += 1.5f * Time.deltaTime;
            exhaustVolume.weight -= 1.5f * Time.deltaTime;
            if (volume.weight > 1)
            {
                volume.weight = 1;
                exhaustVolume.weight = 0;
            }
        }


    }

    public void SetDemonic(bool newDemonic)
    {
        exhausted = newDemonic;
    }

}

