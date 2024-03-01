using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CorralZone : MonoBehaviour
{
    private ObjectiveManager objectiveTracker;

    [SerializeField]
    private int enemyCount = 0;

    void Start()
    {
        objectiveTracker = FindObjectOfType<ObjectiveManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if(obj.GetComponent<EnemyHealth>() != null)
        {
            enemyCount += 1;
            obj.GetComponent<EnemyHealth>().enemyDied += EnemyDied;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.GetComponent<EnemyHealth>() != null)
        {
            enemyCount -= 1;
            obj.GetComponent<EnemyHealth>().enemyDied -= EnemyDied;
        }
    }

    private void UpdateTracker(float amount, string tracker = "EnemiesKilledInZone")
    {
        objectiveTracker.UpdateStat(tracker, amount);
    }

    private void EnemyDied()
    {
        enemyCount -= 1;
        UpdateTracker(1);
    }
}
