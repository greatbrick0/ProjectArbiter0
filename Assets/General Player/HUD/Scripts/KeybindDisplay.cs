using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeybindDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private string keybind;
    private void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey(keybind)) text.text = "[" + ((KeyCode)PlayerPrefs.GetInt(keybind)).ToString().ToUpper() + "]";
    }
}
