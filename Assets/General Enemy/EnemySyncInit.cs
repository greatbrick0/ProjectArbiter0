using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coherence;
using Coherence.Toolkit;

[RequireComponent(typeof(CoherenceSync))]
public class EnemySyncInit : MonoBehaviour
{
    private CoherenceSync sync;

    public void Init(EnemySpawner enemySpawner)
    {
        sync = GetComponent<CoherenceSync>();
        sync.SendCommand<EnemyBrain>(nameof(SetReferences), MessageTarget.All, enemySpawner.gameObject);
    }

    [Command("SetReferences", typeof(GameObject))]
    public void SetReferences(GameObject enemySpawner)
    {
        print("set");
        if (GetComponent<EnemyBrain>() != null) GetComponent<EnemyBrain>().playerTracker = enemySpawner.GetComponent<EnemySpawner>().playerTracker;
        if (GetComponent<EnemyHealth>() != null) GetComponent<EnemyHealth>().enemyDied += enemySpawner.GetComponent<EnemySpawner>().IncrementKillStat;
    }
}
