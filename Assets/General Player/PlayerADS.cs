using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerADS : MonoBehaviour
{
    [SerializeField] Animator animRef;

    PlayerInput inputRef;

    private void Start()
    {
            inputRef = transform.parent.parent.GetComponent<PlayerInput>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(inputRef.aimKey))
        {
            animRef.SetBool("ADS", !animRef.GetBool("ADS"));
            animRef.applyRootMotion = !animRef.applyRootMotion;
        }
    }
}
