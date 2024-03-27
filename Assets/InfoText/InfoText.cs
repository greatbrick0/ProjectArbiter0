using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoText : MonoBehaviour
{
    public bool active = true;
    private float age = 0.0f;
    private float activeDuration = 0.0f;
    private bool fadeOpacity = false;
    private AnimationCurve opacityCurve = AnimationCurve.Linear(0, 1, 1, 0);
    private AnimationCurve scaleCurve = AnimationCurve.Linear(0, 1, 1, 1);
    private Vector3 virtualPos = Vector3.zero;
    private Vector2 driftVelocity = Vector3.zero;
    private AnimationCurve driftEasing = AnimationCurve.Linear(0, 0, 1, 1);
    private Vector2 textOffset = Vector2.zero;
    [SerializeField]
    private TextMeshProUGUI textComponent;
    [SerializeField]
    private Image imageComponent;
    private Camera cam;

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
        textComponent.color = ReplaceAlpha(color, 1);
        imageComponent.color = ReplaceAlpha(color, 1);
        transform.SetAsFirstSibling();
        SetExtra(false, Vector2.zero, Vector2.zero, false);
        imageComponent.enabled = false;
    }

    public void SetExtra(bool fade, Vector2 velocity, Vector2 offset, bool leftAligned)
    {
        fadeOpacity = fade;
        driftVelocity = velocity;
        textOffset = offset;
        textComponent.rectTransform.localPosition = Vector2.right * (leftAligned ? 100 : 0);
        textComponent.alignment = leftAligned ? TextAlignmentOptions.Left : TextAlignmentOptions.Center;
    }

    public void SetCurves(AnimationCurve newScaleCurve = null, AnimationCurve newOpacityCurve = null, AnimationCurve newDriftEasing = null)
    {
        if (newScaleCurve != null) scaleCurve = newScaleCurve;
        if (newOpacityCurve != null) opacityCurve = newOpacityCurve;
        if (newDriftEasing != null) driftEasing = newDriftEasing;
    }

    public void SetImage(Sprite newSprite)
    {
        imageComponent.enabled = true;
        imageComponent.sprite = newSprite;
    }

    private void Update()
    {
        if (!active) return;
        if(Vector3.Dot(cam.transform.forward, (virtualPos - cam.transform.position).normalized) < 0)
        {
            textComponent.color = ReplaceAlpha(textComponent.color, 0);
            imageComponent.color = ReplaceAlpha(imageComponent.color, 0);
            return;
        }

        age += 1.0f * Time.deltaTime;
        if (age >= activeDuration) Deactivate();
        float ageRatio = age / activeDuration;

        if (fadeOpacity)
        {
            textComponent.color = ReplaceAlpha(textComponent.color, opacityCurve.Evaluate(ageRatio));
            imageComponent.color = ReplaceAlpha(imageComponent.color, opacityCurve.Evaluate(ageRatio));
        }
        transform.localScale = Vector3.one * scaleCurve.Evaluate(ageRatio);
        transform.position = cam.WorldToScreenPoint(virtualPos) + AddZ(textOffset + (driftVelocity * driftEasing.Evaluate(ageRatio)));
    }

    private void Deactivate()
    {
        active = false;
        textComponent.text = string.Empty;
        imageComponent.enabled = false;
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
