using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsPanels : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image greyPanel;
    
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //greyPanel.SetActive(true);
        LeanTween.value(0, 1, 0.1f).setOnUpdate(AdjustOpacity);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //greyPanel.SetActive(false);
        LeanTween.value(1, 0, 0.1f).setOnUpdate(AdjustOpacity);
    }

    void AdjustOpacity(float val)
    {
        greyPanel.color = new Color(greyPanel.color.r, greyPanel.color.g, greyPanel.color.b, val);
    }
}
