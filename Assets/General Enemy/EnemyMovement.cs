using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private float age = 0.0f;
    [SerializeField]
    public Vector3 targetPos;
    private Vector3 spawnPos;

    private NavMeshAgent agent;
    private Rigidbody rb;

    private List<GameObject> sensedPlayers = new List<GameObject>();
    private GameObject targetPlayer;

    private EnemyAnimation anim;

    private float attackCooldown = 0.0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<EnemyAnimation>();
        spawnPos = transform.position;
    }

    private void Start()
    {
        targetPos = transform.position;
        age += transform.position.z;
    }

    void Update()
    {
        age += 1.0f * Time.deltaTime;

        CleanDeadPlayers();
        if(sensedPlayers.Count > 0)
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

    public void SensePlayer(GameObject newPlayer)
    {
        sensedPlayers.Add(newPlayer);
    }

    private void CleanDeadPlayers()
    {
        for (int ii = sensedPlayers.Count - 1; ii >= 0; ii--)
        {
            if (sensedPlayers[ii] == null) sensedPlayers.RemoveAt(ii);
            else if (sensedPlayers[ii].GetComponent<PlayerHealth>().playerDead) sensedPlayers.RemoveAt(ii);
        }
    }

    /// <summary>
    /// Chooses a player from the list sensedPlayers to become the target of the pathfinding.
    /// </summary>
    /// <returns>Returns the player that has the best score. Score is determined between a mix of max health and distance.</returns>
    GameObject ChooseTargetPlayer()
    {
        GameObject outputPlayer = sensedPlayers[0];
        float bestScore = 0;
        float dist;
        float score;

        foreach(GameObject ii in sensedPlayers)
        {
            dist = (transform.position - ii.transform.position).magnitude;
            score = Mathf.Pow(ii.GetComponent<PlayerHealth>().maxMainHealth, 2) / dist; // score = maxHealth^2 / distance
            if (score >= bestScore)
            {
                outputPlayer = ii;
                bestScore = score;
            }
        }

        return outputPlayer;
    }
}
