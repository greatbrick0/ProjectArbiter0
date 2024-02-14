using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField]
    private Transform head;

    private Ray ray;
    private RaycastHit hit;

    public bool CheckLosToPlayer(GameObject player, float checkRange)
    {
        bool didCollide;
        int layers = (1 << 6) | (1 << 8); //los will be blocked by other enemies and terrain
        Vector3 targetPos = player.GetComponent<PlayerHealth>().head.position;

        checkRange = Mathf.Min(checkRange, Vector3.Distance(head.position, targetPos));
        ray = new Ray(head.position, head.position - targetPos);
        didCollide = Physics.Raycast(ray, out hit, checkRange, layers);

        return !didCollide;
    }
}
