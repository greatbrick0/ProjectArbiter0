using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaserController : MonoBehaviour
{
    [SerializeField]
    private Transform head;

    private Ray ray;
    private RaycastHit hit;

    public bool charging = false;
    float timeCharging = 0.0f;
    int chargeState = 0;

    private void Update()
    {
        if (charging) timeCharging += 1.0f * Time.deltaTime;
        else timeCharging = 0.0f;

        if (timeCharging >= 5.0f && chargeState == 3) //end laser
        {
            timeCharging = 0.0f;
            chargeState = 0;
        }
        else if (timeCharging >= 4.6f && chargeState == 2) chargeState = 3; //start laser
        else if (timeCharging >= 4.0f && chargeState == 1) chargeState = 2; //stop moving
        else if (timeCharging >= 2.7f && chargeState == 0) chargeState = 1; //slow down
        else if (timeCharging >= 0.0f) chargeState = 0; //default 

        if(chargeState == 3) Laser();
    }

    private void Laser()
    {

    }

    public bool CheckLosToPlayer(GameObject player, float checkRange)
    {
        Vector3 targetPos = player.GetComponent<PlayerHealth>().head.position;
        if (Vector3.Distance(head.position, targetPos) > checkRange) return false;
        checkRange = Vector3.Distance(head.position, targetPos);

        bool didCollide;
        int layers = (1 << 8); //los will be blocked by terrain

        ray = new Ray(head.position, targetPos - head.position);
        didCollide = Physics.Raycast(ray, out hit, checkRange, layers);

        return !didCollide;
    }

    public void PointTowards(Vector3 pos, float deltaTime)
    {
        switch (chargeState)
        {
            case 0:
                //head.LookAt(pos);
                SlowLook(pos, 90.0f, deltaTime);
                break;
            case 1:
                SlowLook(pos, 30.0f, deltaTime);
                break;
            case 2:
            case 3:
                break;
        }
    }

    public void SlowLook(Vector3 pos, float speed, float deltaTime)
    {
        Vector3 intendedDir = (pos - head.position).normalized;
        Vector2 dist = Vector2.one * (speed * deltaTime);
        float hDiff = Vector2.Dot(Vec2FromXZ(intendedDir), Vec2FromXZ(head.parent.right));
        float vDiff = head.forward.y - intendedDir.y;
        dist.x = Mathf.Min(dist.x, Mathf.Abs(hDiff));
        dist.y = Mathf.Min(dist.y, Mathf.Abs(vDiff));

        head.parent.Rotate(Vector3.up, Mathf.Sign(hDiff) * dist.x);
        head.Rotate(Vector3.right, Mathf.Sign(vDiff) * dist.y, Space.Self);
    }

    private Vector2 Vec2FromXZ(Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.z);
    }

    private Vector2 Vec2FromYZ(Vector3 vec3)
    {
        return new Vector2(vec3.y, vec3.z);
    }
}
