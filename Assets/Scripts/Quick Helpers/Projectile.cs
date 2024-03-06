using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 acceleration;
    public float gravity = 0.0f;

    void Update()
    {
        velocity += acceleration * Time.deltaTime;
        velocity += Vector3.down * gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }
}
