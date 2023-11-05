using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    float minTextSize;
    float maxTextSize;
    Vector3 baseSize;
    Vector3 hoveredSize;

    [SerializeField] GameModeText titleText;
    [SerializeField] GameModeText descText;
    [SerializeField] Image borderImage;

    TextMeshProUGUI buttonText;
    
    void Start()
    {
        baseSize = new Vector3(transform.localScale.x, transform.localScale.y, 0);
        hoveredSize = baseSize * 1.1f;

        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        minTextSize = buttonText.fontSize;
        maxTextSize = minTextSize * 1.1f;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.value(minTextSize, maxTextSize, 0.05f).setOnUpdate(ResizeText);
        LeanTween.scale(transform.GetComponent<RectTransform>(), hoveredSize, 0.05f);

        LeanTween.value(2, 2.3f, 0.05f).setOnUpdate(AdjustBorderSize);

        if (titleText != null && descText != null)
        {
            titleText.MoveTextUp();
            descText.MoveTextUp();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.value(maxTextSize, minTextSize, 0.05f).setOnUpdate(ResizeText);
        LeanTween.scale(transform.GetComponent<RectTransform>(), baseSize, 0.05f);

        LeanTween.value(2.3f, 2, 0.05f).setOnUpdate(AdjustBorderSize);

        if (titleText != null && descText != null)
        {
            titleText.MoveTextDown();
            descText.MoveTextDown();
        }
    }

    void ResizeText(float val)
    {
        buttonText.fontSize = val;
    }

    void AdjustBorderSize(float val)
    {
        if (borderImage != null)
        {
            borderImage.pixelsPerUnitMultiplier = val;
        }
    }
}
