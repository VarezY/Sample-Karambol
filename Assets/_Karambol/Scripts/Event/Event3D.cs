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
}
