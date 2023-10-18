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

    private void Start()
    {
        targetPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(targetPos);
    }
}
