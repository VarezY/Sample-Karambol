using System;
using UnityEngine;

namespace _Karambol.Scripts.Event
{
    public class AuthEvent : MonoBehaviour
    {
        public static AuthEvent current;

        private void Awake()
        {
            current = this;
        }
        
        public event Action onLoginEvent;
        public void LoginEvent()
        {
            if (onLoginEvent != null)
            {
                onLoginEvent();
            }
        }
    }
}