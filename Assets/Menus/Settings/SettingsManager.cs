using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Camera gameCamera;
    
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
}
