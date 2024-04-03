using UnityEngine;
using System.Collections;
using FMODUnity;
using FMOD.Studio;

public class SoundEvent : MonoBehaviour
{
    public FMODEvents doorOpen;

    public void PlaySound (FMODEvents doorOpen)
    {
        if(doorOpen != null)
        {
            RuntimeManager.PlayOneShotAttached(FMODEvents.instance.doorOpen, gameObject);
        }
        else
        {
            Debug.LogError("EventSound is null");
        }
    }
}