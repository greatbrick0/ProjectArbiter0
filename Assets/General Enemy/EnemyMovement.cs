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

    [SerializeField]
    private EnemyAnimation anim;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
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
        anim.directionAngle = 0.0f;
    }

    public void LookAtPlayer(GameObject playerObj)
    {
        Vector2 playerHorizontalDir = Vec2FromXZ(playerObj.transform.position - transform.position).normalized;
        Vector2 myHorizontalDir = Vec2FromXZ(transform.forward).normalized;
        float difference = Vector2.Angle(myHorizontalDir, playerHorizontalDir);

        print(difference/360);
        anim.directionAngle = difference / 360;
    }

    public void StandStill()
    {
        agent.SetDestination(transform.position);
        anim.walking = false;
    }

    public void IdleBehaviour()
    {
        agent.SetDestination(spawnPos);
        anim.walking = !(agent.velocity.sqrMagnitude < 0.2f);
    }

    private Vector2 Vec2FromXZ(Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.z);
    }
}
