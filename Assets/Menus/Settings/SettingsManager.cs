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


        //Loading Default Settings
        if (PlayerPrefs.HasKey("Sensitivity")) LoadSensitivity(PlayerPrefs.GetFloat("Sensitivity"));
        if (PlayerPrefs.HasKey("Graphics Quality")) LoadGraphics(PlayerPrefs.GetInt("Graphics Quality"));
        if (PlayerPrefs.HasKey("FPS Limit")) LoadFPSLimit(PlayerPrefs.GetFloat("Sensitivity"));
        if (PlayerPrefs.HasKey("Master Volume")) LoadMasterVolume(PlayerPrefs.GetFloat("Master Volume"));
        if (PlayerPrefs.HasKey("Sounds Volume")) LoadSoundsVolume(PlayerPrefs.GetFloat("Sounds Volume"));
        if (PlayerPrefs.HasKey("Music Volume")) LoadMusicVolume(PlayerPrefs.GetFloat("Music Volume"));
    }


    //CONTROLS SETTINGS
    [SerializeField] Slider sensSlider;
    [SerializeField] TMP_InputField sensInputField;
    [SerializeField] PlayerInput playerInput;

    public void SetSensitivity(float sens) //Used by Slider
    {
        Mathf.Clamp(sens, 0.1f, 100);
        sens = Mathf.Round(sens * 10.0f) * 0.1f;

        playerInput.mouseXSens = sens / 50;
        playerInput.mouseYSens = sens / 50;

        sensInputField.text = sens.ToString();

        PlayerPrefs.SetFloat("Sensitivity", sens);
    }
    public void SetSensitivity(string sensString) //Used by InputField
    {
        float.TryParse(sensString, out float sens);
        Mathf.Clamp(sens, 0.1f, 100);
        sens = Mathf.Round(sens * 10.0f) * 0.1f;

        playerInput.mouseXSens = sens / 50;
        playerInput.mouseYSens = sens / 50;

        sensSlider.value = sens;

        PlayerPrefs.SetFloat("Sensitivity", sens);
    }
    private void LoadSensitivity(float sens)
    {
        sensInputField.text = sens.ToString();
        sensSlider.value = sens;
    }


    //DISPLAY SETTINGS
    [SerializeField] TMP_Dropdown resolutionsDropdown;
    [SerializeField] Slider fpsSlider;
    [SerializeField] TMP_InputField fpsInputField;
    [SerializeField] TMP_Dropdown graphicsDropdown;
    Resolution[] resolutionsArray;

    public void SetResolution(int resolutionIndex)
    {
        Resolution chosenResolution = resolutionsArray[resolutionIndex];
        Screen.SetResolution(chosenResolution.width, chosenResolution.height, Screen.fullScreenMode);
    }

    public void SetGraphics(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel);

        PlayerPrefs.SetInt("Graphics Quality", qualityLevel);
    }
    private void LoadGraphics(int qualityLevel)
    {
        graphicsDropdown.value = qualityLevel;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetFPSLimit(float limit) //Used by Slider
    {
        Application.targetFrameRate = (int)limit;

        fpsInputField.text = limit.ToString();

        PlayerPrefs.SetInt("FPS Limit", (int)limit);
    }
    public void SetFPSLimit(string limitString) //Used by InputField
    {
        int.TryParse(limitString, out int limit);
        Mathf.Clamp(limit, 30, 300);

        Application.targetFrameRate = limit;

        fpsSlider.value = limit;

        PlayerPrefs.SetInt("FPS Limit", limit);
    }
    private void LoadFPSLimit(float limit)
    {
        fpsInputField.text = limit.ToString();
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

        PlayerPrefs.SetFloat("Master Volume", volumeLevel);
    }
    public void SetMasterVolume(string volumeLevelString) //Used by InputField
    {
        float.TryParse(volumeLevelString, out float volumeLevel);
        Mathf.Clamp(volumeLevel, 0, 100);

        AudioManager.instance.masterVolume = volumeLevel / 100;

        masterSlider.value = volumeLevel;

        PlayerPrefs.SetFloat("Master Volume", volumeLevel);
    }
    private void LoadMasterVolume(float volumeLevel)
    {
        masterInputField.text = volumeLevel.ToString();
        masterSlider.value = volumeLevel;
    }

    public void SetSoundsVolume(float volumeLevel) //Used by Slider
    {
        AudioManager.instance.SFXVolume = volumeLevel / 100;
        AudioManager.instance.ambienceVolume = volumeLevel / 100;

        soundsInputField.text = volumeLevel.ToString();

        PlayerPrefs.SetFloat("Sounds Volume", volumeLevel);
    }
    public void SetSoundsVolume(string volumeLevelString) //Used by InputField
    {
        float.TryParse(volumeLevelString, out float volumeLevel);
        Mathf.Clamp(volumeLevel, 0, 100);

        AudioManager.instance.SFXVolume = volumeLevel / 100;
        AudioManager.instance.ambienceVolume = volumeLevel / 100;

        soundsSlider.value = volumeLevel;

        PlayerPrefs.SetFloat("Sounds Volume", volumeLevel);
    }
    private void LoadSoundsVolume(float volumeLevel)
    {
        soundsInputField.text = volumeLevel.ToString();
        soundsSlider.value = volumeLevel;
    }

    public void SetMusicVolume(float volumeLevel) //Used by Slider
    {
        AudioManager.instance.musicVolume = volumeLevel / 100;

        musicInputField.text = volumeLevel.ToString();

        PlayerPrefs.SetFloat("Music Volume", volumeLevel);
    }
    public void SetMusicVolume(string volumeLevelString) //Used by InputField
    {
        float.TryParse(volumeLevelString, out float volumeLevel);
        Mathf.Clamp(volumeLevel, 0, 100);

        AudioManager.instance.musicVolume = volumeLevel / 100;

        musicSlider.value = volumeLevel;

        PlayerPrefs.SetFloat("Music Volume", volumeLevel);
    }
    private void LoadMusicVolume(float volumeLevel)
    {
        musicInputField.text = volumeLevel.ToString();
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


    //DEFAULTS
    public void RestoreDefaults()
    {
        //Controls
        SetSensitivity(15);
        SetSensitivity("15");
        //Display
        SetGraphics(2);
        SetFullscreen(true);
        SetFPSLimit(300);
        SetFPSLimit("300");
        //Audio
        SetMasterVolume(100);
        SetMasterVolume("100");
        SetSoundsVolume(100);
        SetSoundsVolume("100");
        SetMusicVolume(100);
        SetMusicVolume("100");
        SetMenuVolume(100);
        SetMenuVolume("100");
    }
}
