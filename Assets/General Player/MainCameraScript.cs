using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainCameraScript : MonoBehaviour
{
    [SerializeField]
    public Transform playerHead;
    [SerializeField]
    public Vector3 offset = Vector3.zero;
    [SerializeField]
    public Transform playerEyes;

    private int spectateIndex = 0;
    [HideInInspector]
    public Transform spectateTarget;
    private PlayerTracker tracker;

    public string mode = "firstperson";

    private Transform t;

    public Volume volume;
    public Volume exhaustVolume;

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
        else if(mode == "spectate")
        {
            t.position = spectateTarget.position;
            t.LookAt(spectateTarget.position + spectateTarget.forward);
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

    public void ChangeSpectateIndex(int amount)
    {
        spectateIndex += amount;
        if (tracker == null) tracker = FindObjectOfType<PlayerTracker>();
        
        spectateTarget = tracker.GetAlivePlayers()[spectateIndex].head;
    }
}
