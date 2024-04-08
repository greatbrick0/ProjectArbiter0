using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageDetails;
using Unity.VisualScripting;

public class SpawnDataHolder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    PlayerTracker playerTracker;
    [SerializeField] 
    [Tooltip("The enemies that will be spawned as soon as the room is loaded. Spawns one of the supported types for each spot. ")]
    private List<EnemySpot> initialSpawns;
    [SerializeField]
    [Tooltip("Whether or not this room is the first room the players visit. Other rooms must have their logic started by some other source, such as ObjectiveManager. ")]
    private bool firstRoom;
    [SerializeField]
    private List<EnemySpot> continuousSpawns;
    private bool continueSpawning = false;
    [SerializeField]
    [Tooltip("The amount of enemies that will be spawned before stopping. Setting to 0 will disable the limit.")]
    private int maxEnemiesSpawned;
    private int spawnedEnemyCount = 0;
    [SerializeField]
    [Tooltip("The amount of that will pass before stopping spawning enemies, measured in seconds. Setting to 0 will disable the limit. ")]
    private float maxDuration;
    private float spawningDuration = 0.0f;
    private float timeSinceLastSpawn = 0.0f;
    [SerializeField]
    [Tooltip("The time until the next enemy spawn, measured in seconds. Value taken from seconds passed since spawning started. ")]
    private AnimationCurve spawnFrequency;
    [SerializeField]
    [Tooltip("The time spawnFrequency is extended over.")]
    private float frequencyCurveDuration;
    [SerializeField]
    [Tooltip("The frrequency used when frequencyCurveDuration has passed. ")]
    private float beyondCurveFrequency;
    private float timeUntilNextSpawn;
    [SerializeField]
    [Tooltip("The amount of enemies that can exist at the same time. Setting to 0 will disable the limit.")]
    private int maxExistingEnemies = 0;
    [SerializeField]
    private int existingEnemiesCount = 0;

    [Serializable] public class EnemySpot
    {
        public Transform position;
        public List<GameObject> supportedTypes;
        //public float weight = 1;
    }

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        playerTracker = FindObjectOfType<PlayerTracker>();
        if (firstRoom) InitialWaveSpawn();
    }

    private void Update()
    {
        if (!continueSpawning) return;
        spawningDuration += 1.0f * Time.deltaTime;

        timeSinceLastSpawn += 1.0f * Time.deltaTime;
        if(timeSinceLastSpawn >= timeUntilNextSpawn)
        {
            timeSinceLastSpawn = 0.0f;
            if (spawningDuration >= frequencyCurveDuration) timeUntilNextSpawn = beyondCurveFrequency;
            else timeUntilNextSpawn = spawnFrequency.Evaluate(spawningDuration / frequencyCurveDuration);
            SpawnGroupEnemies();
        }

        if (spawnedEnemyCount >= maxEnemiesSpawned && maxEnemiesSpawned > 0) continueSpawning = false;
        if (spawnedEnemyCount >= maxEnemiesSpawned && maxEnemiesSpawned > 0) continueSpawning = false;
    }

    public void InitialWaveSpawn()
    {
        foreach(EnemySpot ii in initialSpawns)
        {
            foreach (Transform child in ii.position)
            {
                SpawnEnemy(ii, child);
            }
        }
        if (spawnedEnemyCount < maxEnemiesSpawned || maxEnemiesSpawned == 0) continueSpawning = true;
    }

    private void SpawnGroupEnemies()
    {
        foreach (EnemySpot ii in continuousSpawns)
        {
            foreach (Transform child in ii.position)
            {
                SpawnEnemy(ii, child);
            }
        }
    }

    public void WipeEnemies()
    {
        if (playerTracker.IsPrimaryClient())
        {
            for (int ii = 0; ii < enemySpawner.transform.childCount; ii++)
            {
                EnemyHealth enemy = enemySpawner.transform.GetChild(ii).GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.enemyDied -= enemySpawner.IncrementKillStat;
                    enemy.TakeDamage(999, DamageSource.Environment, DamageSpot.Body, DamageElement.Normal);
                }
            }
        }
    }

    public void DisableSpawning()
    {
        continueSpawning = false;
    }

    private void DecreaseExistingEnemiesCount()
    {
        existingEnemiesCount -= 1;
    }

    private void SpawnEnemy(EnemySpot chosenSpot, Transform t)
    {
        if(existingEnemiesCount >= maxExistingEnemies && maxExistingEnemies != 0) return;

        GameObject chosenType = chosenSpot.supportedTypes[UnityEngine.Random.Range(0, chosenSpot.supportedTypes.Count)];
        EnemyHealth enemy = enemySpawner.SpawnEnemy(t.position, chosenType);
        spawnedEnemyCount += 1;
        existingEnemiesCount += 1;
        enemy.enemyDied += DecreaseExistingEnemiesCount;
    }
}
