using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunNudger : MonoBehaviour
{
    private Transform t;
    private Vector3 originalPos;
    private Quaternion originalRot;
    [SerializeField]
    public PlayerMovement playerMovement;
    [SerializeField]
    public WeaponHolder playerWeapon;
    [SerializeField]
    private Transform head;

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

    [Header("Sway Parameters")]
    [SerializeField]
    private float swayPower = 1.0f;
    [SerializeField, Range(-10.0f, 10.0f)]
    [Tooltip("Changes how powerful the sway is depending on its distance from being settled. " +
        "\nPositive numbers cause the sway to increse in power with more distance." +
        "\nNegative numbers cause the sway to decrease in power with more distance.")]
    private float swayDistancePower = 1.0f;
    private float swayDistanceWeight;
    [SerializeField]
    private float maxSwayDistance = 0.9f;
    private float swayDistance = 0.0f;
    

    private void Start()
    {
        maxSwayDistance = 1 - Mathf.Pow(maxSwayDistance - 1, 2);
        t = GetComponent<Transform>();
        originalPos = t.localPosition;
        originalRot = t.localRotation;
        playerMovement.nudger = this;
        playerWeapon.animRef = GetComponent<Animator>();
        //head = transform.parent;
        //transform.parent = null;
    }

    private void Update()
    {
        swayDistance = Mathf.Abs(Quaternion.Dot(t.rotation, head.rotation * originalRot));
        if (swayDistance < maxSwayDistance) t.rotation = Quaternion.Slerp(t.rotation, head.rotation * originalRot, (maxSwayDistance - swayDistance) / (1 - swayDistance));
        swayDistanceWeight = Mathf.Pow(swayDistance + 1, swayDistancePower);
        t.rotation = Quaternion.Slerp(t.rotation, head.rotation * originalRot, swayPower * swayDistanceWeight * Time.deltaTime);
        //t.eulerAngles = new Vector3(t.eulerAngles.x, t.eulerAngles.y, originalRot.eulerAngles.z);
        


        t.position = head.position + (originalPos.x * t.right) + (originalPos.y * t.up) + (originalPos.z * t.forward);
        t.position += CalculateStepOffset();
    }

    private Vector3 CalculateStepOffset()
    {
        time += 1.0f * Time.deltaTime;
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

    private Vector2 Vec2FromXY(Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.y);
    }

    private Vector3 Vec3FromVec2(Vector2 vec2, float z = 0.0f)
    {
        return new Vector3(vec2.x, vec2.y, z);
    }

    public void SetHead(Transform transform) { head = transform; }
}
