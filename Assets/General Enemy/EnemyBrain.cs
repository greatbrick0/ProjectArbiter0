using Coherence.Toolkit;
using UnityEngine;

public abstract class EnemyBrain : MonoBehaviour
{
    [Sync]
    public PlayerTracker playerTracker;
    [SerializeField]
    [Tooltip("The period of time between state calculations. Measured in seconds. ")]
    private float cycleTime = 1.0f;
    private float timeSinceLastCycle = 0.0f;

    protected virtual void Update()
    {
        timeSinceLastCycle += 1.0f * Time.deltaTime;
        if(timeSinceLastCycle >= cycleTime)
        {
            timeSinceLastCycle = 0.0f;
            Logic();
        }
    }

    public abstract void Logic();

    /// <summary>
    /// Chooses a player from the list sensedPlayers to become the target of the pathfinding.
    /// </summary>
    /// <returns>Returns the player that has the best score. Score is determined between a mix of max health and distance.</returns>
    protected GameObject ChooseTargetPlayer()
    {
        if (playerTracker.playerCount == 0) return null;
        GameObject outputPlayer = null;
        float bestScore = 0;
        float dist;
        float score;

        for (int ii = 0; ii < playerTracker.playerCount; ii++)
        {
            if (playerTracker.GetPlayerObject(ii).GetComponent<PlayerHealth>().playerDead) continue;

            dist = (transform.position - playerTracker.GetPlayerObject(ii).transform.position).magnitude;
            score = Mathf.Pow(playerTracker.GetPlayerObject(ii).GetComponent<PlayerHealth>().maxMainHealth, 2) / dist; // score = maxHealth^2 / distance
            if (score >= bestScore)
            {
                outputPlayer = playerTracker.GetPlayerObject(ii);
                bestScore = score;
            }
        }

        return outputPlayer;
    }

    public void SetReferences(EnemySpawner enemySpawner)
    {
        print("set");
        playerTracker = enemySpawner.playerTracker;
        if(GetComponent<EnemyHealth>() != null)
        {
            GetComponent<EnemyHealth>().enemyDied += enemySpawner.IncrementKillStat;
        }
    }
}
