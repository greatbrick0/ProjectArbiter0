using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponHolder : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    string gunName = "basic";
    [SerializeField]
    float ReloadTime = 2.0f;
    [SerializeField]
    int maxAmmo = 12;
    [SerializeField]
    Vector2 closeDamage;
    [SerializeField]
    Vector2 farDamage;

    [Header("Trackers")]
    [SerializeField]
    int currentAmmo;

    private void Start()
    {
        currentAmmo = maxAmmo;
        if (closeDamage.x > farDamage.x) Debug.LogError(gunName + " close damage point cannot be farther than far damage point");
    }

    private int DamageFromDistance(float distance)
    {
        float output = 0;
        if (distance <= closeDamage.x) output = closeDamage.y;
        else if (distance >= farDamage.x) output = farDamage.y;
        else
        {
            output = Mathf.Lerp(closeDamage.y, farDamage.y, (distance - closeDamage.x) / (farDamage.x - closeDamage.x));
        }
        return Mathf.RoundToInt(output);
    }
}
