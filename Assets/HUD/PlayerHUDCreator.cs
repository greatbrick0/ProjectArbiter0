using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HUDref;


    public void Awake()
    {
        Instantiate(HUDref);
        GetComponent<WeaponHolder>().GetHUDReference();
        GetComponent<PlayerAbilitySystem>().GetHUDReference();
    }
}
