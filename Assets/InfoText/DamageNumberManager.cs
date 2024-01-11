using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageDetails;
using System;
using Random = UnityEngine.Random;

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
    [Tooltip("The easing a number will use to move from its starting position to its end position calculated from numberDriftDist. A steeper slope means faster movement.")]
    private AnimationCurve driftEasing = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField]
    [Tooltip("The opacity of a damage number over the cousre of its lifespan. 1 is opaque and 0 is invisible.")]
    private AnimationCurve opacityCurve = AnimationCurve.Linear(0, 1, 1, 0);
    [SerializeField]
    [Tooltip("The scale of a damage number over the cousre of its lifespan. 1 is the defualt size of the number and 2 is twice the defualt size.")]
    private AnimationCurve scaleCurve = AnimationCurve.Linear(0, 1, 1, 1);

    [Header("Text Colours")]
    [SerializeField]
    private Color defualtColour = Color.white;
    [SerializeField]
    private List<ElementAndColour> colourDictInit = new List<ElementAndColour>();
    private Dictionary<DamageElement, Color> colourDict = new Dictionary<DamageElement, Color>();

    [Serializable] public class ElementAndColour //not even one hour into the project and im already back to my horrendous ways
    {
        public ElementAndColour(DamageElement initElement, Color initColour)
        {
            element = initElement;
            colour = initColour;
        }
        [field: SerializeField]
        public DamageElement element { get; private set; }
        [SerializeField]
        public Color colour = Color.black;
    }

    private void Awake()
    {
        _manager = this;
        foreach (ElementAndColour ii in colourDictInit) colourDict.Add(ii.element, ii.colour);
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

    public void CreateDamageNumber(int damageAmount, Vector3 hitPos, DamageElement element)
    {
        Color colourType = defualtColour;
        if (colourDict.ContainsKey(element)) colourType = colourDict[element];

        InfoText newInfoText = textManager.CreateInfoText(damageAmount.ToString(), hitPos, numberDuration, colourType);
        newInfoText.SetExtra(true, Vector3.up * Screen.height * numberDriftDist, RandomPointInCircle(maxRandomOffsetDist * Screen.height));
        newInfoText.SetCurves(scaleCurve, opacityCurve, driftEasing);
    }

    private Vector2 RandomPointInCircle(float circleRadius)
    {
        float randomAngle = Random.Range(0, Mathf.PI * 2);
        return new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * Random.Range(0.0f, circleRadius);
    }
}
