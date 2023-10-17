using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float minTextSize;
    [SerializeField] float maxTextSize;
    
    TextMeshProUGUI buttonText;
    
    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonText.fontSize = minTextSize;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.value(minTextSize, maxTextSize, 0.05f).setOnUpdate(ResizeText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.value(maxTextSize, minTextSize, 0.05f).setOnUpdate(ResizeText);
    }

    void ResizeText(float val)
    {
        buttonText.fontSize = val;
    }
}
