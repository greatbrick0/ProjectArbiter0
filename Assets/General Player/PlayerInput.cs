using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    GameObject cameraRef;
    [SerializeField]
    Transform head;
    PlayerMovement playerMovement;

    Vector3 inputtedMoveDirection = Vector3.zero;
    Vector2 inputtedLookDirection = Vector2.zero;
    bool jumpInputted = false;

    [Header("Inputs")]
    [SerializeField]
    private List<InputAndName> wasdKeysInit = new List<InputAndName> {
        new InputAndName("forward", KeyCode.W),
        new InputAndName("backward", KeyCode.S),
        new InputAndName("left", KeyCode.A),
        new InputAndName("right", KeyCode.D)
    };
    Dictionary<string, KeyCode> wasdKeys = new Dictionary<string, KeyCode> { };
    [SerializeField]
    KeyCode jumpKey = KeyCode.Space;
    public float mouseXSens = 1.0f;
    public float mouseYSens = 1.0f;

    [Serializable]
    public class InputAndName //not even one hour into the project and im already back to my horrendous ways
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
        cameraRef = Instantiate(cameraRef);
        SetUpCamera();
        playerMovement = GetComponent<PlayerMovement>();
        foreach (InputAndName ii in wasdKeysInit)
        {
            wasdKeys.Add(ii.name, ii.input);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ShowMouse();
            playerMovement.SetDefaultMovementEnabled(false);
        }

        inputtedMoveDirection = Vector3.zero;
        if (Input.GetKey(wasdKeys["forward"])) inputtedMoveDirection += transform.forward;
        if (Input.GetKey(wasdKeys["backward"])) inputtedMoveDirection -= transform.forward;
        if (Input.GetKey(wasdKeys["left"])) inputtedMoveDirection -= transform.right;
        if (Input.GetKey(wasdKeys["right"])) inputtedMoveDirection += transform.right;

        if (Input.GetKeyDown(jumpKey)) jumpInputted = true;

        inputtedLookDirection = Vector2.zero;
        inputtedLookDirection.x = Input.GetAxis("Mouse X") * mouseXSens;
        inputtedLookDirection.y = Input.GetAxis("Mouse Y") * mouseYSens;

        playerMovement.SetInputs(inputtedMoveDirection, jumpInputted, inputtedLookDirection);
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
}
