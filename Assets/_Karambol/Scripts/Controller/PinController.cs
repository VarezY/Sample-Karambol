using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PinController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private bool nearHole;
    [SerializeField] private bool isStop;

    [SerializeField] private GameObject Karambol;

    private ObjectController _objectController;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _objectController = GetComponent<ObjectController>();
    }

    private void Update()
    {
        if (_rigidbody.velocity.magnitude < 0.01f)
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

        if (this.gameObject.name == "Pin User" && PlayerManager.PinMoving)
        {
            if (isStop)
            {
                if (PlayerManager._currentState == "Player1")
                {
                    Debug.Log("Player 2 Initialize");
                    StartCoroutine(ChangePlayer(new Vector3(0f, -0.00635f, -0.002f)));
                }
                else if (PlayerManager._currentState == "Player2")
                {
                    Debug.Log("Player 1 Initialize");
                    StartCoroutine(ChangePlayer(new Vector3(0f, 0.006541523f, -0.001697744f)));
                }
                PlayerManager.PinMoving = false;

            }
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

    IEnumerator ChangePlayer(Vector3 _startPos)
    {
        yield return new WaitForSeconds(3f);

        if (PlayerManager._currentState == "Player2")
        {
            this.transform.localPosition = _startPos;
            PlayerManager._currentState = "Player1";
            Karambol.transform.DOLocalRotate(new Vector3(0f, 0, 0f), 0.8f);
            _objectController.isInverted = false;

        }
        else if (PlayerManager._currentState == "Player1")
        {
            this.transform.localPosition = _startPos;
            PlayerManager._currentState = "Player2";
            Karambol.transform.DOLocalRotate(new Vector3(0f, 180, 0f), 0.8f);
            _objectController.isInverted = true;

        }
    }
}
