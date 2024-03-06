using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageDetails;

[RequireComponent(typeof(Collider))]
public class Hitbox : MonoBehaviour
{
    [SerializeField]
    private Damageable owner;

    [SerializeField]
    private DamageSpot spotType;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        gameObject.layer = 6;
    }

    public Damageable GetOwner()
    {
        return owner;
    }

    public DamageSpot GetSpotType()
    {
        return spotType;
    }
}
