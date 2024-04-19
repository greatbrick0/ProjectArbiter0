using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDGunAmmoScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text currentAmmoRef;
    private int currentAmmo;


    [SerializeField]
    private TMP_Text maxAmmoRef;
    private int maxAmmo;


    public void SetCurrentAmmo(int newCurrent)
    {
        Debug.Log("Function used SetCurrentAmmo, setting it to" + newCurrent);
        currentAmmo = newCurrent;
        currentAmmoRef.SetText(currentAmmo.ToString());
    }

    public void SetMaxAmmo(int newMax)
    {
        maxAmmo = newMax;
        maxAmmoRef.SetText(maxAmmo.ToString());
    }

    public void UseShot(int amount = 1)
    {
        if (currentAmmo >= amount)
        currentAmmo -= amount;
        currentAmmoRef.SetText(currentAmmo.ToString());
    }

}
