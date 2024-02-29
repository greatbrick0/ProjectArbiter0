using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EscInvoker : MonoBehaviour
{
    public UnityEvent escEvent;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) escEvent.Invoke();
    }
}
