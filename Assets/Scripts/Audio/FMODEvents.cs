using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;

public class FMODEvents : MonoBehaviour
{
    //Player Abilities
    [field: Header ("Player Abilities")]
    [field: Header ("Ice")]
    [field: SerializeField] public EventReference hammerSwing { get; private set; }
    [field: SerializeField] public EventReference iceCharge { get; private set; }
    [field: SerializeField] public EventReference iceEnhancement { get; private set; }
    [field: Header ("Fire")]
    [field: SerializeField] public EventReference fireCone { get; private set; }
    [field: SerializeField] public EventReference fireBomb { get; private set; }
    [field: SerializeField] public EventReference fireEnhancement { get; private set; }
    [field: Header ("Mutant")]
    [field: SerializeField] public EventReference mutant1 { get; private set; }
    [field: SerializeField] public EventReference mutant2 { get; private set; }
    [field: SerializeField] public EventReference mutant3 { get; private set; }

    //This is for non-player specific sounds.
    [field: Header ("General SFX")]
    [field: SerializeField] public EventReference bulletHit { get; private set; }
    [field: SerializeField] public EventReference bodyHit { get; private set; }
    [field: SerializeField] public EventReference citicalHit { get; private set; }

    [field: SerializeField] public EventReference menuHover { get; private set; }
    [field: SerializeField] public EventReference menuSelect { get; private set; }

    [field: Header ("Music")]
    [field: SerializeField] public EventReference music { get; private set; }

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