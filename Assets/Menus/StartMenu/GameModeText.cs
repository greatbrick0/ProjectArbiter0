using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameModeText : MonoBehaviour
{
    [SerializeField] Vector3 activePos;
    [SerializeField] Vector3 inactivePos;
    [SerializeField] float visibleHeight;
    [SerializeField] bool isDescription;

    float textAlpha;
    Color textColorReference;

    private void Start()
    {
        transform.localPosition = inactivePos;
        textColorReference = GetComponent<TextMeshProUGUI>().color;
    }

    public void MoveTextUp()
    {
        LeanTween.value(gameObject, Reposition, inactivePos, activePos, 0.05f);
    }

    public void MoveTextDown()
    {
        LeanTween.value(gameObject, Reposition, activePos, inactivePos, 0.05f);
    }

    void Reposition(Vector3 val)
    {
        transform.localPosition = val;
    }

    private void Update()
    {
        if (isDescription && transform.localPosition.y <= visibleHeight)
        {
            textAlpha = 0;
        }
        else
        {
            textAlpha = 255;
        }
        gameObject.GetComponent<TextMeshProUGUI>().color = new Color(textColorReference.r, textColorReference.g, textColorReference.b, textAlpha);
    }
}
