using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private Vector3 pos;
    private Vector3 rot;
    private Transform t;

    [SerializeField]
    private Transform followTarget;

    void Start()
    {
        t = transform;
        pos = t.position - followTarget.position;
        rot = t.eulerAngles - followTarget.eulerAngles;
    }

    void Update()
    {
        t.position = followTarget.position + pos;
        t.eulerAngles = followTarget.eulerAngles + rot;
    }
}
