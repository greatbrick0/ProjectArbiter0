using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseMenuConnecter : MonoBehaviour
{
    [SerializeField]
    public GameObject pauseMenu;
    [SerializeField]
    public Button resumeButton;
    [SerializeField]
    public SettingsManager settingsManager;

    private void Start()
    {
        resumeButton.onClick.AddListener(delegate { pauseMenu.SetActive(false); });
        pauseMenu.SetActive(false);
    }

    public void PauseMenuActive(bool value)
    {
        pauseMenu.SetActive(value);
    }

}
