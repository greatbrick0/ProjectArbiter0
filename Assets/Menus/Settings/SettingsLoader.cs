using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsLoader : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("Graphics Quality")) QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Graphics Quality"));
        if (PlayerPrefs.HasKey("FPS Limit")) Application.targetFrameRate = PlayerPrefs.GetInt("FPS Limit");
        if (PlayerPrefs.HasKey("Master Volume")) AudioManager.instance.masterVolume = PlayerPrefs.GetFloat("Master Volume") / 100;
        if (PlayerPrefs.HasKey("Sounds Volume")) AudioManager.instance.SFXVolume = PlayerPrefs.GetFloat("Sounds Volume") / 100;
        if (PlayerPrefs.HasKey("Sounds Volume")) AudioManager.instance.ambienceVolume = PlayerPrefs.GetFloat("Sounds Volume") / 100;
        if (PlayerPrefs.HasKey("Music Volume")) AudioManager.instance.musicVolume = PlayerPrefs.GetFloat("Music Volume") / 100;
    }
}
