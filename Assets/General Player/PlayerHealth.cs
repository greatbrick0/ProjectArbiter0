using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coherence.Toolkit;
using Coherence;

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
        GetComponent<PlayerMovement>().partialControlValue = 0.0f;
        GetComponent<PlayerMovement>().enabled = false;
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
}
