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





    
    public abstract void StartAbility();

    public abstract void DemonicStartAbility(); //new function > passing demonic bool. fite me

}
