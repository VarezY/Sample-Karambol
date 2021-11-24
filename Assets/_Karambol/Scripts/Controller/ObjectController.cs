using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private Outline _outline;
    
    private void OutlineOn()
    {
        if (_outline == null)
        {
            _outline = gameObject.AddComponent<Outline>();
        }
        _outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        _outline.OutlineColor = Color.yellow;
    }

    private void OutlineOff()
    {
        
    }

    
}
