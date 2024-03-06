using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 velocity = Vector3.forward;
    public Vector3 acceleration;
    public float gravity = 0.0f;
    public float lifeTime = 2.0f;
    private float age = 0.0f;

    void Update()
    {
        age += 1.0f * Time.deltaTime;
        if (age > lifeTime) Destroy(this.gameObject);

        velocity += acceleration * Time.deltaTime;
        velocity += Vector3.down * gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        transform.LookAt(transform.position + velocity);
    }
}
