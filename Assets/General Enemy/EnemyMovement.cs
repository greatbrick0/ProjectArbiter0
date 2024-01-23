using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    public Vector3 targetPos;

    private NavMeshAgent agent;
    private Rigidbody rb;

    private List<GameObject> sensedPlayers = new List<GameObject>();
    private GameObject targetPlayer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        CleanDeadPlayers();
        if(sensedPlayers.Count > 0)
        {
            targetPos = ChooseTargetPlayer().transform.position;
            agent.SetDestination(targetPos);
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
            if (sensedPlayers[ii].GetComponent<PlayerHealth>().playerDead) sensedPlayers.RemoveAt(ii);
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
