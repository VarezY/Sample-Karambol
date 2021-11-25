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
        if (_outline == null)
        {
            _outline = gameObject.AddComponent<Outline>();
            _outline.enabled = false;
        }
        if (_objectController == null)
        {
            _objectController = gameObject.AddComponent<ObjectController>();
            _objectController.enabled = true;
        }
        Event3D.current.onObjectClicked += OutlineOn;
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
}
