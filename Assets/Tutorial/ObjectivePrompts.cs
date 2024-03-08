using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectivePrompts : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objectiveText;
    [SerializeField] TutorialObjectiveManager tutorialManager;
    [SerializeField] List<string> tutorialPrompts;

    private void Update()
    {
        objectiveText.text = tutorialPrompts[tutorialManager.currentTutorialPrompt];
    }
}
