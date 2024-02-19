using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDSystem : MonoBehaviour
{
    [SerializeField]
    public List<IconCooldown> abilityIcons = new List<IconCooldown>();

    
    //UIHealth healthRef;

    [SerializeField]
    HUDGunAmmoScript gunHUDRef;

    [SerializeField]
    GameObject SanityBar;
    //private float targetSanity = 100;

    [SerializeField]
    TextMeshProUGUI healthLabel;
    [SerializeField]
    DamageOpacity damageGradient;

    [SerializeField]
    TextMeshProUGUI ObjectiveText;
    int progress, maxprogress;
    string objective;

 
    public void UseAbility(int tier)
    {
        abilityIcons[tier].UseSpell();
    }

    public void SetCooldownForIcon(int tier, float cooldownTime)
    {
        abilityIcons[tier].CooldownTimeManipulate(cooldownTime);
    }

    public void SetHealthLabel(string newText)
    {
        healthLabel.text = newText;
    }

    public void EnableDamageGradient()
    {
        damageGradient.FullOpacity();
    }

    public void UpdateObjective(int newprogress, int newmaxprogress, string newObjective)
    {
        progress = newprogress;
        maxprogress = newmaxprogress;
        objective = newObjective;
        ObjectiveText.text = objective + "\n  " + progress + " / " + maxprogress;
    }

    public void UpdateObjective(int increment)
    {
        progress += increment;
        ObjectiveText.text = objective + "\n  " + progress + " / " + maxprogress;
    }
}
