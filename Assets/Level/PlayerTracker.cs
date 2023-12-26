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
        playerCount = clientsData.ClientConnectionCount;
    }
}
