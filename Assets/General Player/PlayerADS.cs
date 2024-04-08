using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerADS : MonoBehaviour
{
    [SerializeField] Animator animRef;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animRef.SetBool("ADS", !animRef.GetBool("ADS"));
            animRef.applyRootMotion = !animRef.applyRootMotion;
        }
    }
}
