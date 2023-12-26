using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ChildCollider : MonoBehaviour
{
    [SerializeField]
    private bool invokeOnSwap = true;
    private int objectsTracked = 0;
    [SerializeField]
    UnityEvent CollideEnterEvent;
    [SerializeField]
    UnityEvent CollideStayEvent;
    [SerializeField]
    UnityEvent CollideExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        objectsTracked++;
        if (invokeOnSwap)
        {
            if(objectsTracked == 1) CollideEnterEvent.Invoke();
        }
        else
        {
            CollideEnterEvent.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        CollideStayEvent.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        objectsTracked--;
        if (invokeOnSwap)
        {
            if (objectsTracked == 0) CollideExitEvent.Invoke();
        }
        else
        {
            CollideExitEvent.Invoke();
        }
    }
}
