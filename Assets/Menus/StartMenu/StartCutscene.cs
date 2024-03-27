using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] Image titleText;
    [SerializeField] Image panelImage;
    [SerializeField] TextMeshProUGUI startText;
    [SerializeField] TextMeshProUGUI optionsText;
    [SerializeField] TextMeshProUGUI exitText;

    private void Start()
    {
        titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0);
        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 1);
        startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, 0);
        optionsText.color = new Color(optionsText.color.r, optionsText.color.g, optionsText.color.b, 0);
        exitText.color = new Color(exitText.color.r, exitText.color.g, exitText.color.b, 0);

        StartCoroutine(StartScene());
    }

    private void AdjustTitleOpacity(float val)
    {
        if (titleText != null) return;
        titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, val);
    }

    private void AdjustButtonsOpacity(float val)
    {
        if(startText == null) return;
        startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, val);
        optionsText.color = new Color(optionsText.color.r, optionsText.color.g, optionsText.color.b, val);
        exitText.color = new Color(exitText.color.r, exitText.color.g, exitText.color.b, val);
    }

    private void AdjustBackgroundOpacity(float val)
    {
        if(panelImage == null) return; 
        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, val);
    }

    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(1.2f);
        LeanTween.value(0, 1, 0.8f).setOnUpdate(AdjustTitleOpacity);
        yield return new WaitForSeconds(1);
        LeanTween.value(0, 1, 0.8f).setOnUpdate(AdjustButtonsOpacity);
        yield return new WaitForSeconds(0.8f);
        LeanTween.value(1, 0.7f, 1.5f).setOnUpdate(AdjustBackgroundOpacity);
    }
}
