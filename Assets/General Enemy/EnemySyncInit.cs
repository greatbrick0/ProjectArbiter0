using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coherence;
using Coherence.Toolkit;

[RequireComponent(typeof(CoherenceSync))]
public class EnemySyncInit : MonoBehaviour
{
    private CoherenceSync sync;

    public IEnumerator Init(EnemySpawner enemySpawner)
    {
        yield return new WaitForSeconds(0.5f);
        sync = GetComponent<CoherenceSync>();
        print("hit");
        sync.SendCommand<EnemySyncInit>(nameof(SetReferences), MessageTarget.All, enemySpawner.gameObject);
    }

    [Command]
    public void SetReferences(GameObject enemySpawner)
    {
        print("set");
        if (GetComponent<EnemyBrain>() != null) GetComponent<EnemyBrain>().playerTracker = enemySpawner.GetComponent<EnemySpawner>().playerTracker;
        if (GetComponent<EnemyHealth>() != null) GetComponent<EnemyHealth>().enemyDied += enemySpawner.GetComponent<EnemySpawner>().IncrementKillStat;
    }
}
