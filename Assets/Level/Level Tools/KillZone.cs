using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KillZone : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        PlayerHealth p = other.gameObject.GetComponent<PlayerHealth>();
        if (p != null)
        {
            p.TakeDamage(p.EffectiveHealth() + 1);
        }
    }
}
