using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerClassChanger : MonoBehaviour
{
    [SerializeField]
    GameObject[] Class;

    [SerializeField]
    WeaponData weaponData;
    [SerializeField]
    HUDSystem HUDRef;

    GameObject initRef;

    GameObject viewmodelRef;

    GameObject playermodelRef;

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

                playermodelRef = currentPlayer.GetComponent<PlayerInput>().selfBodyModel.transform.parent.gameObject;
                GameObject.Destroy(playermodelRef.GetComponentInChildren<Animator>());
                GameObject.Destroy(currentPlayer.GetComponent<PlayerInput>().selfBodyModel.gameObject);
                playermodelRef = Instantiate(Class[2], playermodelRef.transform);


                Debug.Log((currentPlayer.GetComponent<PlayerInput>().selfGunModel.name));
                viewmodelRef = currentPlayer.GetComponent<PlayerInput>().selfGunModel.transform.parent.gameObject;
                GameObject.Destroy(currentPlayer.GetComponent<PlayerInput>().selfGunModel.gameObject);
                viewmodelRef = Instantiate(Class[3], currentPlayer.transform.Find("Head"));

                if (currentPlayer.GetComponent<PlayerInput>().authority)
                {
                    GameObject.Destroy(HUDRef.GetComponentInChildren<HudConnectScript>().gameObject);
                    initRef = Instantiate(Class[1], HUDRef.transform);
                    initRef.GetComponent<HudConnectScript>().ConnectToHUDSystem();

                   
                }
            }
        }
    }

    public void Connect()
    {
        currentPlayer.transform.GetComponent<AbilityInputSystem>().RegisterAbilities();
        if (currentPlayer.GetComponent<PlayerInput>().authority)
        { 
            currentPlayer.transform.GetComponent<WeaponHolder>().GetHUDReference();
            HUDRef.gunHUDRef.SetCurrentAmmo(currentPlayer.transform.GetComponent<WeaponHolder>().currentAmmo);
            currentPlayer.transform.GetComponent<SanitySystem>().GetHUDReference();
            currentPlayer.transform.GetComponent<PlayerInput>().selfBodyModel = playermodelRef;
            currentPlayer.transform.GetComponent<PlayerInput>().SetLayerRecursively(playermodelRef, 9);
            currentPlayer.transform.GetComponent<PlayerMovement>().anim = playermodelRef.GetComponent<PlayerAnimation>();

            currentPlayer.transform.GetComponent<PlayerInput>().selfGunModel = viewmodelRef;
            // Set Gun Animator
            currentPlayer.transform.GetComponent<WeaponHolder>().animRef = viewmodelRef.GetComponent<Animator>();
            // Set GunNudger Script Values
            viewmodelRef.GetComponent<GunNudger>().playerMovement = currentPlayer.GetComponent<PlayerMovement>();
            currentPlayer.GetComponent<PlayerMovement>().nudger = viewmodelRef.GetComponent<GunNudger>();
            viewmodelRef.GetComponent<GunNudger>().playerWeapon = currentPlayer.GetComponent<WeaponHolder>();
            viewmodelRef.GetComponent<GunNudger>().SetHead(viewmodelRef.transform.parent);
            // Set MuzzleFlash
            currentPlayer.GetComponent<WeaponHolder>().SetMuzzleFlash(viewmodelRef.GetComponent<MuzzleFlashHolder>().MuzzleFlash);
            // Viewmodel Layer
            currentPlayer.transform.GetComponent<PlayerInput>().SetLayerRecursively(viewmodelRef, 10);
        }
    }
}
