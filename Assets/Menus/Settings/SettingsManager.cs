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
    }


    //CONTROLS SETTINGS
    [SerializeField] Slider sensSlider;
    [SerializeField] TMP_InputField sensInputField;

    public void SetSensitivity(float sens) //Used by Slider
    {
        Mathf.Clamp(sens, 0.1f, 100);
        sens = Mathf.Round(sens * 10.0f) * 0.1f;

        //Adjust player sens

        sensInputField.text = sens.ToString();
    }
    public void SetSensitivity(string sensString) //Used by InputField
    {
        float.TryParse(sensString, out float sens);
        Mathf.Clamp(sens, 0.1f, 100);
        sens = Mathf.Round(sens * 10.0f) * 0.1f;

        //Adjust player sens

        sensSlider.value = sens;
    }


    //DISPLAY SETTINGS
    [SerializeField] TMP_Dropdown resolutionsDropdown;
    [SerializeField] Slider fpsSlider;
    [SerializeField] TMP_InputField fpsInputField;
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

    public void SetFPSLimit(float limit) //Used by Slider
    {
        Application.targetFrameRate = (int)limit;

        fpsInputField.text = limit.ToString();
    }
    public void SetFPSLimit(string limitString) //Used by InputField
    {
        int.TryParse(limitString, out int limit);
        Mathf.Clamp(limit, 30, 300);

        Application.targetFrameRate = limit;

        fpsSlider.value = limit;
    }


    //AUDIO SETTINGS
    [SerializeField] Slider masterSlider;
    [SerializeField] TMP_InputField masterInputField;
    [SerializeField] Slider soundsSlider;
    [SerializeField] TMP_InputField soundsInputField;
    [SerializeField] Slider musicSlider;
    [SerializeField] TMP_InputField musicInputField;
    [SerializeField] Slider menuSlider;
    [SerializeField] TMP_InputField menuInputField;

    public void SetMasterVolume(float volumeLevel) //Used by Slider
    {
        AudioManager.instance.masterVolume = volumeLevel / 100;

        masterInputField.text = volumeLevel.ToString();
    }
    public void SetMasterVolume(string volumeLevelString) //Used by InputField
    {
        float.TryParse(volumeLevelString, out float volumeLevel);
        Mathf.Clamp(volumeLevel, 0, 100);

        AudioManager.instance.masterVolume = volumeLevel;

        masterSlider.value = volumeLevel;
    }

    public void SetSoundsVolume(float volumeLevel) //Used by Slider
    {
        AudioManager.instance.SFXVolume = volumeLevel / 100;
        AudioManager.instance.ambienceVolume = volumeLevel / 100;

        soundsInputField.text = volumeLevel.ToString();
    }
    public void SetSoundsVolume(string volumeLevelString) //Used by InputField
    {
        float.TryParse(volumeLevelString, out float volumeLevel);
        Mathf.Clamp(volumeLevel, 0, 100);

        AudioManager.instance.SFXVolume = volumeLevel / 100;
        AudioManager.instance.ambienceVolume = volumeLevel / 100;

        soundsSlider.value = volumeLevel;
    }

    public void SetMusicVolume(float volumeLevel) //Used by Slider
    {
        AudioManager.instance.musicVolume = volumeLevel / 100;

        musicInputField.text = volumeLevel.ToString();
    }
    public void SetMusicVolume(string volumeLevelString) //Used by InputField
    {
        float.TryParse(volumeLevelString, out float volumeLevel);
        Mathf.Clamp(volumeLevel, 0, 100);

        AudioManager.instance.musicVolume = volumeLevel;

        musicSlider.value = volumeLevel;
    }

    public void SetMenuVolume(float volumeLevel) //Used by Slider
    {
        //Does not exist yet

        menuInputField.text = volumeLevel.ToString();
    }
    public void SetMenuVolume(string volumeLevelString) //Used by InputField
    {
        float.TryParse(volumeLevelString, out float volumeLevel);
        Mathf.Clamp(volumeLevel, 0, 100);

        //Does not exist yet

        menuSlider.value = volumeLevel;
    }

}
