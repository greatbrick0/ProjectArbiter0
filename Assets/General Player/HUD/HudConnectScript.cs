using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudConnectScript : MonoBehaviour
{
    [SerializeField]
    IconCooldown[] icons;

    [SerializeField]
    HUDGunAmmoScript ammoRef;

    [SerializeField]
    GameObject sanity;

    [SerializeField]
    TextMeshProUGUI healthLabel;
    [SerializeField]
    Image health;

    [SerializeField]
    DamageOpacity gradient;

    [SerializeField]
    TextMeshProUGUI ObjectiveText;


    private void Start()
    {
        transform.SetAsFirstSibling();
    }

    public void ConnectToHUDSystem()
    {
        HUDSystem HUDRef = GetComponentInParent<HUDSystem>();
        HUDRef.abilityIcons[0] = icons[0];
        HUDRef.abilityIcons[1] = icons[1];
        HUDRef.abilityIcons[2] = icons[2];

        HUDRef.gunHUDRef = ammoRef;
        HUDRef.SanityBar = sanity;
        HUDRef.healthLabel = healthLabel;
        HUDRef.healthBar = health;
        HUDRef.damageGradient = gradient;
        HUDRef.ObjectiveText = ObjectiveText;
        Debug.Log("New HUD attached");
    }
}
