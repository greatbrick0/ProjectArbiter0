using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionDropdown : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    private List<TMP_Dropdown.OptionData> resolutionList = new List<TMP_Dropdown.OptionData>();

    private void Start()
    {
        dropdown.ClearOptions();
        Debug.Log(Screen.resolutions.Length);

        foreach (Resolution resolution in Screen.resolutions)
        {
            resolutionList.Add(new TMP_Dropdown.OptionData());
            resolutionList[resolutionList.Count - 1].text = resolution.ToString();
        }

        dropdown.AddOptions(resolutionList);
    }
}
