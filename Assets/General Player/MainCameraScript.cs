using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    [SerializeField]
    public Transform playerHead;
    [SerializeField]
    public Vector3 offset = Vector3.zero;
    [SerializeField]
    public Transform playerEyes;

    public string mode = "firstperson";

    private Transform t;

    void Start()
    {
        t = transform;
    }

    void Update()
    {
        if (playerHead == null) mode = "";
        if(mode == "firstperson")
        {
            t.position = playerHead.position + TransformOffset(offset);
            t.LookAt(playerEyes);
        }
    }

    private Vector3 TransformOffset(Vector3 oldVector)
    {
        Vector3 newVector = Vector3.zero;
        newVector += transform.right * oldVector.x;
        newVector += transform.up * oldVector.y;
        newVector += transform.forward * oldVector.z;
        return newVector;
    }

    public void SetFov(float newFov)
    {
        GetComponent<Camera>().fieldOfView = newFov;
    }
}
