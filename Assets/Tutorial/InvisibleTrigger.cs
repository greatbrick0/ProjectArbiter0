using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleTrigger : MonoBehaviour
{
    [SerializeField] TutorialObjectiveManager tutorialManager;
    [SerializeField] int requiredCurrentPrompt;
    [SerializeField] int tutorialPromptNumber;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (tutorialManager.currentTutorialPrompt == requiredCurrentPrompt)
            {
                tutorialManager.UpdateTutorialPrompt(tutorialPromptNumber);

                if (tutorialManager.currentTutorialPrompt == 4) tutorialManager.FadeInAbility1();
                if (tutorialManager.currentTutorialPrompt == 6) tutorialManager.FadeInAbility2();
                if (tutorialManager.currentTutorialPrompt == 8) tutorialManager.FadeInAbility3();
            }
        }
    }
}
