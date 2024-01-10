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
    private Vector2 driftVelocity = Vector3.zero;
    private Vector2 textOffset = Vector2.zero;
    private TextMeshProUGUI textComponent;
    private Camera cam;

    private void Awake()
    {
        //cam = Camera.main;
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void Initialize(Camera newCam, Transform canvas)
    {
        cam = newCam;
        transform.SetParent(canvas, true);
        print(cam != null);
    }

    public void SetInfoText(string text, Vector3 pos, float duration, Color color)
    {
        textComponent.text = text;
        virtualPos = pos;
        activeDuration = duration;
        textComponent.color = color;
        transform.SetAsFirstSibling();
    }

    public void SetExtra(bool fade, Vector2 velocity, Vector2 offset)
    {
        fadeOpacity = fade;
        driftVelocity = velocity;
        textOffset = offset;
    }

    private void Update()
    {
        if (!active) return;

        age += 1.0f * Time.deltaTime;
        if (age >= activeDuration) Deactivate();

        if (fadeOpacity) textComponent.color = ReplaceAlpha(textComponent.color, opacityCurve.Evaluate(age / activeDuration));
        transform.position = cam.WorldToScreenPoint(virtualPos) + AddZ(textOffset + (driftVelocity * age));
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

    private Vector3 AddZ(Vector2 oldVec)
    {
        return new Vector3(oldVec.x, oldVec.y, 0);
    }
}
