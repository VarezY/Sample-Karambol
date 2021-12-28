using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private bool dragging = false;

    private TMP_Text debugText;
    public Rigidbody _rigidbody;

    public Transform batasKanan, batasKiri;
    [Range(0, 2)]
    public float slideSpeed = 0.035f;

    Vector2 startPos, endPos, direction; // touch start position, touch end position, swipe direction
    float touchTimeStart, touchTimeFinish, timeInterval; // to calculate swipe time to sontrol throw force in Z direction

    [SerializeField]
    float throwForceInXandY = 1f; // to control throw force in X and Y directions

    [SerializeField]
    float throwForceInZ = 50f;

    public bool isInverted = false;
    
    private void Start()
    {
        // if (debugText == null)
        // {
        //     debugText = GameObject.Find("Debug").GetComponent<TMP_Text>();
        // }

        if (batasKanan == null)
        {
            batasKanan = GameObject.Find("Batas Kanan").transform;
        }

        if (batasKiri == null)
        {
            batasKiri = GameObject.Find("Batas Kiri").transform;
        }

        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        Event3D.current.onShootPin += ShootPin;
    }

    void FixedUpdate () {
        
        #region DragPin Movement

        if (Input.touchCount != 1) {
            dragging = false; 
            return;
        }
 
        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if(touch.phase == TouchPhase.Began) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(pos); 
            if(Physics.Raycast(ray, out hit) && (hit.collider.gameObject == this.gameObject))
            {
                dragging = true;
            }
            touchTimeStart = Time.time;
            startPos = Input.GetTouch (0).position;
        }
        if (dragging && touch.phase == TouchPhase.Moved) {
            if (!isInverted)
            {
                transform.Translate(touch.deltaPosition.x * slideSpeed, 0f, 0f);
            }
            else
            {
                transform.Translate(-touch.deltaPosition.x * slideSpeed, 0f, 0f);

            }
            

        }
        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)) {
            dragging = false;

            // #region Swipe to Shoot
            //
            // touchTimeFinish = Time.time;
            //
            // // calculate swipe time interval 
            // timeInterval = touchTimeFinish - touchTimeStart;
            //
            // // getting release finger position
            // endPos = Input.GetTouch (0).position;
            //
            // // calculating swipe direction in 2D space
            // direction = startPos - endPos;
            //
            // // add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
            // _rigidbody.isKinematic = false;
            // _rigidbody.AddForce (- direction.x * throwForceInXandY, - direction.y * throwForceInXandY, throwForceInZ / timeInterval);
            //
            // #endregion
        }

        Vector3 localPos = transform.localPosition;
        localPos.x = Mathf.Clamp(localPos.x, batasKiri.localPosition.x, batasKanan.localPosition.x);
        transform.localPosition = localPos;

        #endregion
        
        
        // debugText.text = _rigidbody.velocity.ToString();

    }

    private void ShootPin()
    {
        Debug.Log("Shooting Pin");
        _rigidbody.AddForce(-transform.up * 2000f);
    }
}
