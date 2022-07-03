using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{

    private void Start()
    {
        SelectActionSystem.Instance.OnBusyChanged += Instance_OnBusyChanged;
        Hide();
    }

    private void Instance_OnBusyChanged(object sender, bool isbusy)
    {
        if(isbusy)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }    

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
