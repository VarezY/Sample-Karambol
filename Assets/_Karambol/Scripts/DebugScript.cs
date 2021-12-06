using System;
using System.Collections;
using System.Collections.Generic;
using InternshipUnity3D.Widget;
using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(delegate
        {
            WidgetProxy.Root.ShowPopup("Ini hanyalah percobaan untuk menguji Widget di dalam Scene", "Test!", new PopupButton("Ok"));

        });
    }
}
