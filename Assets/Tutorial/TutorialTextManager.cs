using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTextManager : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> tutorialTexts; //Tutorial labels in the world

    private void Update()
    {
        if (PlayerPrefs.HasKey("forward")) tutorialTexts[0].text = "[" + ((KeyCode)PlayerPrefs.GetInt("forward")).ToString().ToUpper() + "][" + ((KeyCode)PlayerPrefs.GetInt("left")).ToString().ToUpper() + "][" + ((KeyCode)PlayerPrefs.GetInt("backward")).ToString().ToUpper() + "][" + ((KeyCode)PlayerPrefs.GetInt("right")).ToString().ToUpper() + "] to move";
        else tutorialTexts[0].text = "[W][A][S][D] to move";

        if (PlayerPrefs.HasKey("shoot")) tutorialTexts[1].text = "[" + ((KeyCode)PlayerPrefs.GetInt("shoot")).ToString().ToUpper() + "] to shoot";
        else tutorialTexts[1].text = "[M1] to shoot";

        if (PlayerPrefs.HasKey("reload")) tutorialTexts[2].text = "[" + ((KeyCode)PlayerPrefs.GetInt("reload")).ToString().ToUpper() + "] to reload";
        else tutorialTexts[2].text = "[R] to reload";

        if (PlayerPrefs.HasKey("ability1")) tutorialTexts[3].text = "[" + ((KeyCode)PlayerPrefs.GetInt("ability1")).ToString().ToUpper() + "] to activate primary ability";
        else tutorialTexts[3].text = "[Q] to activate primary ability";

        if (PlayerPrefs.HasKey("ability2")) tutorialTexts[4].text = "[" + ((KeyCode)PlayerPrefs.GetInt("ability2")).ToString().ToUpper() + "] to activate movement ability";
        else tutorialTexts[4].text = "[LShift] to activate movement ability";

        if (PlayerPrefs.HasKey("ability3")) tutorialTexts[5].text = "[" + ((KeyCode)PlayerPrefs.GetInt("ability3")).ToString().ToUpper() + "] to activate toggle ability";
        else tutorialTexts[5].text = "[E] to activate toggle ability";


        if (PlayerPrefs.HasKey("jump")) tutorialTexts[6].text = "[" + ((KeyCode)PlayerPrefs.GetInt("jump")).ToString().ToUpper() + "] to jump";
        else tutorialTexts[6].text = "[Space] to jump";
    }
}
