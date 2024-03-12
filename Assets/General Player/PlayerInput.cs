using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FMODUnity;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using Coherence.Toolkit;
using Coherence;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    bool authority = false;
    [Header("References")]
    [SerializeField]
    [Tooltip("The camera that will be instantiated during runtime, mainly for first person use. ")]
    private GameObject cameraObj;
    private GameObject cameraRef;
    [SerializeField]
    [Tooltip("This object and all of its children will be hidden from the player in first person, but shown to other players. ")]
    private GameObject selfBodyModel;
    [SerializeField]
    [Tooltip("This object and all of its children will be shown to the player in first person, but will be hidden from other players. " +
        "\nThis object should be on the layer 'HiddenViewModel' before runtime.")]
    private GameObject selfGunModel;
    [SerializeField]
    [Tooltip("This will determine the position and direction of the camera when it is instantiated. ")]
    private Transform head;
    PlayerMovement playerMovement;
    WeaponHolder weapon;
    AbilityInputSystem playerAbility;

    CoherenceBridge bridgeRef;
    [HideInInspector]
    public PauseMenuConnecter pauseMenu; //REMOVE WHEN UI MANAGER SCRIPT IS MADE

    Vector3 inputtedMoveDirection = Vector3.zero;
    Vector2 inputtedLookDirection = Vector2.zero;
    bool jumpInputted = false;

    [Header("Inputs")]
    [SerializeField]
    private List<InputAndName> wasdKeysInit = new List<InputAndName> {
        new InputAndName("forward", KeyCode.W),
        new InputAndName("backward", KeyCode.S),
        new InputAndName("left", KeyCode.A),
        new InputAndName("right", KeyCode.D),
    };

    Dictionary<string, KeyCode> wasdKeys = new Dictionary<string, KeyCode> { };
    
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode reloadKey = KeyCode.R;
    [SerializeField] KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] private List<InputAndName> abilityKeysInit = new List<InputAndName>
    {
        new InputAndName("ability1", KeyCode.Q),
        new InputAndName("ability2", KeyCode.LeftShift),
        new InputAndName("ability3", KeyCode.E)
    };

    Dictionary<string, KeyCode> abilityKeys = new Dictionary<string, KeyCode> { };

    public float mouseXSens = 1.0f;
    public float mouseYSens = 1.0f;

    bool inMenuBehaviour = true;

    [Serializable] public class InputAndName //not even one hour into the project and im already back to my horrendous ways
    {
        public InputAndName(string initName, KeyCode initInput)
        {
            name = initName;
            input = initInput;
        }
        [field: SerializeField]
        public string name { get; private set; }
        [SerializeField]
        public KeyCode input;
    }

    private void Start()
    {
        SetLayerRecursively(selfBodyModel, 9);
        SetLayerRecursively(selfGunModel, 10);
        SetUpCamera();
        FindComponentReferences();
        DictionariesFromLists();

        //manager references
        InfoTextManager.GetManager().SetCamera(cameraRef.GetComponent<Camera>());
        bridgeRef = FindObjectOfType<CoherenceBridge>();
        bridgeRef.onConnected.AddListener(delegate { SetInMenuBehaviour(false); });
        bridgeRef.onDisconnected.AddListener(delegate { SetInMenuBehaviour(true); });
        pauseMenu = FindObjectOfType<PauseMenuConnecter>();
        pauseMenu.resumeButton.onClick.AddListener(delegate { SetInMenuBehaviour(false); });
        pauseMenu.settingsManager.playerInput = this;

        LoadSettings();
        SetInMenuBehaviour(false);
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            mouseXSens = PlayerPrefs.GetFloat("Sensitivity") / 50;
            mouseYSens = PlayerPrefs.GetFloat("Sensitivity") / 50;
        }
        else
        {
            mouseXSens = 0.3f;
            mouseYSens = 0.3f;
        }
        //cameraRef.GetComponent<MainCameraScript>().SetFov( _ );

        if (PlayerPrefs.HasKey("forward")) wasdKeys["forward"] = (KeyCode)PlayerPrefs.GetInt("forward");
        if (PlayerPrefs.HasKey("backward")) wasdKeys["backward"] = (KeyCode)PlayerPrefs.GetInt("backward");
        if (PlayerPrefs.HasKey("left")) wasdKeys["left"] = (KeyCode)PlayerPrefs.GetInt("left");
        if (PlayerPrefs.HasKey("right")) wasdKeys["right"] = (KeyCode)PlayerPrefs.GetInt("right");
        if (PlayerPrefs.HasKey("ability1")) abilityKeys["ability1"] = (KeyCode)PlayerPrefs.GetInt("ability1");
        if (PlayerPrefs.HasKey("ability2")) abilityKeys["ability2"] = (KeyCode)PlayerPrefs.GetInt("ability2");
        if (PlayerPrefs.HasKey("ability3")) abilityKeys["ability3"] = (KeyCode)PlayerPrefs.GetInt("ability3");
        if (PlayerPrefs.HasKey("jump")) jumpKey = (KeyCode)PlayerPrefs.GetInt("jump");
        if (PlayerPrefs.HasKey("reload")) reloadKey = (KeyCode)PlayerPrefs.GetInt("reload");
        if (PlayerPrefs.HasKey("shoot")) shootKey = (KeyCode)PlayerPrefs.GetInt("shoot");
    }

    /// <summary>
    /// Puts frequently used nearby components in variables for ease of use and initialization between components.
    /// </summary>
    private void FindComponentReferences()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAbility = GetComponent<AbilityInputSystem>();
        weapon = GetComponent<WeaponHolder>();
        weapon.cam = cameraRef.GetComponent<Camera>();
    }

    /// <summary>
    /// Takes the lists that are visible in the inspector and formats them into their hidden dictionaries. 
    /// </summary>
    private void DictionariesFromLists()
    {
        foreach (InputAndName ii in wasdKeysInit) wasdKeys.Add(ii.name, ii.input);
        foreach (InputAndName ii in abilityKeysInit) abilityKeys.Add(ii.name, ii.input);
    }

    private void Update()
    {
        if (!inMenuBehaviour) DefualtBehaviour();
    }

    public void RebindKey(string type, string inputName, KeyCode newBind)
    {
        switch (type)
        {
            case "wasd": //Working with the wasdKeys dictionary
                if (wasdKeys.ContainsKey(inputName)) //Fail case, nothing else executes if the input name doesn't match anything in the dictionary
                {
                    wasdKeys[inputName] = newBind; //Actually binds new key
                }
                break;

            case "ability":
                if (abilityKeys.ContainsKey(inputName))
                {
                    abilityKeys[inputName] = newBind;
                }
                break;

            case "other":
                switch (inputName)
                {
                    case "jump":
                        jumpKey = newBind;
                        break;

                    case "reload":
                        reloadKey = newBind;
                        break;

                    case "shoot":
                        shootKey = newBind;
                        break;

                    default:
                        break;
                }
                break;

            default:
                break;
        }
    }

    public void SetInMenuBehaviour(bool newBehaviour)
    {
        inMenuBehaviour = newBehaviour;
        playerMovement.SetEnabledControls(!newBehaviour);
        weapon.SetDefaultBehaviourEnabled(!newBehaviour);
        //FMODUnity.RuntimeManager.PauseAllEvents(newBehaviour);
        if (newBehaviour) ShowMouse();
        else HideMouse();
    }

    public void ResumeDefaultBehaviour()
    {
        SetInMenuBehaviour(false);
    }

    private void DefualtBehaviour()
    {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            SetInMenuBehaviour(true);
            if (pauseMenu != null) pauseMenu.PauseMenuActive(true);
        }
        
        inputtedMoveDirection = Vector3.zero;
        if (Input.GetKey(wasdKeys["forward"])) inputtedMoveDirection += transform.forward;
        if (Input.GetKey(wasdKeys["backward"])) inputtedMoveDirection -= transform.forward;
        if (Input.GetKey(wasdKeys["left"])) inputtedMoveDirection -= transform.right;
        if (Input.GetKey(wasdKeys["right"])) inputtedMoveDirection += transform.right;

        jumpInputted = Input.GetKey(jumpKey);

        inputtedLookDirection = Vector2.zero;
        inputtedLookDirection.x = Input.GetAxis("Mouse X") * mouseXSens;
        inputtedLookDirection.y = Input.GetAxis("Mouse Y") * mouseYSens;

        playerMovement.SetInputs(inputtedMoveDirection, jumpInputted, inputtedLookDirection);

        if (weapon != null)
        {
            if (Input.GetKeyDown(shootKey)) weapon.StartInput();
            else if (Input.GetKeyUp(shootKey)) weapon.EndInput();

            if (Input.GetKeyDown(reloadKey)) weapon.StartReload();
        }

        if (playerAbility != null)
        {
            if (Input.GetKeyDown(abilityKeys["ability1"])) playerAbility.AttemptCast(0);
            if (Input.GetKeyDown(abilityKeys["ability2"])) playerAbility.AttemptCast(1);
            if (Input.GetKeyDown(abilityKeys["ability3"])) playerAbility.AttemptCast(2);
        }
    }

    public void FinishJump()
    {
        jumpInputted = false;
    }

    public void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inputtedLookDirection = Vector2.zero;
    }

    /// <summary>
    /// Creates a new player camera, deleting the previous one if it exists. 
    /// </summary>
    private void SetUpCamera()
    {
        if (cameraRef != null) Destroy(cameraRef);
        cameraRef = Instantiate(cameraObj);

        cameraRef.transform.parent = transform.parent;
        cameraRef.GetComponent<MainCameraScript>().playerHead = head;
        cameraRef.GetComponent<MainCameraScript>().playerEyes = head.GetChild(0);
    }

    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public void SetAuthority(bool newAuth)
    {
        authority = newAuth;
    }
}
