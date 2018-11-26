using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuToggle : MonoBehaviour
{

    [SerializeField]
    private Canvas Menu;

    Vector3 outOfBounds = new Vector3(-375, 0, 0);

    public void ToggleMenu(bool isOn)
    {
        if (isOn)
        {
            Menu.transform.localPosition = Vector3.zero;
        }
        else
        {
            Menu.transform.localPosition = outOfBounds;
        }
    }
}
