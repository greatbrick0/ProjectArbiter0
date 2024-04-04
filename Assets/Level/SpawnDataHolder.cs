using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageDetails;

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
    [Tooltip("The amount of enemiess that will be spawned before stopping. Setting to 0 will disable the limit.")]
    private int maxEnemies;
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

    [Serializable] public class EnemySpot
    {
        public Transform position; // Easier to assign a transform of an empty gameobject than type out vector3s
        public List<GameObject> supportedTypes;
        public float weight = 1;
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

        if (spawnedEnemyCount >= maxEnemies && maxEnemies > 0) continueSpawning = false;
        if (spawnedEnemyCount >= maxEnemies && maxEnemies > 0) continueSpawning = false;
    }

    public void InitialWaveSpawn()
    {
        foreach(EnemySpot ii in initialSpawns)
        {
            foreach (Transform child in ii.position) // SPAWNS USING CHILD GAMEOBJECTS
            {
                // Now uses random instead of 0
                enemySpawner.SpawnEnemy(child.position, ii.supportedTypes[(int)UnityEngine.Random.Range(0, ii.supportedTypes.Count)]);
                spawnedEnemyCount += 1;
            }
        }
        if (spawnedEnemyCount < maxEnemies || maxEnemies == 0) continueSpawning = true;
    }

    public void SpawnSingleEnemy()
    {
        spawnedEnemyCount += 1;
        EnemySpot chosenSpot = continuousSpawns[Mathf.FloorToInt(spawningDuration * 0.71f) % continuousSpawns.Count];
        GameObject chosenType = chosenSpot.supportedTypes[Mathf.FloorToInt(spawningDuration * 1.22f) % chosenSpot.supportedTypes.Count];
        enemySpawner.SpawnEnemy(chosenSpot.position.position + transform.position, chosenType);
    }

    private void SpawnGroupEnemies()
    {
        spawnedEnemyCount += 1;

        foreach (EnemySpot chosenSpot in continuousSpawns)
        {
            foreach (Transform child in chosenSpot.position)
            {
                GameObject chosenType = chosenSpot.supportedTypes[(int)UnityEngine.Random.Range(0, chosenSpot.supportedTypes.Count)];
                enemySpawner.SpawnEnemy(child.position, chosenType);
            }
        }
    }

    public void WipeEnemies()
    {
        if (playerTracker.IsPrimaryClient())
        {
            for (int ii = 0; ii < enemySpawner.transform.childCount; ii++)
            {
                if (enemySpawner.transform.GetChild(ii).GetComponent<EnemyHealth>() != null)
                {
                    enemySpawner.transform.GetChild(ii).GetComponent<EnemyHealth>().enemyDied -= enemySpawner.IncrementKillStat;
                    enemySpawner.transform.GetChild(ii).GetComponent<EnemyHealth>().TakeDamage(999, DamageSource.Environment, DamageSpot.Body, DamageElement.Normal);
                }
            }
        }
    }

    public void DisableSpawning()
    {
        continueSpawning = false;
    }
}
