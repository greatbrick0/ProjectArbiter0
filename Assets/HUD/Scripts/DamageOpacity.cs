using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DamageOpacity : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve opacityCurve;
    private bool visible = false;
    private float timeVisible = 0.0f;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        image.color = ReplaceAlpha(image.color, 0.0f);
    }

    void Update()
    {
        if (!visible) return;

        timeVisible += 1.0f * Time.deltaTime;
        image.color = ReplaceAlpha(image.color, opacityCurve.Evaluate(timeVisible));
        if (image.color.a <= 0.0f) visible = false;
    }

    public void FullOpacity()
    {
        visible = true;
        timeVisible = 0.0f;
    }

    private Color ReplaceAlpha(Color oldColor, float newAlpha)
    {
        return new Color(oldColor.r, oldColor.g, oldColor.b, newAlpha);
    }
}
