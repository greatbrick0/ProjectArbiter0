using Coherence.Cloud;
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

    

    [SerializeField]
    [Tooltip("How much the turret can rotate in each axis, measured in degrees per second.")]
    private float turnSpeed = 90.0f;
    [SerializeField]
    [Tooltip("How much the turret can rotate each axis while charging the laser, measured in degrees per second.")]
    private float slowTurnSpeed = 30.0f;
    [Header("Beam")]
    [SerializeField]
    private float beamRadius = 0.5f;
    [SerializeField]
    private float beamLength = 300.0f;
    [SerializeField]
    private int beamDamage = 10;
    private List<PlayerHealth> hitPlayers = new List<PlayerHealth>();
    [SerializeField]
    [Tooltip("How frequently a laser can repeatedly hit a player, measured in seconds. \n" +
        "Lower numbers allow players to dodge out of some of the damage, but also make the beam feel more impactful.")]
    private float hitRefreshTime = 0.2f;
    private float timeSinceRefresh = 0.0f;


    private void Update()
    {
        if (charging) timeCharging += 1.0f * Time.deltaTime;
        else timeCharging = 0.0f;

        //activations of animations and particle effects can go here
        if (timeCharging >= 5.0f && chargeState == 3) //end laser
        {
            timeCharging = 0.0f;
            chargeState = 0;
            timeSinceRefresh = 0.0f;
            hitPlayers.Clear();
        }
        else if (timeCharging >= 4.6f && chargeState == 2) //start laser
        {
            chargeState = 3;
            print("fire");
        }
        else if (timeCharging >= 4.0f && chargeState == 1)  //stop rotation
        {
            chargeState = 2;
        }
        else if (timeCharging >= 2.7f && chargeState == 0)  //slow rotation
        {
            chargeState = 1;
        }
        else //default 
        {
            chargeState = 0;
        }

        if (chargeState == 3)
        {
            timeSinceRefresh += 1.0f * Time.deltaTime;
            if (timeSinceRefresh >= hitRefreshTime)
            {
                timeSinceRefresh = 0.0f;
                hitPlayers.Clear();
            }
            Laser();
        }
    }

    private void Laser()
    {
        bool didCollide;
        int layers = (1 << 0) | (1 << 6) | (1 << 8);

        ray = new Ray(head.position, head.forward);
        didCollide = Physics.SphereCast(ray, beamRadius, out hit, beamLength, layers);

        if (didCollide)
        {
            PlayerHealth p = hit.collider.gameObject.GetComponent<PlayerHealth>();
            if (p != null)
            {
                if(hitPlayers.Contains(p)) return;
                hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(beamDamage);
                hitPlayers.Add(p);
            } 
        }
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
