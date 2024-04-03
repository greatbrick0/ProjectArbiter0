using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyInProgressCheck : MonoBehaviour
{
    void Start()
    {
        if (GameObject.Find("Managers") != null)
        {
            if (GameObject.Find("Managers").GetComponent<ObjectiveManager>().gameStarted)
            {
                GameObject.Find("PlayerHUD").transform.GetChild(1).gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                gameObject.GetComponent<PlayerInput>().enabled = false;
                transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }
}
