using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    //This script is for non-player specific sounds.
    [field: Header ("Player SFX")]
    [field: SerializeField] public EventReference iceSpikes { get; private set; }
    [field: SerializeField] public EventReference iceCharge { get; private set; }
    [field: SerializeField] public EventReference groundImpact { get; private set; }
    [field: SerializeField] public EventReference bodyShot { get; private set; }
    [field: SerializeField] public EventReference headShot { get; private set; }

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