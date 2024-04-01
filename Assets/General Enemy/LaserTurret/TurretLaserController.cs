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

        if (timeCharging >= 5.0f) timeCharging = 0.0f;
        if (timeCharging >= 4.6f) chargeState = 3;
        else if (timeCharging >= 4.0f) chargeState = 2;
        else if (timeCharging >= 2.7f) chargeState = 1;
        else chargeState = 0;

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
                head.LookAt(pos);
                break;
            case 1:
                break;
            case 2:
            case 3:
                break;
        }
    }
}
