using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelPanel : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] GameObject labelImage;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] List<LevelPanel> otherPanels;
    [SerializeField] Image outline;
    [SerializeField] Image dimmer;
    public bool isActive;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isActive)
        {
            LeanTween.value(300, 600, 0.1f).setOnUpdate(AdjustWidth);
            LeanTween.value(0, 1, 0.1f).setOnUpdate(AdjustOutlineOpacity);
            LeanTween.value(0.35f, 0, 0.1f).setOnUpdate(AdjustDimmerOpacity);
            LeanTween.moveLocalX(labelImage, 0, 0.1f);
            isActive = true;
        }
        
        for (int i = 0; i < otherPanels.Count; i++)
        {
            if (otherPanels[i].isActive)
            {
                otherPanels[i].isActive = false;
                otherPanels[i].Deactivate();
            }
        }
    }

    public void Deactivate()
    {
        LeanTween.value(600, 300, 0.1f).setOnUpdate(AdjustWidth);
        LeanTween.value(1, 0, 0.1f).setOnUpdate(AdjustOutlineOpacity);
        LeanTween.value(0, 0.35f, 0.1f).setOnUpdate(AdjustDimmerOpacity);
        LeanTween.moveLocalX(labelImage, -300, 0.1f);
    }

    public void AdjustWidth(float val)
    {
        rectTransform.sizeDelta = new Vector2(val, 360);
        outline.GetComponent<RectTransform>().sizeDelta = new Vector2(rectTransform.sizeDelta.x + 10, rectTransform.sizeDelta.y + 10);
    }

    public void AdjustOutlineOpacity(float val)
    {
        outline.color = new Color(1, 0.93f, 0.57f, val);
    }

    public void AdjustDimmerOpacity(float val)
    {
        dimmer.color = new Color(0, 0, 0, val);
    }
}
