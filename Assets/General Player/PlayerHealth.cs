using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coherence.Toolkit;
using Coherence;

public class PlayerHealth : MonoBehaviour
{
    private CoherenceSync sync;
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
        GetComponent<PlayerMovement>().SetDefaultMovementEnabled(false);
        GetComponent<PlayerMovement>().partialControlValue = 0.0f;
        FindObjectOfType<PlayerTracker>().spectatorCount += 1;
        print("player died");
    }

    private void UpdateHealthLabel(bool damaged = false)
    {
        hudRef.SetHealthLabel(mainHealth.ToString());
        if (damaged) hudRef.EnableDamageGradient();
    }
}
