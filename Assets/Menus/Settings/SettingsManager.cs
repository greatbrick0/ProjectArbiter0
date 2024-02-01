using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Camera gameCamera;
    
    //DISPLAY SETTINGS
    public void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreenMode);
    }

    public void SetGraphics(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel);
    }

    public void SetFullscreen(FullScreenMode fullscreenMode)
    {
        Screen.fullScreenMode = fullscreenMode;
    }

    public void SetFPSLimit(int limit)
    {
        Application.targetFrameRate = limit;
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
