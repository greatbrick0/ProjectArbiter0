using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageDetails;
using Coherence.Toolkit;
using Coherence;
using UnityEngine.VFX;
using FMODUnity;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField]
    private bool defaultBehaviourEnabled = false;
    private CoherenceSync sync;
    private PlayerMovement movementScript;
    private DamageNumberManager damageNumberScript;
    private HUDGunAmmoScript hudGunRef;
    
    public Camera cam;
    private Ray ray;
    private RaycastHit hit;
    public Vector3 straight { get; private set; }
    Vector3 up;
    public Vector3 originPos { get; private set; }

    [SerializeField]
    WeaponData weapon;


    public Animator animRef;

    [SerializeField]
    VisualEffect muzzleFlash;
    [SerializeField]
    GameObject bulletHole;

    [Header("Stats")]
    [SerializeField]
    private string gunName = "basic";
    [SerializeField]
    private bool automatic = false;
    private bool randomizePattern = false;
    [SerializeField]
    private float reloadTime = 2.0f;
    private int maxAmmo = 12;
    private Vector2 closeDamage;
    private Vector2 farDamage;
    private List<ShotShape> shotPatterns;
    private DamageElement bulletElement;
    [SerializeField]
    private float range = 10.0f;
    private float resetTime;
    private EventReference shootSound;
    private EventReference reloadSound;
    private EventReference stopShootSound;

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
    private bool shootingThisFrame = false;
    private bool shotLastFrame = false;

    private bool defaultShootingEnabled = true;

    private bool reloadPermitted = true;


    public delegate void HitEvent(RaycastHit h);
    public event HitEvent shotEvent;


    private void SetAllStats()
    {
        Debug.Log("TestSetAll");

        gunName = weapon.gunName;
        automatic = weapon.automatic;
        randomizePattern = weapon.randomizePattern;
        reloadTime = weapon.reloadTime;
        maxAmmo = weapon.maxAmmo;
        closeDamage = weapon.closeDamage;
        farDamage = weapon.farDamage;
        shotPatterns = weapon.shotPattern;
        bulletElement = weapon.bulletElement;
        range = weapon.range;
        resetTime = weapon.resetTime;
        movementScript.maxRecoilBounds = weapon.maxRecoilBounds;
        shootSound = weapon.shootSound;
        reloadSound = weapon.reloadSound;
        if (!weapon.stopShootSound.IsNull) stopShootSound = weapon.stopShootSound;
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

    private void Start()
    {
        damageNumberScript = DamageNumberManager.GetManager(); //GetManager() must be called after Awake()
    }

    /* I wanted this to be in awake(), but I need to have the HUD instantiated before it, so...                         */
    /*  i saw on a forum somewhere: Awake() is for init within a script, Start() is for connecting to other objects.
     *  i think Start() would fit well for this purpose.     -S                                                         */
    public void GetHUDReference()
    {
        hudGunRef = GameObject.Find("PlayerHUD").GetComponent<HUDSystem>().gunHUDRef;
        if (GetComponent<PlayerInput>().authority)
        {
        hudGunRef.SetCurrentAmmo(currentAmmo);
        hudGunRef.SetMaxAmmo(maxAmmo);
        }
    }

    private void Update()
    {
        if (!defaultBehaviourEnabled) return;

        shootingThisFrame = false;
        timeSinceLastShot += 1.0f * Time.deltaTime;
        if (timeSinceLastShot >= resetTime)
        {
            if(!randomizePattern) patternIndex = 0;
            movementScript.recoilActive = false;
        }

        if (cooling)
        {
            CoolDown();
            shootingThisFrame = true;
        }
        else if (reloading) Reload();
        else if (inputtingFire)
        {
            if (currentAmmo == 0 && !reloading) StartReload();
            else if (defaultShootingEnabled)
            {
                straight = cam.transform.forward;
                up = cam.transform.up;
                originPos = cam.transform.position;
                sync.SendCommand<WeaponHolder>(nameof(Shoot), MessageTarget.All, straight, up, originPos);
                hudGunRef.UseShot();
                shootingThisFrame = true;
            }
        }

        if (!shootingThisFrame && shotLastFrame)
        {
            if (!stopShootSound.IsNull) FMODUnity.RuntimeManager.PlayOneShotAttached(stopShootSound, gameObject);
        }
        shotLastFrame = shootingThisFrame;
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
        if (!reloadPermitted) return false;
        if (currentAmmo == maxAmmo) return false;
        else if (!defaultBehaviourEnabled) return false;
        else if (!reloading)
        {
            reloading = true;
            animRef.SetTrigger("Reload");
            Debug.Log("Adding OtherStateLock");
            GetComponent<AbilityInputSystem>().OtherActionStateLock = true;
            FMODUnity.RuntimeManager.PlayOneShotAttached(reloadSound, gameObject);
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
        if (reloadProgress >= reloadTime)
        {
            reloading = false;
            Debug.Log("Removing OtherStateLock");
            GetComponent<AbilityInputSystem>().OtherActionStateLock = false;
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
    /// <param name="up">A vector perpendicular to the direction the bullets will fire. 
    /// Used for offsetting the bullets from the center of the shot.</param>
    /// <param name="originPos">The position the bllets will be created.</param>
    public void Shoot(Vector3 straight, Vector3 up, Vector3 originPos)
    {
        bool didHit;
        Vector3 right = Vector3.Cross(up, straight);

        ShootDecorations();

        currentAmmo -= 1;
        foreach (Vector2 ii in shotPatterns[patternIndex].points)
        {
            Vector3 angle = Quaternion.AngleAxis(ii.x, up) * straight;
            angle = Quaternion.AngleAxis(-ii.y, right) * angle;
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
        animRef.SetTrigger("Shoot");
        FMODUnity.RuntimeManager.PlayOneShotAttached(shootSound, gameObject);
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
            if (shotEvent != null ) shotEvent(hit);
        }
        else
        {
            GameObject sphere = Instantiate(bulletHole);
            sphere.transform.position = hit.point;
            sphere.transform.LookAt(sphere.transform.position + hit.normal);
            sphere.transform.position += hit.normal * 0.01f;
            //sphere.transform.rotation = Quaternion.Euler(hit.normal);
            FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.groundHit, sphere);
        }
    }

    /// <summary>
    /// Applies damage to a Damageable.
    /// </summary>
    /// <param name="hitbox">The hitbox of the Damageable that was hit.</param>
    /// <param name="hitDetails">The RaycastHit of the bullet that is applying the damage.</param>
    private void ApplyDamage(Hitbox hitbox, RaycastHit hitDetails)
    {
        if (hitbox.GetOwner().team == "Button") 
        {
            Debug.Log("Button ApplyDamage");
            hitbox.GetOwner().GetComponent<GunButton>().recentShotPlayer = this.gameObject;
            hitbox.GetOwner().GetComponent<GunButton>().Press();
        }
        if (hitbox.GetOwner().team != "Enemy") return;
        if(hitbox.GetOwner().GetComponent<EnemyHealth>() != null)
        {
            if (hitbox.GetOwner().GetComponent<EnemyHealth>().health <= 0) return;
        }

        int damageAmount = hitbox.GetOwner().TakeDamage(DamageFromDistance(hitDetails.distance), DamageSource.Bullet, hitbox.GetSpotType(), bulletElement);

        damageNumberScript.CreateDamageNumber(damageAmount, hitDetails.point, bulletElement, hitbox.GetSpotType());
    }

    public void SetDefaultBehaviourEnabled(bool newValue)
    {
        defaultBehaviourEnabled = newValue;
    }

    public void SetDefaultBehaviourEnabled(bool newValue, bool firePermitted = false, bool permitReload = false)
    {
        defaultBehaviourEnabled = newValue;
        defaultShootingEnabled = firePermitted;
        reloadPermitted = permitReload;
        Debug.Log("Default gun behavior modified: new reload permit is "+reloadPermitted);
    }

    public WeaponData GetWeaponData()
    {
        if (weapon != null)
            return weapon;
        else
            return null;
        //This is for you, Spencer. :)
    }
    public void SetWeaponData(WeaponData newWeapon)
    {
        if (newWeapon != null)
         Debug.Log("Set Weapon");
        else
         Debug.Log("newWeapon not passed");
        weapon = newWeapon;

        SetAllStats();
    }

    public void MaxOutAmmo()
    {
        currentAmmo = maxAmmo;
        hudGunRef.SetCurrentAmmo(currentAmmo);
    }

    public void EmptyOutAmmo()
    {
        currentAmmo = 0;
        hudGunRef.SetCurrentAmmo(currentAmmo);
    }

    // For use by class changer
    public void SetMuzzleFlash(VisualEffect muzzleFlash) { this.muzzleFlash = muzzleFlash; }
}