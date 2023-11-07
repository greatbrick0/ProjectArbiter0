using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DamageDetails;
using Coherence.Toolkit;
using Coherence;

public class WeaponHolder : MonoBehaviour
{
    CoherenceSync sync;
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
    bool reloading = false;
    [field: SerializeField]
    public int currentAmmo { get; private set; }
    [SerializeField]
    float reloadProgress = 0.0f;
    private bool inputtingFire = false;

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

    private void Awake()
    {
        sync = GetComponent<CoherenceSync>();
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

        if (inputtingFire)
        {
            straight = cam.transform.forward;
            originPos = cam.transform.position;
            sync.SendCommand<WeaponHolder>(nameof(Shoot), MessageTarget.All, straight, originPos);
        }
    }

    /// <summary>
    /// Calculates damage the damage falloff of a bullet from a given distance.
    /// </summary>
    /// <param name="distance">The distance that the bullet travelled.</param>
    /// <returns>The amount of damage that was calculated.</returns>
    private int DamageFromDistance(float distance)
    {
        float output;

        if (distance <= closeDamage.x) output = closeDamage.y;
        else if (distance >= farDamage.x) output = farDamage.y;
        else output = Mathf.Lerp(closeDamage.y, farDamage.y, (distance - closeDamage.x) / (farDamage.x - closeDamage.x));

        return Mathf.RoundToInt(output);
    }

    /// <summary>
    /// Creates bullets in the pattern of shotPattern.
    /// </summary>
    /// <param name="straight">The direction the bullets will fire.</param>
    /// <param name="originPos">The position the bllets will be created.</param>
    public void Shoot(Vector3 straight, Vector3 originPos)
    {
        print(gameObject.name + " shoot");
        bool didHit;

        foreach (Vector2 ii in shotPattern.points)
        {
            Vector3 angle = Quaternion.AngleAxis(ii.x, Vector3.up) * straight;
            angle = Quaternion.AngleAxis(-ii.y, Vector3.right) * angle;
            ray = new Ray(originPos, angle);

            didHit = Physics.Raycast(ray, out hit, range, (1 << 6) | (1 << 8));
            if (didHit) HitAffect(hit);
        }

        if (!automatic) inputtingFire = false;
    }

    /// <summary>
    /// Determines what to do when a bullet is created. 
    /// </summary>
    /// <param name="hit">The RaycastHit of the bullet.</param>
    private void HitAffect(RaycastHit hit)
    {
        if (hit.collider.gameObject.GetComponent<Hitbox>() != null)
        {
            ApplyDamage(hit.collider.gameObject.GetComponent<Hitbox>(), hit);
        }
        else
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = hit.point;
            sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }

    /// <summary>
    /// Applies damage to a Damageable.
    /// </summary>
    /// <param name="hitbox">The hitbox of the Damageable that was hit.</param>
    /// <param name="hitDetails">The RaycastHit of the bullet that is applying the damage.</param>
    private void ApplyDamage(Hitbox hitbox, RaycastHit hitDetails)
    {
        if (hitbox.GetOwner().team != "Enemy") return;

        hitbox.GetOwner().TakeDamage(DamageFromDistance(hitDetails.distance), DamageSource.Bullet, hitbox.GetSpotType());
    }
}
