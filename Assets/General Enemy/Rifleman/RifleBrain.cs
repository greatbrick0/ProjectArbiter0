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

    private enum States
    {
        Idle = 0,
        Attack = 1,
        Shoot = 2,
        Flee = 3,
    }

    private void Start()
    {
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
            case States.Attack:
                moveScript.WalkTowardsPlayer(targetPlayer);
                break;
            case States.Shoot:
                moveScript.StandStill();
                if (gunScript.CheckLosToPlayer(targetPlayer, shootRange))
                    gunScript.Attack(targetPlayer.GetComponent<PlayerHealth>());
                break;
            case States.Flee:
                break;
        }
    }

    public override void Logic()
    {
        if (targetPlayer == null) state = States.Idle;
        else if (gunScript.CheckLosToPlayer(targetPlayer, shootRange * 0.9f)) state = States.Shoot;
        else state = States.Attack;
    }
}
