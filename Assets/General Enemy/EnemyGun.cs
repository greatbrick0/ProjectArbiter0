using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField]
    private Transform head;
    [SerializeField]
    private EnemyAnimation anim;

    private Ray ray;
    private RaycastHit hit;

    [SerializeField]
    [Tooltip("The time between attacks or bursts, measured in seconds. ")]
    private float attackTime = 1.0f;
    [SerializeField]
    [Tooltip("The amount of projectiles that will be fired in a single burst. "), Min(1)]
    private int attackBurstAmount = 1;
    private int burstTracker = 0;
    [SerializeField]
    [Tooltip("The time between attacks within a single burst, measured in seconds. ")]
    private float attackBurstTime = 0.1f;
    private float attackCooldown = 0.0f;
    [SerializeField]
    [Tooltip("The point the attack projectile will be spawned from, such as the barrel of a gun. ")]
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

        ray = new Ray(head.position, targetPos - head.position);
        didCollide = Physics.Raycast(ray, out hit, checkRange, layers);

        return !didCollide;
    }

    public void Attack(PlayerHealth player)
    {
        attackCooldown -= 1.0f * Time.deltaTime;
        if (attackCooldown <= 0.0f)
        {
            if(burstTracker >= attackBurstAmount - 1)
            {
                attackCooldown = attackTime;
                burstTracker = 0;
                FMODUnity.RuntimeManager.PlayOneShotAttached(FMODEvents.instance.enemyShoot, gameObject);
            }
            else
            {
                attackCooldown = attackBurstTime;
                burstTracker += 1;
            }
            

            if (attackObj == null) player.TakeDamage(attackDamage);
            else CreateBullet(player.transform.position - head.position);
            anim.ShootAnim();
        }
    }

    private void CreateBullet(Vector3 direction)
    {
        GameObject bulletRef = Instantiate(attackObj);
        bulletRef.transform.SetParent(transform.parent);
        bulletRef.transform.position = (attackPoint == null) ? transform.position : attackPoint.position;
        bulletRef.GetComponent<Projectile>().velocity = (direction + new Vector3(Random.value, Random.value, Random.value) - new Vector3(Random.value, Random.value, Random.value)).normalized * bulletRef.GetComponent<Projectile>().velocity.magnitude;
        bulletRef.GetComponent<EnemySlowBullet>().damage = attackDamage;
    }
}
