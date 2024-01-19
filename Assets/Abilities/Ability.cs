using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{

    [SerializeField]
    public float maxCooldownTime { get; private set; }

    public bool onCooldown = false;

    [SerializeField]
    public float activeTime { get; private set; } //how long the spell is doing 'it's thing', preventing other spells/shooting.

    public bool isActive = false;

    [SerializeField]
    public int sanityCost;

    [SerializeField]
    public HUDSystem HUDRef;

    public int tier;




    public void RecieveHUDReference(HUDSystem HUD,int tier)
    {
        HUDRef = HUD;
        Debug.Log("Ability: " + HUDRef);
        this.tier = tier;
    }

    public abstract void StartAbility();

    public abstract void DemonicStartAbility(); //new function > passing demonic bool. fite me

}
