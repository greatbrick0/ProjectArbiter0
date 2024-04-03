using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDelete : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 2.0f;
    private float age = 0.0f;

    void Update()
    {
        age += 1.0f * Time.deltaTime;
        if (age > lifeTime) Destroy(this.gameObject);
    }
}
