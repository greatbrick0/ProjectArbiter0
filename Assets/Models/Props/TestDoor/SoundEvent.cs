using UnityEngine;
using System.Collections;
using FMODUnity;
using FMOD.Studio;

public class SoundEvent : MonoBehaviour
{
    [SerializeField] private EventReference doorOpen;
    public void PlaySound ()
    {
        RuntimeManager.PlayOneShotAttached(doorOpen, gameObject);
    }
}