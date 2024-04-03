using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class DeathReplace : MonoBehaviour
{
    [SerializeField] 
    private GameObject replacement;
    private GameObject instanceRef;

    void Start()
    {
        GetComponent<EnemyHealth>().enemyDied += Replace;
    }

    void Replace()
    {
        instanceRef = Instantiate(replacement, transform.parent);
        instanceRef.transform.position = transform.position;
        instanceRef.transform.rotation = transform.rotation;
    }
}
