using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine.ProBuilder.Shapes;

public class FMODEvents : MonoBehaviour
{
    //Player Abilities
    [field: Header ("Player Abilities")]

    [field: Header ("Ice")]
    [field: SerializeField] public EventReference hammerSwing { get; private set; }
    [field: SerializeField] public EventReference iceDashBeginning { get; private set; }
    [field: SerializeField] public EventReference iceDashEnd { get; private set; }
    [field: SerializeField] public EventReference iceEnhancement { get; private set; }
    [field: Header ("Fire")]

    [field: SerializeField] public EventReference fireCone { get; private set; }
    [field: SerializeField] public EventReference fireBombWindUp { get; private set; }
    [field: SerializeField] public EventReference fireBombExplosion { get; private set; }
    [field: SerializeField] public EventReference fireEnhancement { get; private set; }

    [field: Header ("Nature")]
    [field: SerializeField] public EventReference nature1 { get; private set; }
    [field: SerializeField] public EventReference nature2 { get; private set; }
    [field: SerializeField] public EventReference nature3 { get; private set; }

    [field: Header ("Enemy")]
    [field: SerializeField] public EventReference enemyShoot { get; private set; }

    //This is for non-player specific sounds.
    [field: Header ("General SFX")]
    [field: SerializeField] public EventReference groundHit { get; private set; }
    [field: SerializeField] public EventReference bodyHit { get; private set; }
    [field: SerializeField] public EventReference citicalHit { get; private set; }

    [field: SerializeField] public EventReference menuHover { get; private set; }
    [field: SerializeField] public EventReference menuSelect { get; private set; }

    [field: SerializeField] public EventReference healing { get; private set; }
    [field: SerializeField] public EventReference healed { get; private set; }
    [field: SerializeField] public EventReference heartBeat { get; private set; }

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