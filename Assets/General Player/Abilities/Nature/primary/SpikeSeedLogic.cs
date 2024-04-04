using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSeedLogic : MonoBehaviour
{

    [SerializeField]
    private GameObject spikeVines;

    private GameObject spikeRef;

    public PlayerSpellNatureSpikes playerSpikeRef;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward*6 + transform.up*3, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hitbox = collision.transform.GetComponent<Hitbox>();
        if (collision.transform.tag == "Player")
        {
            Debug.Log("collision with player - firebomb");
            return;
        }
        if (collision.gameObject.layer == 8)
        {

            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            spikeRef = Instantiate(spikeVines,transform.position,Quaternion.identity);
            spikeRef.transform.rotation = Quaternion.Euler(collision.contacts[0].normal);
            spikeRef.transform.LookAt(spikeRef.transform.position + collision.contacts[0].normal);
            spikeRef.transform.eulerAngles = spikeRef.transform.eulerAngles + Vector3.right * 90.0f;
            Destroy(this.gameObject,0.1f);
        }
           
        
    }
}
