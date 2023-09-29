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
    [SerializeField]
    float defualtGravityAccel = 8.0f;

    Vector3 inputtedMoveDirection = Vector3.zero;
    Vector2 hVelocity = Vector2.zero;
    float yVelocity = 0.0f;
    Vector2 inputtedLookDirection = Vector2.zero;
    Vector2 lookDirection = Vector2.zero;
    bool jumpInputted = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
        {
            if(defaultMovementEnabled) GetComponent<PlayerInput>()?.HideMouse();
        }
    }

    public void SetInputs(Vector3 newMove, bool newJump, Vector2 newLook)
    {
        inputtedMoveDirection = newMove;
        jumpInputted = newJump;
        inputtedLookDirection = newLook;
    }

    private void FixedUpdate()
    {
        hVelocity = new Vector2(rb.velocity.x, rb.velocity.z);
        yVelocity = rb.velocity.y;

        if (!defaultMovementEnabled)
        {
            rb.velocity = Vector3.zero;
            return;
        }

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

        yVelocity -= defualtGravityAccel * Time.deltaTime;
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
        GetComponent<PlayerInput>()?.FinishJump();
    }

    public void SetDefaultMovementEnabled(bool newValue)
    {
        defaultMovementEnabled = newValue;
    }
}
