using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageDetails;
using Coherence.Toolkit;
using Coherence;
using UnityEngine.VFX;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField]
    private bool defaultBehaviourEnabled = false;
    private CoherenceSync sync;
    private PlayerMovement movementScript;
    private HUDGunAmmoScript hudGunRef;
    [HideInInspector]
    public Camera cam { private get; set; }
    private Ray ray;
    private RaycastHit hit;
    Vector3 straight;
    Vector3 originPos;

    [SerializeField]
    WeaponData weapon;

    [SerializeField]
    VisualEffect muzzleFlash;

    [Header("Stats")]
    [SerializeField]
    private string gunName = "basic";
    [SerializeField] 
    private bool automatic = false;
    private bool randomizePattern = false;
    [SerializeField] 
    private float reloadTime = 2.0f;
    [SerializeField]
    private int maxAmmo = 12;
    private Vector2 closeDamage;
    private Vector2 farDamage;
    private List<ShotShape> shotPatterns;
    [SerializeField]
    private float range = 10.0f;
    private float resetTime;

    [field: Header("Trackers")]
    [field: SerializeField]
    public bool reloading { get; private set; } = false;
    [field: SerializeField]
    bool cooling = false;
    [field: SerializeField]
    public int currentAmmo { get; private set; }
    public float reloadProgress { get; private set; } = 0.0f;
    float cooldownProgress = 0.0f;
    private bool inputtingFire = false;
    private float timeSinceLastShot = 0.0f;
    [SerializeField]
    private int patternIndex = 0;

    private void SetAllStats()
    {
        gunName = weapon.gunName;
        automatic = weapon.automatic;
        randomizePattern = weapon.randomizePattern;
        reloadTime = weapon.reloadTime;
        maxAmmo = weapon.maxAmmo;
        closeDamage = weapon.closeDamage;
        farDamage = weapon.farDamage;
        shotPatterns = weapon.shotPattern;
        range = weapon.range;
        resetTime = weapon.resetTime;
        movementScript.maxRecoilBounds = weapon.maxRecoilBounds;
    }

    public void StartInput()
    {
        if (!defaultBehaviourEnabled) return;
        inputtingFire = true;
    }

    public void EndInput()
    {
        if (!defaultBehaviourEnabled) return;
        if (reloading) inputtingFire = false;
        if (automatic) inputtingFire = false;
    }

    private void Awake()
    {
        sync = GetComponent<CoherenceSync>();
        movementScript = GetComponent<PlayerMovement>();
        
        SetAllStats();
        currentAmmo = maxAmmo;
        
#if (UNITY_EDITOR)
        if (closeDamage.x > farDamage.x) Debug.LogError(gunName + " close damage point cannot be farther than far damage point");
#endif
    }

    public void GetHUDReference() //I wanted this to be in awake(), but I need to have the HUD instantiated before it, so...
    {
        hudGunRef = GameObject.Find("GunHUD").GetComponent<HUDGunAmmoScript>();
        hudGunRef.SetCurrentAmmo(currentAmmo);
        hudGunRef.SetMaxAmmo(maxAmmo);
    }

    private void Update()
    {
        if (!defaultBehaviourEnabled) return;

        timeSinceLastShot += 1.0f * Time.deltaTime;
        if (timeSinceLastShot >= resetTime)
        {
            if(!randomizePattern) patternIndex = 0;
            movementScript.recoilActive = false;
        }

        if (cooling) CoolDown();
        else if (reloading) Reload();
        else if (inputtingFire)
        {
            if (currentAmmo == 0 && !reloading) StartReload();
            else
            {
                straight = cam.transform.forward;
                originPos = cam.transform.position;
                sync.SendCommand<WeaponHolder>(nameof(Shoot), MessageTarget.All, straight, originPos);
                hudGunRef.UseShot();
            }
        }
    }

    private void CoolDown()
    {
        cooldownProgress -= 1.0f * Time.deltaTime;
        if (cooldownProgress <= 0)
        {
            cooling = false;
        }
    }

    /// <summary>
    /// Called for one frame when a reload is attempted. Determines whether a reload is valid, then handles initial reload logic.
    /// </summary>
    /// <returns></returns>
    public bool StartReload()
    {
        if (currentAmmo == maxAmmo) return false;
        else if (!defaultBehaviourEnabled) return false;
        else if (!reloading)
        {
            reloading = true;
            FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.playerReloading, gameObject);
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Called on every frame that the weapon is reloading. Determines when to finish reloading. 
    /// </summary>
    private void Reload()
    {
        reloadProgress += 1.0f * Time.deltaTime;
        print(reloadProgress);
        if (reloadProgress >= reloadTime)
        {
            reloading = false;
            reloadProgress = 0;
            currentAmmo = maxAmmo;
            hudGunRef.SetCurrentAmmo(maxAmmo);
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
        bool didHit;

        ShootDecorations();

        currentAmmo -= 1;
        foreach (Vector2 ii in shotPatterns[patternIndex].points)
        {
            Vector3 angle = Quaternion.AngleAxis(ii.x, Vector3.up) * straight;
            angle = Quaternion.AngleAxis(-ii.y, Vector3.right) * angle;
            ray = new Ray(originPos, angle);

            didHit = Physics.Raycast(ray, out hit, range, (1 << 6) | (1 << 8));
            if (didHit) HitAffect(hit);
        }
        cooldownProgress = shotPatterns[0].cooldownTime;
        cooling = true;
        timeSinceLastShot = 0.0f;
        movementScript.NewRecoil(shotPatterns[patternIndex].recoilDirection, shotPatterns[patternIndex].recoilTime);

        if (!automatic) inputtingFire = false;
        if (randomizePattern)
        {
            patternIndex += 1;
            patternIndex %= shotPatterns.Count;
        }
        else
        {
            patternIndex = Mathf.FloorToInt(Random.Range(0, shotPatterns.Count));
        }
        
    }

    /// <summary>
    /// Called in Shoot. Used for all effects that do not affect any logic. Visual effects, sound effect, and more. 
    /// </summary>
    private void ShootDecorations()
    {
        muzzleFlash.Reinit();
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.pistolShoot, gameObject);
        
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
            FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.bulletHit, sphere);
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

    public void SetDefaultBehaviourEnabled(bool newValue)
    {
        defaultBehaviourEnabled = newValue;
    }
}
