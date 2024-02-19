using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDCreator : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public GameObject HUDref;

    public void Awake()
    {
        if (HUDref != null) Instantiate(HUDref);
        Invoke(nameof(AssignUIToComponents),0.5f);
        
    }

    private void AssignUIToComponents()
    {
        GetComponent<WeaponHolder>().GetHUDReference();
        GetComponent<SanitySystem>().GetHUDReference();
        GetComponent<AbilityInputSystem>().GetHUDReference();
        Debug.Log("Assigned UI");
    }
}
