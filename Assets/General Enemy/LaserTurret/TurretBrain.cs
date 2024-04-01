using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class TurretBrain : EnemyBrain
{
    private TurretLaserController gun;

    private GameObject targetPlayer;
    private Vector3 targetPlayerPos;

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
            if(gun.CheckLosToPlayer(targetPlayer, 200.0f))
            {
                targetPlayerPos = targetPlayer.transform.position;
                if (HDist(targetPlayerPos) - VDist(targetPlayerPos) > 1 && HDist(targetPlayerPos) + VDist(targetPlayerPos) > 1)
                    gun.PointTowards(targetPlayer.transform.position, Time.deltaTime);
                gun.charging = true;
            }
            else gun.charging = false;
        }
        else gun.charging = false;
    }

    public override void Logic()
    {
        targetPlayer = ChooseTargetPlayer();
        if (targetPlayer == null) powered = false;
    }

    private float HDist(Vector3 point)
    {
        return Vector2.Distance(VecFromXZ(transform.position), VecFromXZ(point));
    }

    private float VDist(Vector3 point)
    {
        return Mathf.Abs(point.y - transform.position.y);
    }

    private Vector2 VecFromXZ(Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.z);
    }
}
