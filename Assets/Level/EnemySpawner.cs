using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [field: SerializeField]
    public PlayerTracker playerTracker { get; private set; }
    [SerializeField]
    private ObjectiveManager objectiveTracker;
    [SerializeField]
    private List<GameObject> enemyTypes;
    private GameObject instanceRef;

    public void SpawnEnemy(Vector3 pos, int typeIndex)
    {
        SpawnEnemy(pos, enemyTypes[typeIndex]);
    }

    public EnemyHealth SpawnEnemy(Vector3 pos, GameObject typePrefab)
    {
        if (!(playerTracker.IsPrimaryClient() || typePrefab.GetComponent<EnemySyncInit>() == null)) return null;

        instanceRef = Instantiate(typePrefab);
        instanceRef.transform.parent = transform;
        instanceRef.transform.position = pos;
        if (instanceRef.GetComponent<EnemySyncInit>() != null) StartCoroutine(instanceRef.GetComponent<EnemySyncInit>().Init(this));
        else
        {
            instanceRef.GetComponent<EnemyBrain>().playerTracker = playerTracker;
            instanceRef.GetComponent<EnemyHealth>().enemyDied += IncrementKillStat;
        }
        return instanceRef.GetComponent<EnemyHealth>();
    }

    public void IncrementKillStat()
    {
        print("Incrementing kill stat");
        objectiveTracker.UpdateStat("EnemiesKilled", 1);
    }
}
