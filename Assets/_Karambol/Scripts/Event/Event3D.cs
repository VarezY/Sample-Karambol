using System;
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
    
    public event Action onChangePlayer1;
    public void ChangePlayer1()
    {
        if (onChangePlayer1 != null)
        {
            onChangePlayer1();
        }
    }
    
    public event Action onChangePlayer2;
    public void ChangePlayer2()
    {
        if (onChangePlayer2 != null)
        {
            onChangePlayer2();
        }
    }
}
