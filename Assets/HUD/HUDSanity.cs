using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDSanity : MonoBehaviour
{
    [SerializeField]
    GameObject sanityBar;

    [SerializeField]
    GameObject sanityWarning;

    [SerializeField]
    GameObject sanityWarping;

    public float sanityTarget;

    [SerializeField]
    float age;

    private bool barAtRest = true;

    private bool demonic = false;

    public void UpdateSanityHUD(int sanity)
    {
        Debug.Log("Update Sanity");
        sanityTarget = sanity;
        Debug.Log(sanityTarget);

        if (sanityTarget <= 0)
        {
            sanityTarget = 0;
            demonic = true;
            SanityBarColourChange();
        }


        age = 0;
        barAtRest = false;
    }

    public void SanityBarColourChange()
    {
        if (demonic) sanityBar.GetComponent<Image>().color = Color.red;
        else sanityBar.GetComponent<Image>().color = Color.green;
    }

    private void Update()
    {
        if (barAtRest)
            return;
        if (age < 1)
            age += Time.deltaTime;
        if (age > 1)
            age = 1;

        
        sanityBar.transform.localScale = new Vector3(Mathf.Lerp(sanityBar.transform.localScale.x, sanityTarget/100, age), 1, 1);
        if (age == 1)
            barAtRest = true;
       
        
    }

}
