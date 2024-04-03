using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreLobbySize : MonoBehaviour
{
    public int lobbySizeLimit;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void AdjustLobbySize(int lobbySize)
    {
        lobbySizeLimit = lobbySize;
    }
}
