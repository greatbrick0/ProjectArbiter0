using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBurning : MonoBehaviour
{

    DamageNumberManager hitNumberRef;

    EnemyHealth health;

    int burnTicks = 4;

    private void Awake()
    {
        hitNumberRef = DamageNumberManager.GetManager();
        health = transform.parent.GetComponent<EnemyHealth>();

    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireTick());
    }


    IEnumerator FireTick()
    {
        burnTicks--;
        health.TakeDamage(12, DamageDetails.DamageSource.Ability, DamageDetails.DamageSpot.Body, DamageDetails.DamageElement.Ice);
        hitNumberRef.CreateDamageNumber(12, transform.position, DamageDetails.DamageElement.Fire, DamageDetails.DamageSpot.Body);
        if (burnTicks == 0) Destroy(this.gameObject);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FireTick());
    }



}
