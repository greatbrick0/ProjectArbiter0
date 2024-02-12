using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header ("Player SFX")]
    [field: SerializeField] public EventReference playerReloading { get; private set; }
    [field: SerializeField] public EventReference pistolShoot { get; private set; }
    [field: SerializeField] public EventReference iceSpikes { get; private set; }
    [field: SerializeField] public EventReference iceCharge { get; private set; }
    [field: SerializeField] public EventReference bulletHit { get; private set; }

    [field: Header ("Music")]
    [field: SerializeField] public EventReference music { get; private set; }

    [field: Header ("Menu Sounds")]
    [field: SerializeField] public EventReference menuHover { get; private set; }
    [field: SerializeField] public EventReference menuSelect { get; private set; }

    public static FMODEvents instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one audio manager in the scene.");
        }
        instance = this;
    }
}