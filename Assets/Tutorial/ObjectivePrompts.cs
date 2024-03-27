using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectivePrompts : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objectiveText;
    [SerializeField] GameObject objectiveFrame;
    [SerializeField] TutorialObjectiveManager tutorialManager;
    [SerializeField] List<string> tutorialPrompts;

    int previousPrompt = -1;

    private void Update()
    {
        if(previousPrompt != tutorialManager.currentTutorialPrompt)
        {
            ChangeText(tutorialPrompts[tutorialManager.currentTutorialPrompt]);
            previousPrompt = tutorialManager.currentTutorialPrompt;
        }
    }

    void ChangeText(string newText)
    {
        if(newText != "")
        {
            StopAllCoroutines();
            objectiveText.text = newText;
            objectiveFrame.GetComponent<Image>().color = ChangeColourAlpha(objectiveFrame.GetComponent<Image>().color, 1f);
        }
        else
        {
            StartCoroutine(SetClearDelayed(0.3f));
        }
    }

    Color ChangeColourAlpha(Color oldColour, float newAlpha)
    {
        return new Color(oldColour.r, oldColour.g, oldColour.b, newAlpha);
    }

    IEnumerator SetClearDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        objectiveFrame.GetComponent<Image>().color = ChangeColourAlpha(objectiveFrame.GetComponent<Image>().color, 0.0f);
        objectiveText.text = "";
    }
}
