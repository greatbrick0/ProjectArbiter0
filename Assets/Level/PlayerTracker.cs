using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coherence;
using Coherence.Toolkit;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField]
    private CoherenceBridge bridge;
    private CoherenceClientConnectionManager clientsData;
    [SerializeField]
    private List<GameObject> playerList;
    [field: SerializeField]
    public int playerCount { get; private set; }
    [SerializeField]
    public int spectatorCount = 0;

    private void Awake()
    {
        clientsData = bridge.ClientConnections;
    }

    private void Update()
    {
        if (playerCount != clientsData.ClientConnectionCount) RefreshPlayerList();
        playerCount = clientsData.ClientConnectionCount;
    }

    private void RefreshPlayerList()
    {
        playerList.Clear();
        foreach(CoherenceClientConnection ii in clientsData.GetAllClients())
        {
            playerList.Add(ii.GameObject);
        }
    }

    public GameObject GetPlayerObject(int index)
    {
        return playerList[index];
    }
}
