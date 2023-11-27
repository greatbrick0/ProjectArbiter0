using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDSystem : MonoBehaviour
{
    [SerializeField]
    public List<IconCooldown> abilityIcons = new List<IconCooldown>();

    
    //UIHealth healthRef;

    [SerializeField]
    HUDGunAmmoScript gunHUDRef;

    [SerializeField]
    GameObject SanityBar;


    public void UseAbility(int tier)
    {
        abilityIcons[tier].UseSpell();
    }


    public void LiveSanityUpdate(float currentSanity)
    {
        SanityBar.transform.localScale= new Vector3(1 / (100 / currentSanity),1,1);
    }

    public void SanityDemonicChange(bool Demonic)
    {
        if (Demonic) SanityBar.GetComponent<Image>().color = Color.red;
        else SanityBar.GetComponent<Image>().color = Color.green;
    }


}
