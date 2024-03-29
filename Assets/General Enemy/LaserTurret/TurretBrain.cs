using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class TurretBrain : EnemyBrain
{
    private TurretLaserController gun;

    private GameObject targetPlayer;

    [SerializeField]
    private bool powered = true;

    protected override void Start()
    {
        base.Start();
        gun = GetComponentInChildren<TurretLaserController>();
    }

    protected override void Update()
    {
        base.Update();
        if (targetPlayer == null) Logic();
        
        if(powered)
        {
            gun.PointTowards(targetPlayer.transform.position);
        }
    }

    public override void Logic()
    {
        targetPlayer = ChooseTargetPlayer();
        if (targetPlayer == null) powered = false;
    }
}
