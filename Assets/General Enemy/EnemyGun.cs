using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField]
    private Transform head;

    private Ray ray;
    private RaycastHit hit;

    [SerializeField]
    [Tooltip("The time between attacks, measured in seconds. ")]
    private float attackTime = 1.0f;
    private float attackCooldown = 0.0f;
    [SerializeField]
    [Tooltip("The point the attack projectile will be spawned from. ")]
    private Transform attackPoint;
    [SerializeField]
    private GameObject attackObj;
    [SerializeField]
    private int attackDamage = 35;

    public bool CheckLosToPlayer(GameObject player, float checkRange)
    {
        Vector3 targetPos = player.GetComponent<PlayerHealth>().head.position;
        if (Vector3.Distance(head.position, targetPos) > checkRange) return false;
        checkRange = Vector3.Distance(head.position, targetPos);

        bool didCollide;
        int layers = (1 << 6) | (1 << 8) ; //los will be blocked by other enemies and terrain

        ray = new Ray(head.position, head.position - targetPos);
        didCollide = Physics.Raycast(ray, out hit, checkRange, layers);

        return !didCollide;
    }

    public void Attack(PlayerHealth player)
    {
        attackCooldown += 1.0f * Time.deltaTime;
        if (attackCooldown >= attackTime)
        {
            attackCooldown = 0.0f;
            player.TakeDamage(attackDamage);
        }
    }
}
