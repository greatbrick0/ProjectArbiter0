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
    [SerializeField]
    float recoilRecentreSpeed = 90;

    Vector2 hVelocity = Vector2.zero;
    float yVelocity = 0.0f;

    bool jumpInputted = false;
    Vector3 inputtedMoveDirection = Vector3.zero;
    
    private Vector2 inputtedLookDirection = Vector2.zero;
    private Vector2 controlledLookDirection = Vector2.zero;
    private Vector2 recoilLookDirection = Vector2.zero;
    private float remainingRecoilTime = 0.0f;
    public Vector2 lookDirection {get; private set;} = Vector2.zero;
    

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

        if (defaultMovementEnabled)
        {
            DetermineLookDirection();
        }
    }

    private void DetermineLookDirection()
    {
        if (remainingRecoilTime <= 0)
        {
            if (recoilLookDirection.magnitude != 0)
            {
                StopAllCoroutines();
                recoilLookDirection -= recoilLookDirection.normalized * Mathf.Min(recoilLookDirection.magnitude, recoilRecentreSpeed * Time.deltaTime);
            }
        }
        else remainingRecoilTime -= 1.0f * Time.deltaTime;

        controlledLookDirection += inputtedLookDirection * 12;
        controlledLookDirection.y = Math.Clamp(controlledLookDirection.y, -85.0f, 85.0f);
        lookDirection = controlledLookDirection + recoilLookDirection;

        transform.localRotation = Quaternion.Euler(0, lookDirection.x, 0);
        head.localRotation = Quaternion.Euler(lookDirection.y * -1, 0, 0);
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

    public void NewRecoil(Vector2 recoilDir, float recoilTime, float resetTime)
    {
        StartCoroutine(ChangeRecoilDirection(recoilDir, recoilTime));
        remainingRecoilTime = Mathf.Max(remainingRecoilTime, recoilTime + resetTime);
    }

    private IEnumerator ChangeRecoilDirection(Vector2 recoilDir, float recoilTime)
    {
        float recoilProgress = 0.0f;
        float recoilSpeed = 0.0f;

        while(recoilProgress < recoilTime)
        {
            recoilProgress += 1.0f * Time.deltaTime;
            recoilSpeed = Mathf.Lerp(2, 0, recoilProgress / recoilTime);

            recoilLookDirection.x += recoilDir.x / recoilTime * recoilSpeed * Time.deltaTime;
            recoilLookDirection.y += recoilDir.y / recoilTime * recoilSpeed * Time.deltaTime;

            yield return null;
        }
    }
}
