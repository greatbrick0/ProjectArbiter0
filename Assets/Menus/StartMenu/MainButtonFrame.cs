using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtonFrame : MonoBehaviour
{
    Vector3 startPos = new Vector3(70, -100, 0);
    Vector3 endPos = new Vector3(-500, -150, 0);
    
    public void FadeAway()
    {
        LeanTween.move(transform.GetComponent<RectTransform>(), endPos, 0.3f).setEaseInExpo();
    }
}
