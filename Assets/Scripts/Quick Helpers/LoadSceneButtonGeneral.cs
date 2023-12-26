using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButtonGeneral : MonoBehaviour
{
    public void LoadScene(string sceneToLoadName)
    {
        SceneManager.LoadScene(sceneToLoadName);
    }
}
