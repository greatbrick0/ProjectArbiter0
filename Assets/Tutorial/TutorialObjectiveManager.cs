using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObjectiveManager : MonoBehaviour
{
    public int currentTutorialPrompt = 0;

    public void UpdateTutorialPrompt(int currentPrompt)
    {
        currentTutorialPrompt = currentPrompt;
    }
}
