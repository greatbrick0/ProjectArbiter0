using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SettingsLoader : MonoBehaviour
{
    private void Start()
    {
        LoadGraphicsSettings();
        LoadVolumeSettings();
    }

    public static void LoadGraphicsSettings()
    {
        if (PlayerPrefs.HasKey("Graphics Quality")) QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Graphics Quality"));
        if (PlayerPrefs.HasKey("FPS Limit")) Application.targetFrameRate = PlayerPrefs.GetInt("FPS Limit");
        if (PlayerPrefs.HasKey("FullScreen")) Screen.fullScreen = (PlayerPrefs.GetInt("FullScreen") == 1);
        if (PlayerPrefs.HasKey("Resolutions Width") && PlayerPrefs.HasKey("Resolutions Height"))
        {
            Resolution resolution = new Resolution();
            resolution.width = PlayerPrefs.GetInt("Resolutions Width");
            resolution.height = PlayerPrefs.GetInt("Resolutions Height");
            if (Screen.resolutions.Contains(resolution))
            {
                Screen.SetResolution(PlayerPrefs.GetInt("Resolutions Width"), PlayerPrefs.GetInt("Resolutions Height"), Screen.fullScreenMode);
            }
            else
            {
                Debug.LogError("Resolution is not valid");
                Screen.SetResolution(Screen.resolutions[^1].width, Screen.resolutions[^1].height, Screen.fullScreenMode);
            }
        }
    }

    public static void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("Master Volume")) AudioManager.instance.masterVolume = PlayerPrefs.GetFloat("Master Volume") / 100;
        if (PlayerPrefs.HasKey("Sounds Volume"))
        {
            AudioManager.instance.SFXVolume = PlayerPrefs.GetFloat("Sounds Volume") / 100;
            AudioManager.instance.ambienceVolume = PlayerPrefs.GetFloat("Sounds Volume") / 100;
        }
        if (PlayerPrefs.HasKey("Music Volume")) AudioManager.instance.musicVolume = PlayerPrefs.GetFloat("Music Volume") / 100;
    }
}
