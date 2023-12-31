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
    private float targetSanity = 100;


    public void Update()
    {
        SanityBar.transform.localScale = new Vector3(Mathf.Lerp(SanityBar.transform.localScale.x, 1 / (100 / targetSanity), Time.deltaTime), 1, 1);
    }
    public void UseAbility(int tier)
    {
        abilityIcons[tier].UseSpell();
    }


    public void SanityUpdate(float newTarget)
    {
        targetSanity = newTarget;
    }

    public void SanityDemonicChange(bool Demonic)
    {
        if (Demonic) SanityBar.GetComponent<Image>().color = Color.red;
        else SanityBar.GetComponent<Image>().color = Color.green;
    }


}
