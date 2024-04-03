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
    private CoherenceClientConnection currentClient;
    [SerializeField]
    private List<GameObject> playerList;
    [field: SerializeField]
    public int playerCount { get; private set; }
    [SerializeField]
    public int spectatorCount { get; private set; } = 0;

    private void Awake()
    {
        clientsData = bridge.ClientConnections;
        currentClient = clientsData.GetMine();
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
        currentClient = clientsData.GetMine();
    }

    public GameObject GetPlayerObject(int index)
    {
        return playerList[index];
    }

    public bool IsPrimaryClient()
    {
        return currentClient.GameObject == playerList[0];
    }

    public void PlayerDied()
    {
        spectatorCount += 1;
        if(spectatorCount >= playerCount)
        {
            print("You Lose!");
        }
    }

    [ContextMenu("Respawn Players")]
    public void RespawnPlayers()
    {
        Vector3 spawnPoint = Vector3.zero;
        foreach(GameObject ii in playerList)
        {
            if(!ii.GetComponent<PlayerHealth>().playerDead)
            {
                spawnPoint = ii.transform.position;
                break;
            }
        }
        foreach (GameObject ii in playerList)
        {
            ii.GetComponent<PlayerHealth>().AttemptRespawn(spawnPoint);
            spectatorCount = 0;
        }
    }
}
