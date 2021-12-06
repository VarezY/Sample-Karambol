using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event3D : MonoBehaviour
{
    public static Event3D current;

    private void Awake()
    {
        current = this;
    }

    public event Action<GameObject> onObjectClicked;
    public void ObjectClicked(GameObject _gameObject)
    {
        if (onObjectClicked != null)
        {
            onObjectClicked(_gameObject);
        }
    }
    
    public event Action onResetPosition;
    public void ResetPosition()
    {
        if (onResetPosition != null)
        {
            onResetPosition();
        }
    }
    
    public event Action onShootPin;
    public void ShootPin()
    {
        if (onShootPin != null)
        {
            onShootPin();
        }
    }
    
    public event Action<string> onScoring;
    public void Scoring(string _string)
    {
        if (onScoring != null)
        {
            onScoring(_string);
        }
    }
}
