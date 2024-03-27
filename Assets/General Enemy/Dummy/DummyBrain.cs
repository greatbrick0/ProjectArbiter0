using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class DummyBrain : EnemyBrain
{
    private EnemyMovement moveScript;

    protected override void Start()
    {
        moveScript = GetComponent<EnemyMovement>();
    }

    public override void Logic()
    {
        moveScript.StandStill();
    }
}
