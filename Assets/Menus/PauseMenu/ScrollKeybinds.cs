using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollKeybinds : MonoBehaviour
{
    [SerializeField] VerticalLayoutGroup layoutGroup;

    public void AdjustPadding(float val)
    {
        int paddingValue = (int)(val * -118);
        layoutGroup.padding.bottom = paddingValue;
    }
}
