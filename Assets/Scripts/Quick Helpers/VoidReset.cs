using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidReset : MonoBehaviour
{
    Transform t;

    [SerializeField]
    float voidLevel = -10.0f;
    [SerializeField]
    float boostHeight = 20.0f;

    private void Start()
    {
        t = transform;
    }
    void Update()
    {
        if (t.position.y < voidLevel) t.position = t.position + (Vector3.up * boostHeight);
    }
}
