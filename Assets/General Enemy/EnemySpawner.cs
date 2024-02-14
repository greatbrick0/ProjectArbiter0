using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private PlayerTracker playerTracker;
    [SerializeField]
    private List<GameObject> enemyTypes;
    private GameObject instanceRef;

    public void SpawnEnemy(Vector3 pos, int typeIndex)
    {
        instanceRef = Instantiate(enemyTypes[typeIndex]);
        instanceRef.transform.parent = transform;
        instanceRef.transform.position = pos;
        instanceRef.GetComponent<EnemyBrain>().playerTracker = playerTracker;
    }

    public void SpawnEnemy(Vector3 pos, GameObject typePrefab)
    {
        instanceRef = Instantiate(typePrefab);
        instanceRef.transform.parent = transform;
        instanceRef.transform.position = pos;
        instanceRef.GetComponent<EnemyBrain>().playerTracker = playerTracker;
    }
}
