using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleTrigger : MonoBehaviour
{
    [SerializeField] TutorialObjectiveManager tutorialManager;
    [SerializeField] int tutorialPromptNumber;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tutorialManager.UpdateTutorialPrompt(tutorialPromptNumber);
        }
    }
}
