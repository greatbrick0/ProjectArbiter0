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
    public bool isActive;

    private void Update()
    {
        outline.GetComponent<RectTransform>().sizeDelta = new Vector2(rectTransform.sizeDelta.x + 10, rectTransform.sizeDelta.y + 10);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isActive)
        {
            LeanTween.value(300, 600, 0.1f).setOnUpdate(AdjustWidth);
            LeanTween.value(0, 1, 0.1f).setOnUpdate(AdjustOutlineOpacity);
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
        LeanTween.moveLocalX(labelImage, -300, 0.1f);
    }

    public void AdjustWidth(float val)
    {
        rectTransform.sizeDelta = new Vector2(val, 360);
    }

    public void AdjustOutlineOpacity(float val)
    {
        outline.color = new Color(1, 1, 1, val);
    }
}
