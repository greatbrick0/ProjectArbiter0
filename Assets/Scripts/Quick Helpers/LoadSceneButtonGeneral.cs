using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButtonGeneral : MonoBehaviour
{
    public void LoadScene(string sceneToLoadName)
    {
        if (sceneToLoadName == "StartMenuScene")
        {
            if (GameObject.Find("Lobby Size Limiter") != null) Destroy(GameObject.Find("Lobby Size Limiter"));
        }
        SceneManager.LoadScene(sceneToLoadName);
    }
}
