using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySense : MonoBehaviour
{
    [SerializeField]
    private EnemyMovement brain;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerHealth>() != null) brain.SensePlayer(other.gameObject);
    }
}
