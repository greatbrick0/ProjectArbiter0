using System.Collections.Generic;
using UnityEngine;
using DamageDetails;
using UnityEngine.Events;


public class GunButton : Damageable
{
    [SerializeField]
    private GameObject readyModel;
    [SerializeField]
    private GameObject usedModel;
    [SerializeField]
    private List<DamageElement> acceptedElements = new List<DamageElement>();
    public UnityEvent pressedEvent;

    public override int TakeDamage(int damageAmount, DamageSource sourceType, DamageSpot spotType, DamageElement element = DamageElement.Normal)
    {
        if (acceptedElements.Count == 0 || acceptedElements.Contains(element))
        {
            Press();
            return 1;
        }
        else return 0;
    }

    public void Press()
    {
        readyModel.SetActive(false);
        usedModel.SetActive(true);
        pressedEvent.Invoke();
    }

    public void Refresh()
    {
        readyModel.SetActive(true);
        usedModel.SetActive(false);
    }

    public void ProgressTracker(string tracker = "ButtonsPressed")
    {
        if (tracker.Length == 0) tracker = "ButtonsPressed";
        FindObjectOfType<ObjectiveManager>().UpdateStat(tracker, 1);
    }
}
