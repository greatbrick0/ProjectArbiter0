using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRebinder : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    public string activeType;
    public string activeInputName;

    public void SetActiveType(string type)
    {
        activeType = type;
    }
    public void SetActiveInputName(string name)
    {
        activeInputName = name;
    }

    private void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Event detectInput = Event.current;
            if (detectInput.isKey)
            {
                if (detectInput.keyCode == KeyCode.Escape)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    playerInput.RebindKey(activeType, activeInputName, detectInput.keyCode);
                    gameObject.SetActive(false);
                }
            }
            if (detectInput.isMouse)
            {
                switch (detectInput.button)
                {
                    case 0:
                        playerInput.RebindKey(activeType, activeInputName, KeyCode.Mouse0);
                        break;

                    case 1:
                        playerInput.RebindKey(activeType, activeInputName, KeyCode.Mouse1);
                        break;

                    case 2:
                        playerInput.RebindKey(activeType, activeInputName, KeyCode.Mouse2);
                        break;

                    default:
                        break;
                }
                gameObject.SetActive(false);
            }
        }
    }
}
