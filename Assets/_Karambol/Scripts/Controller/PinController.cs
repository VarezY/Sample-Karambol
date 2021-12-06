using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private bool nearHole;
    [SerializeField] private bool isStop;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_rigidbody.velocity.magnitude == 0)
        {
            isStop = true;
        }
        else
        {
            isStop = false;
        }
        if (nearHole && isStop)
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        nearHole = !nearHole;
    }

    private void OnTriggerExit(Collider other)
    {
        nearHole = !nearHole;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        // _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
    }
}
