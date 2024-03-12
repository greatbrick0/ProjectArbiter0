using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClassChanger : MonoBehaviour
{
    [SerializeField]
    GameObject[] Class;

   

    [SerializeField]
    HUDSystem HUDRef;

    GameObject initRef;

    GameObject currentPlayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            currentPlayer = collision.gameObject;
            if (collision.transform.GetComponentInChildren<Ability>() != null)
            {
                GameObject originRef = collision.transform.GetComponentInChildren<Ability>().transform.parent.gameObject;
                GameObject.Destroy(originRef.GetComponentInChildren<Ability>().gameObject);
                initRef = Instantiate(Class[0], originRef.transform);
                Invoke("Connect", 0.1f);

                GameObject.Destroy(HUDRef.GetComponentInChildren<HudConnectScript>().gameObject);
                initRef = Instantiate(Class[1], HUDRef.transform);
                initRef.GetComponent<HudConnectScript>().ConnectToHUDSystem();
                
            }
            



        }
    }

    public void Connect()
    {
        currentPlayer.transform.GetComponent<AbilityInputSystem>().RegisterAbilities();
        currentPlayer.transform.GetComponent<WeaponHolder>().GetHUDReference();
        HUDRef.gunHUDRef.SetCurrentAmmo(currentPlayer.transform.GetComponent<WeaponHolder>().currentAmmo);
        currentPlayer.transform.GetComponent<SanitySystem>().GetHUDReference();
    }
}
