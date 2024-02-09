using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelPanel : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] GameObject labelImage;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] List<LevelPanel> otherPanels;
    public bool isActive;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isActive)
        {
            LeanTween.value(300, 600, 0.1f).setOnUpdate(AdjustWidth);
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
        LeanTween.moveLocalX(labelImage, -300, 0.1f);
    }

    public void AdjustWidth(float val)
    {
        rectTransform.sizeDelta = new Vector2(val, 360);
    }
}
