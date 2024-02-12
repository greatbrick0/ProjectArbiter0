using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDataHolder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    [SerializeField] 
    [Tooltip("The enemies that will be spawned as soon as the room is loaded. Only spawns the first supported type for each spot. ")]
    private List<EnemySpot> initialSpawns;
    [SerializeField]
    [Tooltip("Whether or not this room is the first room the players visit. Other rooms must have their logic started by some other source, such as ObjectiveManager. ")]
    private bool firstRoom;
    [SerializeField]
    private List<EnemySpot> continuousSpawns;

    [Serializable] public class EnemySpot
    {
        public Vector3 position;
        public List<GameObject> supportedTypes;
    }

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        if (firstRoom) InitialWaveSpawn();
    }

    public void InitialWaveSpawn()
    {
        foreach(EnemySpot ii in initialSpawns)
        {
            enemySpawner.SpawnEnemy(ii.position + transform.position, ii.supportedTypes[0]);
            print("should: " + (ii.position + transform.position));
        }
    }
}
