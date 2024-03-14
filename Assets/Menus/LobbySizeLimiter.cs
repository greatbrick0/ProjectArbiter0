using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbySizeLimiter : MonoBehaviour
{
    [SerializeField] InputField inputField;

    private void Start()
    {
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string newValue)
    {
        if (newValue != "1" && newValue != "2" && newValue != "3")
        {
            inputField.text = "";
            return;
        }
    }
}
