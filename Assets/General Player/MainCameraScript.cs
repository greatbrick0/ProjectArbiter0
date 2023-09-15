using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    [SerializeField]
    Transform playerHead;
    [SerializeField]
    Transform playerEyes;

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
            t.position = playerHead.position;
            t.LookAt(playerEyes);
        }
    }
}
