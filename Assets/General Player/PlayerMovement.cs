using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Coherence.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private bool defaultMovementEnabled = false;

    [Header("References")]
    [SerializeField]
    Transform head;
    Rigidbody rb;
    
    [Header("Movement Variables")]
    [SerializeField]
    float maxMoveSpeed = 3.0f;
    [SerializeField]
    float moveSpeedAccel = 6.0f;
    [SerializeField]
    float moveSpeedDeccel = 3.0f;
    [SerializeField]
    float jumpStrength = 10.0f;
    Vector3 inputtedMoveDirection = Vector3.zero;
    Vector2 hVelocity = Vector2.zero;
    float yVelocity = 0.0f;
    Vector2 inputtedLookDirection = Vector2.zero;
    Vector2 lookDirection = Vector2.zero;
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
        rb = GetComponent<Rigidbody>();
        foreach (InputAndName ii in wasdKeysInit)
        {
            wasdKeys.Add(ii.name, ii.input);
        }
    }

    void Update()
    {
        if (defaultMovementEnabled)
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)) HideMouse();
            else if (Input.GetKey(KeyCode.Escape))
            {
                ShowMouse();
                SetDefaultMovementEnabled(false);
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
        }
    }

    private void FixedUpdate()
    {
        hVelocity = new Vector2(rb.velocity.x, rb.velocity.z);
        yVelocity = rb.velocity.y;

        if(inputtedMoveDirection.sqrMagnitude == 0)
        {
            if (hVelocity.sqrMagnitude <= moveSpeedDeccel * Time.deltaTime) hVelocity = Vector3.zero;
            else hVelocity -= hVelocity.normalized * moveSpeedDeccel * Time.deltaTime;
        }
        else
        {
            hVelocity += new Vector2(inputtedMoveDirection.x, inputtedMoveDirection.z).normalized * moveSpeedAccel * Time.deltaTime;
            hVelocity = Vector2.ClampMagnitude(hVelocity, maxMoveSpeed);
        }

        if (jumpInputted) PlayerJump();

        rb.velocity = new Vector3(hVelocity.x, yVelocity, hVelocity.y);
        lookDirection += inputtedLookDirection * 12;
        transform.localRotation = Quaternion.Euler(0, lookDirection.x, 0);
        lookDirection.y = Math.Clamp(lookDirection.y, -85.0f, 85.0f);
        head.localRotation = Quaternion.Euler(lookDirection.y * -1, 0, 0);
    }

    private void PlayerJump()
    {
        jumpInputted = false;
        yVelocity = jumpStrength;
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
    }

    public void SetDefaultMovementEnabled(bool newValue)
    {
        defaultMovementEnabled = newValue;
    }
}
