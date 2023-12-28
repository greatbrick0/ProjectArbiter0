using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoText : MonoBehaviour
{
    public bool active = true;
    private float age = 0.0f;
    private float activeDuration = 0.0f;
    private bool fadeOpacity = false;
    [SerializeField]
    private AnimationCurve opacityCurve = AnimationCurve.Linear(0, 1, 1, 0);
    private Vector3 virtualPos = Vector3.zero;
    private Vector3 virtualVelocity = Vector3.zero;
    private TextMeshProUGUI textComponent;
    private Camera cam;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void Initialize(Camera newCam, Transform canvas)
    {
        cam = newCam;
        transform.SetParent(canvas, true);
    }

    public void SetInfoText(string text, Vector3 pos, float duration, Color color)
    {
        textComponent.text = text;
        virtualPos = pos;
        activeDuration = duration;
        textComponent.color = color;
        print(text);
    }

    public void SetExtra(bool fade, Vector3 velocity)
    {
        fadeOpacity = fade;
        virtualVelocity = velocity;
    }

    private void Update()
    {
        if (!active) return;

        age += 1.0f * Time.deltaTime;
        if (age >= activeDuration) Deactivate();

        if (fadeOpacity) textComponent.color = ReplaceAlpha(textComponent.color, opacityCurve.Evaluate(age / activeDuration));
        virtualPos += virtualVelocity;
        transform.position = cam.WorldToScreenPoint(virtualPos);
    }

    private void Deactivate()
    {
        active = false;
        textComponent.text = string.Empty;
        age = 0;
    }

    private Color ReplaceAlpha(Color oldColor, float newAlpha)
    {
        return new Color(oldColor.r, oldColor.g, oldColor.b, newAlpha);
    }
}
