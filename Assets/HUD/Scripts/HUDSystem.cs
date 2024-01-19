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

 
    public void UseAbility(int tier)
    {
        abilityIcons[tier].UseSpell();
    }

    public void SetCooldownForIcon(int tier, float cooldownTime)
    {
        abilityIcons[tier].CooldownTimeManipulate(cooldownTime);
    }

    

    


}
