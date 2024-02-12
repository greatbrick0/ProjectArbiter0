using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class MenuSoundsPlayer : MonoBehaviour, IPointerEnterHandler
{
    private EventReference hoverSound;
    private EventReference selectSound;
    
    private void Start()
    {
        hoverSound = FMODEvents.instance.menuHover;
        selectSound = FMODEvents.instance.menuSelect;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FMODUnity.RuntimeManager.PlayOneShot(hoverSound);
    }

    public void PlaySelectSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(selectSound);
    }
}
