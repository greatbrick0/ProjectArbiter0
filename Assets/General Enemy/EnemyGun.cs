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
    private float attackTime = 1.0f;
    private float attackCooldown = 0.0f;
    [SerializeField]
    private int attackDamage = 35;

    public bool CheckLosToPlayer(GameObject player, float checkRange)
    {
        Vector3 targetPos = player.GetComponent<PlayerHealth>().head.position;
        if (Vector3.Distance(head.position, targetPos) > checkRange) return false;
        checkRange = Vector3.Distance(head.position, targetPos);

        bool didCollide;
        int layers = (1 << 6) | (1 << 8); //los will be blocked by other enemies and terrain

        ray = new Ray(head.position, head.position - targetPos);
        didCollide = Physics.Raycast(ray, out hit, checkRange, layers);

        return !didCollide;
    }

    public void Attack(PlayerHealth player)
    {
        attackCooldown += 1.0f * Time.deltaTime;
        if (attackCooldown >= 1.0f)
        {
            attackCooldown = 0.0f;
            player.TakeDamage(attackDamage);
        }
    }
}
