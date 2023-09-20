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
        if(mode == "firstperson")
        {
            t.position = playerHead.position + offset;
            t.LookAt(playerEyes);
        }
    }
}
