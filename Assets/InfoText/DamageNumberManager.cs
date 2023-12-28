using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageDetails;

public class DamageNumberManager : InfoTextManager
{
   
    [Header("Damage Number Settings")]
    [SerializeField, Min(0)]
    [Tooltip("The distance a damage number will drift upward, measured as a percentage of the screen height.")]
    private float numberDriftDist = 0.1f;
    [SerializeField, Min(0)]
    [Tooltip("The max distance that could be chosen to randomly offset a damage number, measured as a percentage of the screen height.")]
    private float maxRandomOffsetDist = 0.0f;

    private void Awake()
    {
        _manager = this;
    }

    public void CreateDamageNumber(int damageAmount, Vector3 hitPos)
    {
        InfoText newInfoText = CreateInfoText(damageAmount.ToString(), hitPos, 0.5f);
        newInfoText.SetExtra(true, Vector3.up * Screen.height * numberDriftDist, RandomPointInCircle(maxRandomOffsetDist * Screen.height));
    }

    private Vector2 RandomPointInCircle(float circleRadius)
    {
        float randomAngle = Random.Range(0, Mathf.PI * 2);
        return new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * Random.Range(0.0f, circleRadius);
    }
}
