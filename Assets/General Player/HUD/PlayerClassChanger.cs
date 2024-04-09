using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerClassChanger : GunButton
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

    [SerializeField]
    List<PlayerClassChanger> otherClassButtons;

    public override void Press()
    {
       base.Press();
       ChangePlayer();
    }

    public IEnumerator Connect()
    {
        yield return new WaitForEndOfFrame();
        ConnectAll();
        if (currentPlayer.GetComponent<PlayerInput>().authority) 
        {
            ActivateOtherButtons();
            ConnectMe();     
        }

    }


    private void ConnectAll()
    {
        currentPlayer.transform.GetComponent<AbilityInputSystem>().RegisterAbilities();
        currentPlayer.transform.GetComponent<PlayerMovement>().anim = playermodelRef.GetComponent<PlayerAnimation>();
        currentPlayer.transform.GetComponent<WeaponHolder>().animRef = viewmodelRef.GetComponent<Animator>();
        HUDRef.gunHUDRef.SetCurrentAmmo(currentPlayer.transform.GetComponent<WeaponHolder>().currentAmmo);
        viewmodelRef.GetComponent<GunNudger>().playerMovement = currentPlayer.GetComponent<PlayerMovement>();
        currentPlayer.GetComponent<PlayerMovement>().nudger = viewmodelRef.GetComponent<GunNudger>();
        viewmodelRef.GetComponent<GunNudger>().playerWeapon = currentPlayer.GetComponent<WeaponHolder>();
        viewmodelRef.GetComponent<GunNudger>().SetHead(viewmodelRef.transform.parent);
        currentPlayer.transform.GetComponent<PlayerInput>().selfBodyModel = playermodelRef;
        currentPlayer.transform.GetComponent<PlayerInput>().selfGunModel = viewmodelRef;
        currentPlayer.GetComponent<WeaponHolder>().SetMuzzleFlash(viewmodelRef.GetComponent<MuzzleFlashHolder>().MuzzleFlash);

        currentPlayer.transform.GetComponent<PlayerInput>().SetLayerRecursively(viewmodelRef, 11);
    }

    private void ConnectMe()
    {
        currentPlayer.transform.GetComponent<WeaponHolder>().GetHUDReference();
        HUDRef.gunHUDRef.SetCurrentAmmo(currentPlayer.transform.GetComponent<WeaponHolder>().currentAmmo);
        currentPlayer.transform.GetComponent<SanitySystem>().GetHUDReference();
        currentPlayer.transform.GetComponent<PlayerInput>().selfBodyModel = playermodelRef;
        currentPlayer.transform.GetComponent<PlayerInput>().SetLayerRecursively(playermodelRef, 9);
        currentPlayer.transform.GetComponent<PlayerInput>().selfGunModel = viewmodelRef;

        currentPlayer.transform.GetComponent<PlayerInput>().SetLayerRecursively(viewmodelRef, 10);
    }

    private void ActivateOtherButtons()
    {
        foreach(PlayerClassChanger pCC in otherClassButtons)
        {
            pCC.Refresh();
        }
    }

    public void ChangePlayer()
    {
            Debug.Log("Changing player");
            currentPlayer = recentShotPlayer.gameObject;
            if (currentPlayer.transform.GetComponentInChildren<Ability>() != null)
            {
                currentPlayer.GetComponent<AbilityInputSystem>().ChangeClassCancel();
                GameObject originRef = currentPlayer.transform.GetComponentInChildren<Ability>().transform.parent.gameObject;
                GameObject.Destroy(originRef.GetComponentInChildren<Ability>().gameObject);
                initRef = Instantiate(Class[0], originRef.transform);
                StartCoroutine(Connect());
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

                currentPlayer.GetComponent<PlayerMovement>().SetMoveModify(0);    

                if (currentPlayer.GetComponent<PlayerInput>().authority)
                {
                    GameObject.Destroy(HUDRef.GetComponentInChildren<HudConnectScript>().gameObject);
                    initRef = Instantiate(Class[1], HUDRef.transform);
                    initRef.GetComponent<HudConnectScript>().ConnectToHUDSystem();

                   
                }
            }
        }
}
