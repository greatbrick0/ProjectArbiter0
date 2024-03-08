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

                if (tutorialManager.currentTutorialPrompt == 4)
                {
                    tutorialManager.FadeInAbility1();
                    other.gameObject.GetComponent<AbilityInputSystem>().abilityLocks[0] = true;
                }
                if (tutorialManager.currentTutorialPrompt == 6)
                {
                    tutorialManager.FadeInAbility2();
                    other.gameObject.GetComponent<AbilityInputSystem>().abilityLocks[1] = true;
                }
                if (tutorialManager.currentTutorialPrompt == 8)
                {
                    tutorialManager.FadeInAbility3();
                    other.gameObject.GetComponent<AbilityInputSystem>().abilityLocks[2] = true;
                }

                if (tutorialManager.currentTutorialPrompt == 0)
                {
                    other.gameObject.GetComponent<AbilityInputSystem>().abilityLocks[0] = false;
                    other.gameObject.GetComponent<AbilityInputSystem>().abilityLocks[1] = false;
                    other.gameObject.GetComponent<AbilityInputSystem>().abilityLocks[2] = false;
                }
            }
        }
    }
}
