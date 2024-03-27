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
        yield return new WaitForEndOfFrame();
        sync = GetComponent<CoherenceSync>();
        sync.SendCommand<EnemySyncInit>(nameof(SetReferences), MessageTarget.All, enemySpawner.gameObject.name);
    }

    [Command]
    public void SetReferences(string enemySpawner)
    {
        print("set");
        EnemySpawner spawner = GameObject.Find(enemySpawner).GetComponent<EnemySpawner>();
        if (GetComponent<EnemyBrain>() != null) GetComponent<EnemyBrain>().playerTracker = spawner.playerTracker;
        if (GetComponent<EnemyHealth>() != null) GetComponent<EnemyHealth>().enemyDied += spawner.IncrementKillStat;
    }
}
