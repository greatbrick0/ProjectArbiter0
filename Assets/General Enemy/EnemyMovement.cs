using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public PlayerTracker playerTracker;
    private float age = 0.0f;
    [SerializeField]
    public Vector3 targetPos;
    private Vector3 spawnPos;

    private NavMeshAgent agent;
    private Rigidbody rb;
    private GameObject targetPlayer;

    private EnemyAnimation anim;

    private float attackCooldown = 0.0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<EnemyAnimation>();
    }

    private void Start()
    {
        spawnPos = transform.position;
        agent.enabled = true;
        targetPos = transform.position;
    }

    void Update()
    {
        age += 1.0f * Time.deltaTime;

        if(playerTracker.playerCount > 0)
        {
            targetPlayer = ChooseTargetPlayer();
            targetPos = targetPlayer.transform.position;
            agent.SetDestination(targetPos);
            anim.walking = true;
            if (Vector3.Distance(transform.position, targetPos) < 3) Attack(targetPlayer.GetComponent<PlayerHealth>());
        }
        else
        {
            //targetPos = spawnPos + (Vector3.right * Mathf.Round(Mathf.Sin(age / 4)) * 4);
            agent.SetDestination(targetPos);
        }
    }

    private void Attack(PlayerHealth player)
    {
        attackCooldown += 1.0f * Time.deltaTime;
        if (attackCooldown >= 1.0f)
        {
            player.TakeDamage(35);
            attackCooldown = 0;
        }
    }

    /// <summary>
    /// Chooses a player from the list sensedPlayers to become the target of the pathfinding.
    /// </summary>
    /// <returns>Returns the player that has the best score. Score is determined between a mix of max health and distance.</returns>
    GameObject ChooseTargetPlayer()
    {
        GameObject outputPlayer = playerTracker.GetPlayerObject(0);
        float bestScore = 0;
        float dist;
        float score;

        for(int ii = 0; ii < playerTracker.playerCount; ii++)
        {
            if (playerTracker.GetPlayerObject(ii).GetComponent<PlayerHealth>().playerDead) continue;

            dist = (transform.position - playerTracker.GetPlayerObject(ii).transform.position).magnitude;
            score = Mathf.Pow(playerTracker.GetPlayerObject(ii).GetComponent<PlayerHealth>().maxMainHealth, 2) / dist; // score = maxHealth^2 / distance
            if (score >= bestScore)
            {
                outputPlayer = playerTracker.GetPlayerObject(0);
                bestScore = score;
            }
        }

        return outputPlayer;
    }
}
