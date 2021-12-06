using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerManager : MonoBehaviour
{
    private Outline _outline;
    private ObjectController _objectController;
    
    private void Start()
    {
        _outline = GetComponent<Outline>();
        _objectController = GetComponent<ObjectController>();
        
        Event3D.current.onObjectClicked += OutlineOn;
        Event3D.current.onShootPin += ObjectControllOff;
    }

    private void OutlineOn(GameObject _gameObject)
    {
        if (_gameObject == this.gameObject)
        {
            _outline.enabled = true;
            _outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            _outline.OutlineColor = Color.yellow;
        }
        else
        {
            _outline.enabled = false;
        }
    }

    private void ObjectControllOff()
    {
        _objectController.enabled = false;
    }
    
    
}
