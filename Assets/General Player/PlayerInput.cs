using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FMODUnity;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    GameObject cameraRef;
    [SerializeField]
    GameObject selfBodyModel;
    [SerializeField]
    GameObject selfGunModel;
    [SerializeField]
    Transform head;
    PlayerMovement playerMovement;
    WeaponHolder weapon;
    AbilityInputSystem playerAbility;

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
        new InputAndName("ability3", KeyCode.F)
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
        cameraRef = Instantiate(cameraRef);
        SetUpCamera();
        playerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponent<WeaponHolder>();
        playerAbility = GetComponent<AbilityInputSystem>();
        weapon.cam = cameraRef.GetComponent<Camera>();
        foreach (InputAndName ii in wasdKeysInit) wasdKeys.Add(ii.name, ii.input);
        foreach (InputAndName ii in abilityKeysInit) abilityKeys.Add(ii.name, ii.input);
    }

    void Update()
    {
        if (!inMenuBehaviour) DefualtBehaviour();
    }

    public void SetInMenuBehaviour(bool newBehaviour)
    {
        inMenuBehaviour = newBehaviour;
        playerMovement.SetDefaultMovementEnabled(!newBehaviour);
        weapon.SetDefaultBehaviourEnabled(!newBehaviour);
        if (newBehaviour) ShowMouse();
        else HideMouse();
    }

    private void DefualtBehaviour()
    {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            ShowMouse();
            playerMovement.SetDefaultMovementEnabled(false);
            weapon.SetDefaultBehaviourEnabled(false);

            FMODUnity.RuntimeManager.PauseAllEvents(true);
        }
        else if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
        {
            HideMouse();
            playerMovement.SetDefaultMovementEnabled(true);
            weapon.SetDefaultBehaviourEnabled(true);

            FMODUnity.RuntimeManager.PauseAllEvents(false);
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
            if (Input.GetKeyDown(shootKey))
            {
                weapon.StartInput();
            }
            else if (Input.GetKeyUp(shootKey))
            {
                weapon.EndInput();
            }
            if (Input.GetKeyDown(reloadKey))
            {
                weapon.StartReload();
            }
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

    private void SetUpCamera()
    {
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
}
