using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGlide : MonoBehaviour
{
    private Vector3 pos1 = new Vector3(-7.2f, 6.4f, -3.5f);
    private Vector3 pos2 = new Vector3(3.7f, 6.4f, -10.8f);
    private Vector3 targetPos;

    private void Start()
    {
        targetPos = pos2;
        StartMoving();
    }

    private void Update()
    {
        if (transform.position == targetPos)
        {
            SwapPositions();
            StartMoving();
        }
    }

    private void StartMoving()
    {
        LeanTween.moveLocal(gameObject, targetPos, 25).setEase(LeanTweenType.easeInOutSine);
    }

    private void SwapPositions()
    {
        if (targetPos == pos1) targetPos = pos2;
        else targetPos = pos1;
    }
}
