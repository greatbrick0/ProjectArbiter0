using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    Animator enemy;
    public bool walking;
    public float directionAngle;

    void Start()
    {
        enemy = GetComponent<Animator>();
    }

    public void ShootAnim()
    {
        enemy.SetTrigger("Shoot");
    }

    public void HurtAnim()
    {
        enemy.SetTrigger("Hurt");
    }

    void Update()
    {
        enemy.SetBool("Walking", walking);
        enemy.SetFloat("Angle", directionAngle);
    }
}
