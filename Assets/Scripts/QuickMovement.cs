using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float speed = 5.0f;
    [SerializeField]
    float turnSpeed = 2.0f;
    [SerializeField]
    float jumpForce = 10.0f;
    [SerializeField]
    GameObject cameraRef;
    [SerializeField]
    Transform head;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 5;
        cameraRef = Instantiate(cameraRef);
        SetUpCamera();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.W)) rb.AddForce(transform.forward * speed, ForceMode.Force);
        if (Input.GetKey(KeyCode.S)) rb.AddForce(-transform.forward * speed, ForceMode.Force);
        if (Input.GetKey(KeyCode.A)) rb.AddForce(-transform.right * speed, ForceMode.Force);
        if (Input.GetKey(KeyCode.D)) rb.AddForce(transform.right * speed, ForceMode.Force);

        if (Input.GetKeyDown(KeyCode.Space)) rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

        if (Input.GetKey(KeyCode.Q)) transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.E)) transform.Rotate(-Vector3.up * turnSpeed * Time.deltaTime);

    }

    private void SetUpCamera()
    {
        cameraRef.transform.parent = transform.parent;
        cameraRef.GetComponent<MainCameraScript>().playerHead = head;
        cameraRef.GetComponent<MainCameraScript>().playerEyes = head.GetChild(0);
    }
}
