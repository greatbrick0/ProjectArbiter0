using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objectives;

public class GenerationData : MonoBehaviour
{
    [field: SerializeField]
    public Transform entryPoint { get; private set; }
    [field: SerializeField]
    public Transform exitPoint { get; private set; }
}
