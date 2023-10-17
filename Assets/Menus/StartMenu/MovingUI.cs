using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUI : MonoBehaviour
{
    [SerializeField] Vector3 activePos;
    [SerializeField] Vector3 inactivePos;

    public void FadeAway()
    {
        LeanTween.move(transform.GetComponent<RectTransform>(), inactivePos, 0.4f).setEaseInOutQuint();
    }

    public void FadeIn()
    {
        LeanTween.move(transform.GetComponent<RectTransform>(), activePos, 0.4f).setEaseInOutQuint();
    }
}
