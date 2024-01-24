using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coherence.Toolkit;
using Coherence;

public class PlayerHealth : MonoBehaviour
{
    private CoherenceSync sync;

    [SerializeField]
    public bool playerDead = false;
    [field: Header("Health")]
    [field: SerializeField]
    public int maxMainHealth { get; private set; } = 200;
    [SerializeField]
    public int mainHealth = 1;
    [SerializeField]
    private float regenDelay = 5.0f;
    [SerializeField]
    private float regenRate = 15.0f;
    private float regenBank = 0.0f;
    [Header("Shield")]
    [SerializeField]
    private int tempShield = 0;

    private float timeSinceDamaged = 0.0f;

    private void Start()
    {
        sync = GetComponent<CoherenceSync>();
        mainHealth = maxMainHealth;
    }

    private void Update()
    {
        timeSinceDamaged += 1.0f * Time.deltaTime;
        if (timeSinceDamaged >= regenDelay)
        {
            regenBank += regenRate * Time.deltaTime;
            while (regenBank >= 1) //probably not the best solution, ill fix it later
            { // i hate while loops for a system with no user input
                regenBank -= 1;
                mainHealth += 1;
            }
        }
    }

    public bool TakeDamage(int realAmount)
    {
        print(realAmount);
        timeSinceDamaged = 0.0f;

        tempShield -= realAmount;
        if (tempShield < 0)
        {
            mainHealth += tempShield;
            tempShield = 0;
        }

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
        GetComponent<PlayerMovement>().SetDefaultMovementEnabled(false);
        print("player died");
    }
}
