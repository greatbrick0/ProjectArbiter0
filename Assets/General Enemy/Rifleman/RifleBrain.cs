using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement)), RequireComponent(typeof(EnemyGun)), RequireComponent(typeof(EnemyHealth))]
public class RifleBrain : EnemyBrain
{
    private EnemyMovement moveScript;
    private EnemyGun gunScript;
    private EnemyHealth healthScript;

    private GameObject targetPlayer;
    [SerializeField]
    [Tooltip("Determines the behaviour of this enemy. ")]
    private States state = States.Idle;

    [SerializeField]
    [Tooltip("The maximum distance the AI can shoot a player from. Will start to try to shoot the player within 90% of the distance. Measured in units. ")]
    private float shootRange = 10.0f;
    [SerializeField]
    private float closeRange = 3.0f;

    private enum States
    {
        Idle = 0,
        Find = 1,
        Shoot = 2,
        Flee = 3,
    }

    protected override void Start()
    {
        base.Start();
        moveScript = GetComponent<EnemyMovement>();
        gunScript = GetComponent<EnemyGun>();
        healthScript = GetComponent<EnemyHealth>();
    }

    protected override void Update()
    {
        base.Update();
        targetPlayer = ChooseTargetPlayer();
        if (state != States.Idle && targetPlayer == null) Logic();

        switch (state)
        {
            case States.Idle:
                moveScript.IdleBehaviour();
                break;
            case States.Find:
                if (rootedTimer <= 0.0f)
                {
                    moveScript.WalkTowardsPlayer(targetPlayer);
                }
                else
                {
                    rootedTimer -= Time.deltaTime;
                    moveScript.StandStill();
                }
                break;
            case States.Shoot:
                if(DistanceToPlayer() <= closeRange) moveScript.StandStill();
                else if (rootedTimer <= 0.0f)
                {
                    moveScript.WalkTowardsPlayer(targetPlayer);
                }
                else
                {
                    rootedTimer -= Time.deltaTime;
                    moveScript.StandStill();
                }
                if (gunScript.CheckLosToPlayer(targetPlayer, shootRange))
                {
                    if (stunnedTimer <= 0.0f)
                    {
                        gunScript.Attack(targetPlayer.GetComponent<PlayerHealth>());
                    }
                    else
                    {
                        stunnedTimer -= Time.deltaTime;
                    }

                }
                break;
            case States.Flee:
                break;
        }
    }

    public override void Logic()
    {
        if (targetPlayer == null) state = States.Idle;
        else if (gunScript.CheckLosToPlayer(targetPlayer, shootRange * 0.9f)) state = States.Shoot;
        else state = States.Find;
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, targetPlayer.transform.position);
    }
}
