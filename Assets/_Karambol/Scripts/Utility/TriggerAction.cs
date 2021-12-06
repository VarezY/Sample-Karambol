using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.Events;

namespace InternshipUnity3D
{
    public class TriggerAction : MonoBehaviour
    {
        [SerializeField] bool _isTriggerOnStart;
        [SerializeField] int _delayTriggerOnStart = 0;
        [SerializeField] UnityEvent _eventTriggerOnStart;

        [SerializeField] bool _isTriggerOnEnable;
        [SerializeField] int _delayTriggerOnEnable = 0;

        [SerializeField] UnityEvent _eventTriggerOnEnable;

        [SerializeField] bool _isTriggerOnDisable;
        [SerializeField] UnityEvent _eventTriggerOnDisable;

        IEnumerator Start()
        {
            if (_isTriggerOnStart)
            {
                yield return new WaitForSeconds(_delayTriggerOnStart);
                _eventTriggerOnStart.Invoke();
            }
        }

        void OnEnable()
        {
            StartCoroutine(IEOnEnable());
        }

        IEnumerator IEOnEnable()
        {
            if (_isTriggerOnEnable)
            {
                yield return new WaitForSeconds(_delayTriggerOnEnable);
                _eventTriggerOnEnable.Invoke();
            }
        }
        void OnDisable()
        {
            StopAllCoroutines();
            _eventTriggerOnDisable.Invoke();
        }

    }


}
