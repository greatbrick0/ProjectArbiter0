using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DamageDetails;

public class WeaponHolder : MonoBehaviour
{
    [HideInInspector]
    public Camera cam;
    private Ray ray;
    private RaycastHit hit;
    Vector3 straight;
    Vector3 originPos;

    [Header("Stats")]
    [SerializeField]
    string gunName = "basic";
    [SerializeField] [Tooltip("Whether the player is allowed to hold the button or needs to let go between shots.")]
    bool automatic = false;
    [SerializeField] [Tooltip("Reload time in seconds.")]
    float ReloadTime = 2.0f;
    [SerializeField]
    int maxAmmo = 12;
    [SerializeField] [Tooltip("X: The maximum distance the weapon will stay at this damage amount.\n" +
        "Y: The damage the weapon will produce below this distance.")]
    Vector2 closeDamage;
    [SerializeField] [Tooltip("X: The minimum distance the weapon will reach this damage amount.\n" +
        "Y: The damage the weapon will produce above this distance.")]
    Vector2 farDamage;
    [SerializeField]
    private ShotShape shotPattern;
    [SerializeField]
    private float range = 10.0f;

    [Header("Trackers")]
    [SerializeField]
    int currentAmmo;
    [SerializeField]
    bool reloading = false;
    [SerializeField]
    float reloadProgress = 0.0f;
    bool inputtingFire = false;

    [Serializable]
    public class ShotShape
    {
        [field: SerializeField]
        public List<Vector2> points { get; private set; }
    }

    public void StartInput()
    {
        inputtingFire = true;
    }

    public void EndInput()
    {
        if(automatic) inputtingFire = false;
    }

    private void Start()
    {
        currentAmmo = maxAmmo;
        if (closeDamage.x > farDamage.x) Debug.LogError(gunName + " close damage point cannot be farther than far damage point");
    }

    private void Update()
    {
        if (reloading)
        {
            reloadProgress += 1.0f * Time.deltaTime;
            if(reloadProgress >= ReloadTime)
            {
                reloading = false;
                reloadProgress = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Aim();
            Shoot(straight, originPos);
        }
    }

    private int DamageFromDistance(float distance)
    {
        float output;
        if (distance <= closeDamage.x) output = closeDamage.y;
        else if (distance >= farDamage.x) output = farDamage.y;
        else
        {
            output = Mathf.Lerp(closeDamage.y, farDamage.y, (distance - closeDamage.x) / (farDamage.x - closeDamage.x));
        }
        return Mathf.RoundToInt(output);
    }

    public void Shoot(Vector3 straight, Vector3 originPos)
    {
        bool didHit;
        foreach (Vector2 ii in shotPattern.points)
        {
            Vector3 angle = Quaternion.AngleAxis(ii.x, Vector3.up) * straight;
            angle = Quaternion.AngleAxis(-ii.y, Vector3.right) * angle;
            ray = new Ray(originPos, angle);

            didHit = Physics.Raycast(ray, out hit, range, (1 << 6) | (1 << 8));

            if (didHit)
            {
                if (hit.collider.gameObject.GetComponent<Hitbox>() != null)
                {
                    CalcDamage(hit.collider.gameObject.GetComponent<Hitbox>(), hit);
                }
                else
                {
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.position = hit.point;
                    sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
        }
    }

    private void Aim()
    {
        straight = cam.transform.forward;
        originPos = cam.transform.position;
    }

    private void CalcDamage(Hitbox hitbox, RaycastHit hitDetails)
    {
        if (hitbox.GetOwner().team != "Enemy") return;

        hitbox.GetOwner().TakeDamage(DamageFromDistance(hit.distance), DamageSource.Bullet, hitbox.GetSpotType());
    }
}
