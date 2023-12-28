using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTextManager : MonoBehaviour
{
    private static InfoTextManager _manager = null;
    public static InfoTextManager manager { 
        get 
        {
            if(_manager == null)
            {
                GameObject managerObj = new GameObject("InfoTextManager", typeof(InfoTextManager));
                _manager = managerObj.GetComponent<InfoTextManager>();
            }
            return _manager;
        }
        private set { _manager = value; }
    }

    [SerializeField] 
    private GameObject infoTextPrefab;
    private List<InfoText> infoTexts = new List<InfoText>();
    private Camera camRef;
    [SerializeField]
    private Transform textCanvas;

    private void Awake()
    {
        _manager = this;
    }

    public void SetCamera(Camera cam)
    {
        camRef = cam;
        foreach(InfoText ii in infoTexts) ii.Initialize(camRef, textCanvas);
    }

    public void SetCanvas(Transform newCanvas)
    {
        textCanvas = newCanvas;
        foreach (InfoText ii in infoTexts) ii.Initialize(camRef, textCanvas);
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

        print("Need bigger pool");
        infoTexts.Add(Instantiate(infoTextPrefab).GetComponent<InfoText>());
        infoTexts[^1].Initialize(camRef, textCanvas);
        return infoTexts[^1];
    }
}
