using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coherence;
using Coherence.Toolkit;
using Coherence.Connection;
using System;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField]
    private CoherenceBridge bridge;
    private CoherenceClientConnectionManager clientsData;
    [field: SerializeField]
    public int playerCount { get; private set; }
    [SerializeField]
    public int spectatorCount = 0;
    [SerializeField]
    public List<PlayerClient> playerClientList;

    [Serializable] public class PlayerClient
    {
        public PlayerClient(ClientID newId, GameObject newObj)
        {
            id = newId;
            playerObj = newObj;
            if(playerObj != null) playerObjName = playerObj.name;
            isSpectator = false;
        }
        public GameObject playerObj { get; private set; }
        [SerializeField]
        private string playerObjName;
        [SerializeField]
        public bool isSpectator = false;
        [field: SerializeField]
        public ClientID id { get; private set; }
    }

    private void Awake()
    {
        clientsData = bridge.ClientConnections;
    }

    private void Update()
    {
        if(playerCount != clientsData.ClientConnectionCount)
        {
            playerCount = clientsData.ClientConnectionCount;
            UpdatePlayerClientList();
        }
    }

    private void UpdatePlayerClientList()
    {
        playerClientList = new List<PlayerClient>();
        foreach(CoherenceClientConnection ii in clientsData.GetAllClients())
        {
            playerClientList.Add(new PlayerClient(ii.ClientId, ii.GameObject));
        }
    }
}
