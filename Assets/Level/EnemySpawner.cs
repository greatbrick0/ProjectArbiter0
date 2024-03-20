using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private PlayerTracker playerTracker;
    [SerializeField]
    private ObjectiveManager objectiveTracker;
    [SerializeField]
    private List<GameObject> enemyTypes;
    private GameObject instanceRef;

    public void SpawnEnemy(Vector3 pos, int typeIndex)
    {
        SpawnEnemy(pos, enemyTypes[typeIndex]);
    }

    public void SpawnEnemy(Vector3 pos, GameObject typePrefab)
    {
        if (!playerTracker.IsPrimaryClient()) return;

        instanceRef = Instantiate(typePrefab);
        instanceRef.transform.parent = transform;
        instanceRef.transform.position = pos;
        instanceRef.GetComponent<EnemyBrain>().playerTracker = playerTracker;
        instanceRef.GetComponent<EnemyHealth>().enemyDied += IncrementKillStat;
    }

    public void IncrementKillStat()
    {
        objectiveTracker.UpdateStat("EnemiesKilled", 1);
    }
}
