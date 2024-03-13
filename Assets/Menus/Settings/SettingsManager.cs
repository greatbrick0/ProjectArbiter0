using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    List<string> actionKeys = new List<string>(){ "forward", "backward", "left", "right", "jump", "reload", "shoot", "aim", "ability1", "ability2", "ability3" };
    [HideInInspector] public PlayerInput playerInput;

    [Header("Control Settings")]
    [SerializeField] Slider sensSlider;
    [SerializeField] TMP_InputField sensInputField;
    [SerializeField] List<TextMeshProUGUI> bindButtonTextList;

    [Header("Display Settings")]
    [SerializeField] TMP_Dropdown resolutionsDropdown;
    [SerializeField] Slider fpsSlider;
    [SerializeField] TMP_InputField fpsInputField;
    [SerializeField] TMP_Dropdown graphicsDropdown;
    Resolution[] resolutionsArray;

    [Header("Audio Settings")]
    [SerializeField] Slider masterSlider;
    [SerializeField] TMP_InputField masterInputField;
    [SerializeField] Slider soundsSlider;
    [SerializeField] TMP_InputField soundsInputField;
    [SerializeField] Slider musicSlider;
    [SerializeField] TMP_InputField musicInputField;
    [SerializeField] Slider menuSlider;
    [SerializeField] TMP_InputField menuInputField;

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


        //Loading Saved Settings
        SettingsLoader.LoadGraphicsSettings();
        SettingsLoader.LoadVolumeSettings();

        if (PlayerPrefs.HasKey("Sensitivity")) LoadSensitivity(PlayerPrefs.GetFloat("Sensitivity"));

        for (int ii = 0; ii < actionKeys.Count; ii++)
        {
            if (PlayerPrefs.HasKey(actionKeys[ii])) 
                LoadBind(bindButtonTextList[ii], ((KeyCode)PlayerPrefs.GetInt(actionKeys[ii])).ToString());
        }

        if(playerInput != null) playerInput.LoadSettings();
    }

    public void SetSensitivity(float sens) //Used by Slider
    {
        Mathf.Clamp(sens, 0.1f, 100);
        sens = Mathf.Round(sens * 10.0f) * 0.1f;

        playerInput.mouseXSens = sens / 50;
        playerInput.mouseYSens = sens / 50;

        sensInputField.text = sens.ToString("#.0");

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
        sensInputField.text = sens.ToString("#.0");
        sensSlider.value = sens;
    }

    public void UpdateBind(string actionName, int keyCodeInt)
    {
        if (actionKeys.Contains(actionName))
        {
            PlayerPrefs.SetInt(actionName, keyCodeInt);
            if (playerInput != null) playerInput.LoadSettings();
        }
    }

    private void LoadBind(TextMeshProUGUI buttonText, string bind)
    {
        switch (bind)
        {
            case "Mouse0":
                buttonText.text = "Left Click";
                break;

            case "Mouse1":
                buttonText.text = "Right Click";
                break;

            case "Mouse2":
                buttonText.text = "Middle Click";
                break;

            default:
                buttonText.text = bind;
                break;
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution chosenResolution = resolutionsArray[resolutionIndex];
        Screen.SetResolution(chosenResolution.width, chosenResolution.height, Screen.fullScreenMode);

        PlayerPrefs.SetInt("Resolution Width", chosenResolution.width);
        PlayerPrefs.SetInt("Resolution Height", chosenResolution.height);
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
        PlayerPrefs.SetInt("FullScreen", isFullscreen ? 1 : 0);
    }

    public void SetFPSLimit(float limit) //Used by Slider
    {
        Application.targetFrameRate = (int)limit;

        fpsInputField.text = limit.ToString();
        fpsSlider.value = limit;

        PlayerPrefs.SetInt("FPS Limit", (int)limit);
    }
    public void SetFPSLimit(string limitString) //Used by InputField
    {
        int.TryParse(limitString, out int limit);
        Mathf.Clamp(limit, 30, 300);

        SetFPSLimit(limit);
    }
    private void LoadFPSLimit(float limit)
    {
        fpsInputField.text = limit.ToString();
        fpsSlider.value = limit;
    }

    public void SetMasterVolume(float volumeLevel) //Used by Slider
    {
        AudioManager.instance.masterVolume = volumeLevel / 100;

        masterInputField.text = volumeLevel.ToString();
        masterSlider.value = volumeLevel;

        PlayerPrefs.SetFloat("Master Volume", volumeLevel);
    }
    public void SetMasterVolume(string volumeLevelString) //Used by InputField
    {
        float.TryParse(volumeLevelString, out float volumeLevel);
        Mathf.Clamp(volumeLevel, 0, 100);

        SetMasterVolume(volumeLevel);
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
        soundsSlider.value = volumeLevel;

        PlayerPrefs.SetFloat("Sounds Volume", volumeLevel);
    }
    public void SetSoundsVolume(string volumeLevelString) //Used by InputField
    {
        float.TryParse(volumeLevelString, out float volumeLevel);
        Mathf.Clamp(volumeLevel, 0, 100);

        SetSoundsVolume(volumeLevel);
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
        musicSlider.value = volumeLevel;

        PlayerPrefs.SetFloat("Music Volume", volumeLevel);
    }
    public void SetMusicVolume(string volumeLevelString) //Used by InputField
    {
        float.TryParse(volumeLevelString, out float volumeLevel);
        Mathf.Clamp(volumeLevel, 0, 100);

        SetMusicVolume(volumeLevel);
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
        menuSlider.value = volumeLevel;
    }
    public void SetMenuVolume(string volumeLevelString) //Used by InputField
    {
        float.TryParse(volumeLevelString, out float volumeLevel);
        Mathf.Clamp(volumeLevel, 0, 100);

        SetMenuVolume(volumeLevel);
    }


    //DEFAULTS
    public void RestoreDefaults()
    {
        //Controls
        SetSensitivity(15);
        UpdateBind("forward", 119);
        LoadBind(bindButtonTextList[0], "W");
        UpdateBind("backward", 115);
        LoadBind(bindButtonTextList[1], "S");
        UpdateBind("left", 97);
        LoadBind(bindButtonTextList[2], "A");
        UpdateBind("right", 100);
        LoadBind(bindButtonTextList[3], "D");
        UpdateBind("jump", 32);
        LoadBind(bindButtonTextList[4], "Space");
        UpdateBind("reload", 114);
        LoadBind(bindButtonTextList[5], "R");
        UpdateBind("shoot", 323);
        LoadBind(bindButtonTextList[6], "Mouse0");
        UpdateBind("aim", 324);
        LoadBind(bindButtonTextList[7], "Mouse1");
        UpdateBind("ability1", 113);
        LoadBind(bindButtonTextList[8], "Q");
        UpdateBind("ability2", 304);
        LoadBind(bindButtonTextList[9], "LeftShift");
        UpdateBind("ability3", 101);
        LoadBind(bindButtonTextList[10], "E");
        if(playerInput != null) playerInput.LoadSettings();
        //Display
        SetGraphics(2);
        LoadGraphics(2);
        SetFullscreen(true);
        SetFPSLimit(300);
        //Audio
        SetMasterVolume(100);
        SetSoundsVolume(100);
        SetMusicVolume(100);
        SetMenuVolume(100);
    }
}
