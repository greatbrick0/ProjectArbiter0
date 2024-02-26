using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Coherence.Toolkit;
using UnityEditor.Experimental;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField]
    public bool defaultMovementEnabled { get; private set; } = false;

    [field: SerializeField]
    public bool cameraControlsEnabled { get; private set; } = false;


    public float partialControlValue { get; set; } = 0f; //used to gather a portion of the player's movement when not using defaultMovement

    [Header("References")]
    [SerializeField]
    Transform head;
    Rigidbody rb;
    [SerializeField]
    PlayerAnimation anim;

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
    float recoilRecentreSpeed = 90;

    Vector2 hVelocity = Vector2.zero;
    float yVelocity = 0.0f;

    public bool canJump;
    bool jumpInputted = false;
    float timeSinceJumpInput = 0.0f;
    [field: SerializeField]
    public bool grounded { get; private set; } = false;
    float timeSinceGrounded = 0.0f;

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
        if (cameraControlsEnabled)
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
        inputtedMoveDirection = newMove;
        jumpInputted = newJump;
        inputtedLookDirection = newLook;
    }

    private void FixedUpdate()
    {
        hVelocity = Vec2FromXZ(rb.velocity);
        yVelocity = rb.velocity.y;

        if (!defaultMovementEnabled)
        {
            if (partialControlValue > 0)
            {
                hVelocity = AdditiveMotion(hVelocity, inputtedMoveDirection);
            }
            else
            {
                hVelocity = DeccelerateHorizontal(hVelocity);
                yVelocity = AccelerateGravity(yVelocity);
                anim.walking = false;
            }
        }
        else
        {
            if (inputtedMoveDirection.sqrMagnitude == 0)
            {
                hVelocity = DeccelerateHorizontal(hVelocity);
                anim.walking = false;
            }
            else
            {
                hVelocity = AccelerateHorizontal(hVelocity, inputtedMoveDirection);
                anim.walking = true;
            }
            yVelocity = AccelerateGravity(yVelocity);
            if (jumpInputted && grounded) PlayerJump();
        }

        rb.velocity = new Vector3(hVelocity.x, yVelocity, hVelocity.y);
        grounded = false;
    }

    private float AccelerateGravity(float prevMotion)
    {
        return prevMotion - (defualtGravityAccel * Time.deltaTime);
    }

    private Vector3 DeccelerateHorizontal(Vector2 prevMotion)
    {
        if (prevMotion.sqrMagnitude <= moveSpeedDeccel * Time.deltaTime) return Vector2.zero;
        else return prevMotion - (prevMotion.normalized * moveSpeedDeccel * Time.deltaTime);
    }

    private Vector2 AccelerateHorizontal(Vector2 prevMotion, Vector3 inputMotion)
    {
        prevMotion += Vec2FromXZ(inputMotion).normalized * moveSpeedAccel * Time.deltaTime;
        return Vector2.ClampMagnitude(prevMotion, maxMoveSpeed);
    }

    private Vector2 AdditiveMotion(Vector2 prevMotion, Vector3 inputMotion)
    {
        return prevMotion + (Vec2FromXZ(inputMotion).normalized * moveSpeedAccel * Time.deltaTime * partialControlValue);
    }

    private void PlayerJump()
    {
        jumpInputted = false;
        yVelocity = jumpStrength;
        GetComponent<PlayerInput>()?.FinishJump();
    }

    public void SetEnabledControls(bool newMoveEnabled, bool newCamEnabled)
    {
        defaultMovementEnabled = newMoveEnabled;
        cameraControlsEnabled = newCamEnabled;
    }

    public void SetEnabledControls(bool newValue)
    {
        defaultMovementEnabled = newValue;
        if (defaultMovementEnabled)
            cameraControlsEnabled = true;
        else
            cameraControlsEnabled = false;
    }

    public void SetPartialControl(float newValue)
    {
        partialControlValue = newValue;
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
        float recoilSpeed;

        while(recoilProgress < recoilTime)
        {
            recoilProgress += 1.0f * Time.deltaTime;
            recoilSpeed = Mathf.Lerp(2, 0, recoilProgress / recoilTime);

            recoilLookDirection.x += recoilDir.x / recoilTime * recoilSpeed * Time.deltaTime;
            recoilLookDirection.y += recoilDir.y / recoilTime * recoilSpeed * Time.deltaTime;

            yield return null;
        }
    }

    public void ApplySpellSlow(float modifyValue, float slowDuration) //i guess you can overload this with a forcedirection if that is cooler.
    {
        StartCoroutine(ApplySlow(modifyValue, slowDuration));
    }

    public IEnumerator ApplySlow(float slowValue, float duration)
    {
        maxMoveSpeed += slowValue;
        yield return new WaitForSeconds(duration);
        maxMoveSpeed -= slowValue;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.GetContact(0).normal.y < 0.5) return;
        else if (collision.gameObject.layer == 8) grounded = true;
    }

    private Vector2 Vec2FromXZ(Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.z);
    }

}