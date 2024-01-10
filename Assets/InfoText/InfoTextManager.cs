using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class InfoTextManager : MonoBehaviour
{
    protected static InfoTextManager _manager = null;

    [Header("Info Text Settings")]
    [SerializeField] 
    private GameObject infoTextPrefab;
    private List<InfoText> infoTexts = new List<InfoText>();
    private Camera camRef;
    [SerializeField]
    [Tooltip("The transform that the prefabs will become a child of. Use SetCanvas() to change during runtime.")]
    private Transform textCanvas;

    protected virtual void Awake()
    {
        _manager = this;
    }

    public static InfoTextManager GetManager()
    {
        if (_manager == null)
        {
            Debug.LogError("No Existing InfoTextManager");
            /*
            GameObject managerObj = new GameObject("InfoTextManager", typeof(InfoTextManager));
            _manager = managerObj.GetComponent<InfoTextManager>();
            */
        }
        return _manager;
    }

    public void SetCamera(Camera cam)
    {
        camRef = cam;
        if(infoTexts.Count > 0)
        {
            foreach (InfoText ii in infoTexts) ii.Initialize(camRef, textCanvas);
        }
    }

    public void SetCanvas(Transform newCanvas)
    {
        textCanvas = newCanvas;
        if (infoTexts.Count > 0)
        {
            foreach (InfoText ii in infoTexts) ii.Initialize(camRef, textCanvas);
        }
    }

    public InfoText CreateInfoText(string text, Vector3 pos, float duration, Color color)
    {
        InfoText newInfoText = GetAvailableInfoText();
        newInfoText.SetInfoText(text, pos, duration, color);
        newInfoText.active = true;
        return newInfoText;
    }

    public InfoText CreateInfoText(string text, Vector3 pos, float duration)
    {
        return CreateInfoText(text, pos, duration, Color.white);
    }

    /// <summary>
    /// Uses object pooling to reduce instancing. Returns either an InfoText that was previously used or a fresh InfoText if none are available. 
    /// </summary>
    /// <returns>An InfoText ready to be used. May require values to be reset from previous uses.</returns>
    private InfoText GetAvailableInfoText()
    {
        if(infoTexts.Count > 0)
        {
            foreach (InfoText candidate in infoTexts)
            {
                if (!candidate.active) return candidate;
            }
        }

        return CreateAvailableInfoText();
    }

    private InfoText CreateAvailableInfoText()
    {
        InfoText newTextObj = Instantiate(infoTextPrefab).GetComponent<InfoText>();
        infoTexts.Add(newTextObj);
        newTextObj.Initialize(camRef, textCanvas);
        return newTextObj;
    }
}
