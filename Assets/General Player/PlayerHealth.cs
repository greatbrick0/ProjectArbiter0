using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coherence.Toolkit;
using Coherence;
using FMOD.Studio;
using FMODUnity;

public class PlayerHealth : MonoBehaviour
{
    private CoherenceSync sync;
    private bool authority = false;
    private HUDSystem hudRef;
    [field:  SerializeField]
    public Transform head { get; private set; }

    [SerializeField]
    public bool playerDead = false;
    public delegate void PlayerDied();
    public event PlayerDied playerDied;
    [field: Header("Health")]
    [field: SerializeField]
    public int maxMainHealth { get; private set; } = 200;
    [SerializeField]
    public int mainHealth = 1;
    [SerializeField]
    private float regenDelay = 5.0f;
    [SerializeField]
    private float regenRate = 15.0f;
    [SerializeField]
    private int regenChunkSize = 3;
    private float regenBank = 0.0f;
    [Header("Shield")]
    [SerializeField]
    private int tempShield = 0;

    private float timeSinceDamaged = 0.0f;

    public EventInstance healingSoundInstance;
    public EventInstance heartBeatSoundInstance;
    bool isBreathing;

    private void Start()
    {
        sync = GetComponent<CoherenceSync>();
        authority = GetComponent<PlayerInput>().authority;
        hudRef = FindObjectOfType<HUDSystem>();
        mainHealth = maxMainHealth;
        UpdateHealthLabel();
    }

    private void Update()
    {
        if (playerDead) return;

        timeSinceDamaged += 1.0f * Time.deltaTime;
        if (timeSinceDamaged >= regenDelay && mainHealth < maxMainHealth)
        {
            regenBank += regenRate * Time.deltaTime;
            if (regenBank >= Mathf.Min(regenChunkSize, maxMainHealth - mainHealth)) 
            { 
                mainHealth += Mathf.FloorToInt(regenBank);
                regenBank -= Mathf.FloorToInt(regenBank);
                UpdateHealthLabel();
            }
        }

        if (mainHealth < maxMainHealth * 0.4f && isBreathing == false)
        {
            healingSoundInstance = RuntimeManager.CreateInstance(FMODEvents.instance.healing);
            RuntimeManager.AttachInstanceToGameObject(healingSoundInstance, transform);
            healingSoundInstance.start();
            healingSoundInstance.release();

            heartBeatSoundInstance = RuntimeManager.CreateInstance(FMODEvents.instance.heartBeat);
            RuntimeManager.AttachInstanceToGameObject(heartBeatSoundInstance, transform);
            heartBeatSoundInstance.start();

            isBreathing = true;
        }

        if (mainHealth > maxMainHealth * 0.5f && isBreathing == false)
        {
            heartBeatSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        if (mainHealth == maxMainHealth && isBreathing == true)
        {
            healingSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.healed, gameObject);

            isBreathing = false;
        }
    }

    public bool TakeDamage(int realAmount)
    {
        timeSinceDamaged = 0.0f;

        tempShield -= realAmount;
        if (tempShield < 0)
        {
            mainHealth += tempShield;
            tempShield = 0;
        }

        UpdateHealthLabel(true);

        if (mainHealth <= 0)
        {
            sync.SendCommand<PlayerHealth>(nameof(PlayerDown), MessageTarget.All);
            return true;
        }
        else return false;
    }

    public int EffectiveHealth()
    {
        return mainHealth + tempShield;
    }

    public void PlayerDown()
    {
        playerDead = true;
        if(playerDied != null) playerDied();
        GetComponent<PlayerInput>().selfBodyModel.transform.localScale = Vector3.zero;
        GetComponent<PlayerInput>().selfGunModel.SetActive(false);
        GetComponent<PlayerInput>().cameraRef.GetComponent<MainCameraScript>().spectateTarget = head;
        GetComponent<PlayerInput>().cameraRef.GetComponent<MainCameraScript>().mode = "spectate";
        GetComponent<PlayerMovement>().partialControlValue = 0.0f;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<WeaponHolder>().EmptyOutAmmo();
        GetComponent<WeaponHolder>().enabled = false;
        GetComponent<AbilityInputSystem>().SetAbilityLocks(false);
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        FindObjectOfType<PlayerTracker>().PlayerDied();
    }

    public void AttemptRespawn(Vector3 spawnPoint)
    {
        if (!playerDead) return;

        playerDead = false;
        mainHealth = maxMainHealth / 2;
        UpdateHealthLabel();
        GetComponent<PlayerInput>().selfBodyModel.transform.localScale = Vector3.one;
        GetComponent<PlayerInput>().selfGunModel.SetActive(true);
        GetComponent<PlayerInput>().cameraRef.GetComponent<MainCameraScript>().mode = "firstperson";
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<WeaponHolder>().enabled = true;
        GetComponent<AbilityInputSystem>().SetAbilityLocks(true);
        GetComponent<Collider>().enabled = true;
        transform.position = spawnPoint + Vector3.up + RandomPointInCircle(0.3f);
    }

    private void UpdateHealthLabel(bool damaged = false)
    {
        if(!authority) return;

        hudRef.SetHealthLabel(mainHealth.ToString());
        hudRef.SetHealthBarFill(mainHealth / (float)maxMainHealth);
        if (damaged) hudRef.EnableDamageGradient();
    }

    private Vector3 RandomPointInCircle(float circleRadius)
    {
        float randomAngle = Random.Range(0, Mathf.PI * 2);
        return new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle)) * Random.Range(0.0f, circleRadius);
    }

    public void ObtainShield(int shieldValue)
    {
        tempShield += shieldValue;
    }

    public int GetShield()
    {
        return tempShield;
    }
}
