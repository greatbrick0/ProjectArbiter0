using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class DummyBrain : EnemyBrain
{
    private EnemyMovement moveScript;

    [SerializeField]
    private bool returnToSpot = false;

    private void Start()
    {
        moveScript = GetComponent<EnemyMovement>();
    }

    public override void Logic()
    {
        //if (returnToSpot) moveScript.IdleBehaviour();
        moveScript.StandStill();
    }
}
