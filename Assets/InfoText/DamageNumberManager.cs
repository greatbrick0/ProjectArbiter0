using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageDetails;

[RequireComponent(typeof(InfoTextManager))]
public class DamageNumberManager : MonoBehaviour
{
    private InfoTextManager textManager;
    private static DamageNumberManager _manager = null;

    [Header("Damage Number Settings")]
    [SerializeField]
    [Tooltip("The amount of time it will take for a damage number to disappear, measured in seconds.")]
    private float numberDuration = 0.5f;
    [SerializeField, Min(0)]
    [Tooltip("The distance a damage number will drift upward, measured as a percentage of the screen height.")]
    private float numberDriftDist = 0.1f;
    [SerializeField, Min(0)]
    [Tooltip("The max distance that could be chosen to randomly offset a damage number, measured as a percentage of the screen height.")]
    private float maxRandomOffsetDist = 0.0f;
    [SerializeField]
    [Tooltip("The opacity of a damage number over the cousre of its lifespan. 1 is opaque and 0 is invisible.")]
    private AnimationCurve opacityCurve = AnimationCurve.Linear(0, 1, 1, 0);
    [SerializeField]
    [Tooltip("The scale of a damage number over the cousre of its lifespan. 1 is the defualt size of the number and 2 is twice the defualt size.")]
    private AnimationCurve scaleCurve = AnimationCurve.Linear(0, 1, 1, 1);

    private void Awake()
    {
        _manager = this;
    }

    public static DamageNumberManager GetManager()
    {
        if (_manager == null) Debug.LogError("No Existing DamageNumberManager");
        return _manager;
    }

    private void Start()
    {
        textManager = GetComponent<InfoTextManager>();
    }

    public void CreateDamageNumber(int damageAmount, Vector3 hitPos)
    {
        InfoText newInfoText = textManager.CreateInfoText(damageAmount.ToString(), hitPos, numberDuration);
        newInfoText.SetExtra(true, Vector3.up * Screen.height * numberDriftDist, RandomPointInCircle(maxRandomOffsetDist * Screen.height));
        newInfoText.SetCurves(scaleCurve, opacityCurve);
    }

    private Vector2 RandomPointInCircle(float circleRadius)
    {
        float randomAngle = Random.Range(0, Mathf.PI * 2);
        return new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * Random.Range(0.0f, circleRadius);
    }
}
