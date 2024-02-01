using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    private void Start()
    {
        resolutionsArray = Screen.resolutions;

        resolutionsDropdown.ClearOptions();
        List<string> resolutionsList = new List<string>();

        int initialResolution = 0;
        for (int i = 0; i < resolutionsArray.Length; i++)
        {
            resolutionsList.Add(resolutionsArray[i].width + " x " + resolutionsArray[i].height);

            if (resolutionsArray[i].width == Screen.currentResolution.width && resolutionsArray[i].height == Screen.currentResolution.height)
            {
                initialResolution = i;
            }
        }

        resolutionsDropdown.AddOptions(resolutionsList);

        resolutionsDropdown.value = initialResolution;
        resolutionsDropdown.RefreshShownValue();


        fpsDisplay.text = "Unlimited";
    }

    //DISPLAY SETTINGS
    [SerializeField] Camera gameCamera;
    [SerializeField] TMP_Dropdown resolutionsDropdown;
    [SerializeField] TextMeshProUGUI fpsDisplay;
    Resolution[] resolutionsArray;
    public void SetResolution(int resolutionIndex)
    {
        Resolution chosenResolution = resolutionsArray[resolutionIndex];
        Screen.SetResolution(chosenResolution.width, chosenResolution.height, Screen.fullScreenMode);
    }

    public void SetGraphics(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetFPSLimit(float limit)
    {
        if (limit == 301)
        {
            Application.targetFrameRate = -1;
            fpsDisplay.text = "Unlimited";
        }
        else
        {
            Application.targetFrameRate = (int)limit;
            fpsDisplay.text = limit.ToString();
        }
    }

    public void SetFOV(int fov)
    {
        gameCamera.fieldOfView = fov;
    }


    //AUDIO SETTINGS
    [SerializeField] TextMeshProUGUI masterDisplay;
    [SerializeField] TextMeshProUGUI soundsDisplay;
    [SerializeField] TextMeshProUGUI musicDisplay;
    [SerializeField] TextMeshProUGUI menuDisplay;
    public void SetMasterVolume(float volumeLevel)
    {
        AudioManager.instance.masterVolume = volumeLevel;
        masterDisplay.text = Mathf.Round(volumeLevel * 100).ToString();
    }
    public void SetSoundsVolume(float volumeLevel)
    {
        AudioManager.instance.SFXVolume = volumeLevel;
        AudioManager.instance.ambienceVolume = volumeLevel;
        soundsDisplay.text = Mathf.Round(volumeLevel * 100).ToString();
    }
    public void SetMusicVolume(float volumeLevel)
    {
        AudioManager.instance.musicVolume = volumeLevel;
        musicDisplay.text = Mathf.Round(volumeLevel * 100).ToString();
    }
    public void SetMenuVolume(float volumeLevel)
    {
        //Doesn't exist currently
        menuDisplay.text = Mathf.Round(volumeLevel * 100).ToString();
    }
}
