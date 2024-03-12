using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClassChanger : MonoBehaviour
{
    [SerializeField]
    GameObject[] Class;

    [SerializeField]
    WeaponData weaponData;
    [SerializeField]
    HUDSystem HUDRef;

    GameObject initRef;

    GameObject currentPlayer;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Detected player");
            currentPlayer = collision.gameObject;
            if (collision.transform.GetComponentInChildren<Ability>() != null)
            {
                GameObject originRef = collision.transform.GetComponentInChildren<Ability>().transform.parent.gameObject;
                GameObject.Destroy(originRef.GetComponentInChildren<Ability>().gameObject);
                initRef = Instantiate(Class[0], originRef.transform);
                Invoke("Connect", 0.1f);
                currentPlayer.transform.GetComponent<WeaponHolder>().SetWeaponData(weaponData);
                currentPlayer.transform.GetComponent<WeaponHolder>().MaxOutAmmo();
                GameObject.Destroy(HUDRef.GetComponentInChildren<HudConnectScript>().gameObject);

                if (currentPlayer.GetComponent<PlayerInput>().authority)
                {
                    initRef = Instantiate(Class[1], HUDRef.transform);
                    initRef.GetComponent<HudConnectScript>().ConnectToHUDSystem();
                }
                
            }
            



        }
    }

    public void Connect()
    {
        if (currentPlayer.GetComponent<PlayerInput>().authority)
        {
            currentPlayer.transform.GetComponent<AbilityInputSystem>().RegisterAbilities();
            currentPlayer.transform.GetComponent<WeaponHolder>().GetHUDReference();
            HUDRef.gunHUDRef.SetCurrentAmmo(currentPlayer.transform.GetComponent<WeaponHolder>().currentAmmo);
            currentPlayer.transform.GetComponent<SanitySystem>().GetHUDReference();
        }
    }
}
