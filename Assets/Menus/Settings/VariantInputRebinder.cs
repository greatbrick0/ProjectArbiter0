using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VariantInputRebinder : MonoBehaviour
{
    [SerializeField] VariantSettingsManager settingsManager;
    public string activeType;
    public string activeInputName;
    public TextMeshProUGUI activeButtonText;

    public void SetActiveType(string type)
    {
        activeType = type;
    }
    public void SetActiveInputName(string name)
    {
        activeInputName = name;
    }
    public void SetActiveButtonText(TextMeshProUGUI buttonText)
    {
        activeButtonText = buttonText;
    }

    private void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Event detectInput = Event.current;
            if (detectInput.isKey)
            {
                if (detectInput.keyCode == KeyCode.Escape)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    activeButtonText.text = detectInput.keyCode.ToString();
                    settingsManager.UpdateBind(activeInputName, (int)detectInput.keyCode);
                    gameObject.SetActive(false);
                }
            }
            if (detectInput.isMouse)
            {
                switch (detectInput.button)
                {
                    case 0:
                        activeButtonText.text = "Left Click";
                        settingsManager.UpdateBind(activeInputName, 323);
                        break;

                    case 1:
                        activeButtonText.text = "Right Click";
                        settingsManager.UpdateBind(activeInputName, 324);
                        break;

                    case 2:
                        activeButtonText.text = "Middle Click";
                        settingsManager.UpdateBind(activeInputName, 325);
                        break;

                    default:
                        break;
                }
                gameObject.SetActive(false);
            }
        }
    }
}
