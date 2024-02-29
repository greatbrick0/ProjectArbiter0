using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TabKeyboardSwitcher : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Key switching events will only be invoked if this object is active. If no object is provided, events will be invoked normally.")]
    GameObject selectionIndicator;
    [Tooltip("If no selection indicator object is given, this boolean can be modified to determine when events are invoked. ")]
    public bool selected;
    
    public UnityEvent leftEvent;
    private bool leftQueued = false;
    public UnityEvent rightEvent;
    private bool rightQueued = false;

    void Update()
    {
        if (selectionIndicator != null) selected = selectionIndicator.activeInHierarchy;
        if (!selected) return;

        if (Input.GetKeyUp((KeyCode)PlayerPrefs.GetInt("left"))) leftQueued = true;
        if (Input.GetKeyUp((KeyCode)PlayerPrefs.GetInt("right"))) rightQueued = true;
    }

    private void LateUpdate()
    {
        //these have to be invoked after queuing, otherwise they chain into each other within a single frame    -S
        if (leftQueued) 
        { 
            leftEvent.Invoke();
            leftQueued = false;
        }
        else if (rightQueued)
        {
            rightEvent.Invoke();
            rightQueued = false;
        }
    }
}
