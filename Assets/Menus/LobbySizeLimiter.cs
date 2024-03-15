using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbySizeLimiter : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField, Range(1, 9)]
    int maxLobbySize = 3;

    private void Start()
    {
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string newValue)
    {
        if(newValue == "") return;
        int intValue;
        if(Int32.TryParse(newValue, out intValue))
        {
            if (intValue > maxLobbySize) inputField.text = maxLobbySize.ToString();
            else if (intValue < 1) inputField.text = "1";
        }
        else inputField.text = maxLobbySize.ToString();
    }
}
