using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTextManager : MonoBehaviour
{
    private static InfoTextManager _manager = null;
    public static InfoTextManager manager { 
        get 
        {
            return _manager;
        } 
    }

    [SerializeField]
    private GameObject infoTextPrefab;
    private List<InfoText> infoTexts = new List<InfoText>();

    private void Awake()
    {
        _manager = this;
    }

    public void CreateInfoText(string text, Vector3 pos, float duration)
    {
        CreateInfoText(text, pos, duration, Color.white);
    }

    public void CreateInfoText(string text, Vector3 pos, float duration, Color color)
    {
        InfoText newInfoText = GetAvailableInfoText();
        newInfoText.SetInfoText(text, pos, duration, color);
    }

    private InfoText GetAvailableInfoText()
    {
        foreach(InfoText candidate in infoTexts)
        {
            if (!candidate.active) return candidate;
        }

        infoTexts.Add(Instantiate(infoTextPrefab).GetComponent<InfoText>());
        return infoTexts[^1];
    }
}
