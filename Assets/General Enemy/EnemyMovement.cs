using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private float age = 0.0f;
    private Vector3 spawnPos;

    private NavMeshAgent agent;
    private Rigidbody rb;

    private EnemyAnimation anim;

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
        agent.SetDestination(transform.position);
    }

    private void Update()
    {
        age += 1.0f * Time.deltaTime;
    }

    public void WalkTowardsPlayer(GameObject playerObj)
    {
        agent.SetDestination(playerObj.transform.position);
        anim.walking = true;
    }

    public void StandStill()
    {
        agent.SetDestination(transform.position);
        anim.walking = false;
    }

    public void IdleBehaviour()
    {
        agent.SetDestination(spawnPos);
        anim.walking = (agent.velocity == Vector3.zero);
    }
}
