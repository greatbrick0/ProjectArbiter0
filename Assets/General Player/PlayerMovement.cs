using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Coherence.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField]
    public bool defaultMovementEnabled { get; private set; } = false;

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
    float timeSinceJumpInput = 0.0f;
    Vector3 inputtedMoveDirection = Vector3.zero;

    private Vector2 inputtedLookDirection = Vector2.zero;
    private Vector2 controlledLookDirection = Vector2.zero;
    private Vector2 recoilLookDirection = Vector2.zero;
    [HideInInspector]
    public bool recoilActive = false;
    public Vector2 maxRecoilBounds { private get; set; } = Vector2.one * 20.0f;
    public Vector2 lookDirection {get; private set;} = Vector2.zero;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (defaultMovementEnabled)
        {
            DetermineLookDirection();
        }
    }

    /// <summary>
    /// Calculates which direction the camera should be facing. Takes into account player input, recoil, camera shake, and anything else.
    /// </summary>
    private void DetermineLookDirection()
    {
        if (!recoilActive)
        {
            if (recoilLookDirection.magnitude != 0)
            {
                StopAllCoroutines();
                recoilLookDirection -= recoilLookDirection.normalized * Mathf.Min(recoilLookDirection.magnitude, recoilRecentreSpeed * Time.deltaTime);
            }
        }
        else
        {
            recoilLookDirection.y = Math.Clamp(recoilLookDirection.y, -maxRecoilBounds.y, maxRecoilBounds.y);
            recoilLookDirection.x = Math.Clamp(recoilLookDirection.x, -maxRecoilBounds.x, maxRecoilBounds.x);
        }

        controlledLookDirection += inputtedLookDirection * 12;
        controlledLookDirection.y = Mathf.Clamp(controlledLookDirection.y, -85.0f, 85.0f);
        lookDirection = controlledLookDirection + recoilLookDirection;

        transform.localRotation = Quaternion.Euler(0, lookDirection.x, 0);
        head.localRotation = Quaternion.Euler(lookDirection.y * -1, 0, 0);
    }

    /// <summary>
    /// Tells the PlayerMovement component what actions the player is inputting. 
    /// </summary>
    /// <param name="newMove">The horizontal direction the player is inputting.</param>
    /// <param name="newJump">Whether or not the player has pressed the jump key recently.</param>
    /// <param name="newLook">The direction the player wants to move the camera.</param>
    public void SetInputs(Vector3 newMove, bool newJump, Vector2 newLook)
    {
        if (!defaultMovementEnabled) return;
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

    /// <summary>
    /// Starts applying movement to the camera over time. 
    /// </summary>
    /// <param name="recoilDir">The total distance the camera will be moved, measured in degrees.</param>
    /// <param name="recoilTime">The time over which the recoil is applied, measured in seconds</param>
    public void NewRecoil(Vector2 recoilDir, float recoilTime)
    {
        StartCoroutine(ChangeRecoilDirection(recoilDir, recoilTime));
        recoilActive = true;
    }

    /// <summary>
    /// Applies movement to the camera direction over time via recoilLookDirection. Called in NewRecoil. 
    /// </summary>
    /// <param name="recoilDir">The total distance the camera will be moved, measured in degrees.</param>
    /// <param name="recoilTime">The time over which the recoil is applied, measured in seconds</param>
    /// <returns></returns>
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
