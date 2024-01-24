using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDetector : MonoBehaviour
{
    private void OnDestroy()
    {
        print(gameObject.name + " destroyed");
    }

    private void OnDisable()
    {
        print(gameObject.name + " disabled");
    }
}
