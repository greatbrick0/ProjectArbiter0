using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLightController : MonoBehaviour
{
    int currentState;

    [SerializeField]
    MeshRenderer mRenderer;


    [SerializeField]
    Material white;
    [SerializeField]
    Material green;
    [SerializeField]
    Material red;

    public int urgency
    {
        get { return currentState; }
        set { 
            if (currentState < value) 
            {
                currentState = value;
                UpdateLight();
            }
        
        }

    }

    public void Calm()
    {
        currentState = 0;
        UpdateLight();
    }


    void UpdateLight()
    {
        switch (currentState)
        {
            case 0:
                mRenderer.material = white;
                break;
            case 1:
                mRenderer.material = green;
                break;
            case 2:
                mRenderer.material = red;
                break;
        }
    }
}
