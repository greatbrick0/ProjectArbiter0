using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coherence.Toolkit;
using Coherence;

public class PlayerAbilitySystem : MonoBehaviour
{

    //public Animator animator;
    public PlayerMovement movementRef; //When casting, you probably want to reduce movement/input speed.
    private CoherenceSync sync;

    //Hardcoding for 3 abilities for the moment. I hate this cooldown list, but I cant be bothered to make a class right now.
    [SerializeField] private List<AbilityCastInfo> abilities = new List<AbilityCastInfo>(3);
    [SerializeField] private List<float> cooldowns = new List<float>(3); //their respective cooldowns.


    //Sanity Variables
    [SerializeField] private float SanityMaximum = 100;
    [SerializeField] private float targetSanity;
    [SerializeField] private float currentSanity;

    [SerializeField] private float baseSanityRegen;
    [SerializeField] private float improvedSanityRegen;

    private bool sanityDecreasing = false;

    private float sanityRegenPauseTimer;

    //Ability variables for casting
    public float chargeTime;

    public GameObject newAbility;

    public bool inDemonicState;
    //public bool firstDemonicStore; //in case we want something special to happen on the first transformation.
    

    [SerializeField] private GameObject abilityOffset; //default offset between player location and spell origin 

    //Bools that would prevent you from using abilities, others define abilityCastValid
    private bool abilityCastValid = false;
    public bool isCasting { get; private set; } = false;
    protected bool inAnimation_Uncancellable = false;
    protected bool inAnimationOther = false;
    public bool inputsDisabled = false;

    public bool postDemonAbilityLock = false; //After exiting demon form, you have to wait for sanity to fully recharge before you can cast.




    public void Awake()
    {
        sync = GetComponent<CoherenceSync>();
        targetSanity = SanityMaximum;
        //Assign all References
        //animator = GetComponent<Animator>();
    }

    public void AssignAbilities(List<AbilityCastInfo> newAbilities) //probably could save a line, but im tired
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            abilities[i] = newAbilities[i];
        }
    }

    private bool CanCast() //Please run before casting. Checks for reasons that ALL casting is disabled. This is not spell-dependant
    {
        abilityCastValid = (!inAnimation_Uncancellable && !inAnimationOther && !inputsDisabled && !postDemonAbilityLock);
        return abilityCastValid;
    }
    public void AttemptCast(int tier)
    {
        Debug.Log("Attempt Cast for tier " + tier);
        if (CanCast())
        {
            if (cooldowns[tier] <= 0)
            {
                sync.SendCommand<PlayerAbilitySystem>(nameof(CastProcess), MessageTarget.All, tier);
            }
        }
        else //insert castexception here. UI or Audio, probably.
        {

        }
    }

    void Update()
    {

        //Cooldowns for spells
        for (int i = 0; i < cooldowns.Count; i++)
        {
            if (cooldowns[i] > 0.0f)
            {
                cooldowns[i] -= Time.deltaTime;
                //UpdateUICooldowns();
            }
        }

        //Update sanity value.
        if (sanityDecreasing)
        {
            currentSanity = Mathf.Lerp(currentSanity, targetSanity, Time.deltaTime); //lerps down towards new sanity value
            if (currentSanity - targetSanity < 0.5) //'clicks' when it is close enough;
            {
                currentSanity = targetSanity;
                sanityDecreasing = false;
            }
        }
        else
        {
            if (currentSanity < SanityMaximum)
            {
                currentSanity += Time.deltaTime * baseSanityRegen;
                targetSanity = currentSanity;
            }
        }

    }

    public void CastProcess(int tier) //most casting logic.
    {
        Debug.Log("CastProcessstarted-tier " + tier);
        var cast = abilities[tier];
        var cooldownIndex = abilities.IndexOf(cast);

        if (cast.hasCooldown)
        {
            cooldowns[cooldownIndex] = cast.cooldownTime;
        }

        //Sanity cost
        if (cast.sanCost > 0)
        {
            sanityDecreasing = true;
            targetSanity -= cast.sanCost;
        }
        

        if (cast.abilityRef != null) //if spell has a physical component/projectile, etc..
        {
            newAbility = Instantiate(cast.abilityRef, abilityOffset.transform.position, abilityOffset.transform.rotation);
            newAbility.GetComponent<Ability>().SetCaster(this.gameObject);
        }
        
        

        if (cast.castSlowDuration > 0)
        {
            movementRef.ExternalMotionApply(cast.castSlowDuration);
        }

        
        if (targetSanity <= 0 && !inDemonicState)
        {
            DemonTransformation();
        }

    }

    private void DemonTransformation()
    {
        inDemonicState = true;
    }

    private void ExitDemonForm()
    {

    }
}