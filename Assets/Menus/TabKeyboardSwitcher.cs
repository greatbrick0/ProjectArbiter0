using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TabKeyboardSwitcher : MonoBehaviour
{
    public UnityEvent escEvent;
    public UnityEvent leftEvent;
    public UnityEvent rightEvent;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) escEvent.Invoke();

    }
}
