using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableZoom : MonoBehaviour
{
    Camera cam;
    float minFOV = 12f;
    float maxFOV = 30f;
    float increment = 12f;

    void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        float newFOV = cam.fieldOfView - (scrollWheelInput * increment);
        newFOV = Mathf.Clamp(newFOV, minFOV, maxFOV);
        cam.fieldOfView = newFOV;
    }
}
