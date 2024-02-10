using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUI : MonoBehaviour
{
    [SerializeField] Vector3 activePos;
    [SerializeField] Vector3 inactivePos;
    [SerializeField] Vector3 inactivePosAlt; //UI is not on screen but has gone to a secondary position for the sake of better visual flow

    public void FadeAway(int pos)
    {
        switch (pos)
        {
            case 1:
                LeanTween.move(transform.GetComponent<RectTransform>(), inactivePos, 0.4f).setEaseInOutQuint();
                break;

            case 2:
                LeanTween.move(transform.GetComponent<RectTransform>(), inactivePosAlt, 0.4f).setEaseInOutQuint();
                break;
        }
    }

    public void FadeIn()
    {
        LeanTween.move(transform.GetComponent<RectTransform>(), activePos, 0.4f).setEaseInOutQuint();
    }
}
