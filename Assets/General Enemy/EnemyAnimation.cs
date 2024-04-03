using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    Animator enemy;
    public bool walking;
    public float directionAngle;
    public VisualEffect muzzleFlash;

    void Start()
    {
        enemy = GetComponent<Animator>();
    }

    public void ShootAnim()
    {
        enemy.SetTrigger("Shoot");
        muzzleFlash.Reinit();
    }

    public void HurtAnim()
    {
        enemy.SetFloat("HurtRandom", Random.Range(0f, 1f));
        enemy.SetTrigger("Hurt");
    }

    void Update()
    {
        enemy.SetBool("Walking", walking);
        enemy.SetFloat("Angle", directionAngle);
    }
}
