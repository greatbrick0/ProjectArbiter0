using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    Transform head;
    Rigidbody rb;
    
    [Header("Movement Variables")]
    [SerializeField]
    float maxMoveSpeed = 10.0f;
    [SerializeField]
    float moveSpeedAccel = 10.0f;
    [SerializeField]
    float jumpStrength = 10.0f;
    Vector3 inputtedMoveDirection = Vector3.zero;
    Vector2 inputtedLookDirection = Vector2.zero;

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
            print(wasdKeys);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        inputtedMoveDirection = Vector3.zero;
        if (Input.GetKey(wasdKeys["forward"]))
        {
            inputtedMoveDirection += transform.forward;
        }
        if (Input.GetKey(wasdKeys["backward"]))
        {
            inputtedMoveDirection -= transform.forward;
        }
        if (Input.GetKey(wasdKeys["left"]))
        {
            inputtedMoveDirection -= transform.right;
        }
        if (Input.GetKey(wasdKeys["right"]))
        {
            inputtedMoveDirection += transform.right;
        }

        inputtedLookDirection = Vector2.zero;
        inputtedLookDirection.x = Input.GetAxis("Mouse X") * mouseXSens;
        inputtedLookDirection.y = Input.GetAxis("Mouse Y") * mouseYSens;
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        rb.velocity += inputtedMoveDirection.normalized * moveSpeedAccel;
        if(rb.velocity.sqrMagnitude > maxMoveSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxMoveSpeed;
        }

        transform.Rotate(new Vector3(0, inputtedLookDirection.x * 12, 0));
        head.Rotate(new Vector3(inputtedLookDirection.y * -12, 0, 0));
    }
}
