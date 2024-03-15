using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunNudger : MonoBehaviour
{
    private Transform t;
    private Vector3 originalPos;
    [SerializeField]
    public PlayerMovement playerMovement;

    [HideInInspector]
    public bool walking = false;
    private float time = 0.0f;
    private float walkTransitionWeight = 0.0f;
    [Header("Step Parameters")]
    [SerializeField]
    [Tooltip("The amount of time between each step, measured in seconds.")]
    private float stepFequency = 0.3f;
    [SerializeField]
    private float stepHeight = 0.005f;
    [SerializeField]
    [Tooltip("The amount of time the player has to stop moving for the gun to recover to its original position, measured in seconds.")]
    private float timeToRecover = 0.3f;

    private void Start()
    {
        t = GetComponent<Transform>();
        originalPos = t.localPosition;
        playerMovement.nudger = this;
    }

    private void Update()
    {
        time += 1.0f * Time.deltaTime;
        t.localPosition = originalPos;
        if (walking)
        {
            if (walkTransitionWeight == 0.0f) walkTransitionWeight = 1.0f;
            walkTransitionWeight += 1.0f * Time.deltaTime / timeToRecover;
        }
        else
        {
            if (walkTransitionWeight == 0.0f) time = 0.0f;
            walkTransitionWeight -= 1.0f * Time.deltaTime / timeToRecover;
        }
        walkTransitionWeight = Mathf.Clamp01(walkTransitionWeight);
        t.localPosition += CalculateStepOffset();
    }

    private Vector3 CalculateStepOffset()
    {
        Vector3 output = Vector3.up * -Mathf.Abs(Mathf.Sin(time * Mathf.PI / stepFequency)) * stepHeight;
        if (time > stepFequency) output += Vector3.right * (Mathf.Cos(time * Mathf.PI / stepFequency)) * stepHeight;
        else output += Vector3.right * -(Mathf.Sin(0.5f * time * Mathf.PI / stepFequency)) * stepHeight;
        output = VectorLerp(Vector3.zero, output, walkTransitionWeight);
        return output;
    }

    private Vector3 VectorLerp(Vector3 a, Vector3 b, float t)
    {
        return a + (b - a) * t;
    }
}
