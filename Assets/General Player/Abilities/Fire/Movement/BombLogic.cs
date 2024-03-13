using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BombLogic : MonoBehaviour
{

    public PlayerSpellFireBomb bombAbilityRef;
    public int abilityDamage;
    DamageNumberManager hitNumberRef;



    [SerializeField]
    GameObject bombVisual;
    [SerializeField]
    GameObject explosionvisual;



    public List<Damageable> hitTargets;

    public float lifespan;

    int count;

    private void Start()
    {
        hitNumberRef = DamageNumberManager.GetManager();
        Invoke(nameof(Trigger), lifespan);
        GetComponent<Rigidbody>().AddForce(transform.forward*4 + transform.up*3, ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision collision)
    {
        var hitbox = collision.transform.GetComponent<Hitbox>();
        if (collision.transform.tag == "Player")
            Debug.Log("collision with player - firebomb");
        if (hitbox == null)
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        
    }
    private void OnTriggerEnter(Collider other)
    {

        var hitbox = other.GetComponent<Hitbox>();
        if (hitbox != null)
        {
            foreach (Damageable ii in hitTargets)
            {
                if (hitbox.GetOwner() == ii)
                {
                    return;
                }
            }
            if (hitbox.GetOwner().team == "Enemy")
            {
                Trigger();
                var spot = hitbox.GetSpotType();
                int hit = hitbox.GetOwner().TakeDamage(abilityDamage, DamageDetails.DamageSource.Ability, hitbox.GetSpotType(), DamageDetails.DamageElement.Ice);
                Vector3 location = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                hitNumberRef.CreateDamageNumber(hit, location, DamageDetails.DamageElement.Fire, spot);
                hitTargets.Add(hitbox.GetOwner());
            }
            
        }
        else
        {
            
        }
      
    }

    
    public void RequestDestroy() //Hello there!
    {
        Destroy(this.gameObject);
    }

    public void EndAttack()
    {
        Destroy(GetComponent<SphereCollider>());
    }

    public void Trigger()
    {
        bombVisual.SetActive(false);
        explosionvisual.SetActive(true);
        bombAbilityRef.bombRef = null;
        bombAbilityRef.bombActive = false;
        GetComponent<SphereCollider>().radius = 3;
        explosionvisual.SetActive(true);
        
        Invoke(nameof(RequestDestroy), 1.6f);
        Invoke(nameof(EndAttack), 0.6f);
        bombAbilityRef.ExplosionOccured();
        if ((bombAbilityRef.GetComponentInParent<CapsuleCollider>().transform.position - transform.position).magnitude <= 6)
        {
            Debug.Log("Player is in range");
            bombAbilityRef.ChangeControls();
            bombAbilityRef.GetComponentInParent<Rigidbody>().AddForce((bombAbilityRef.GetComponentInParent<CapsuleCollider>().transform.position - transform.position).normalized * 15, ForceMode.Impulse);
        }

    }

    

}

