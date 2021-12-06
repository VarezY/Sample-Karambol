using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DragAndRotate : MonoBehaviour
{
    public bool isActive = false;
    public bool isInverted = false;
    [Range(0, 2)]
    public float speedRotate;

    private float initialDistance;
    private Vector3 initialScale;

    private void Start()
    {
        Event3D.current.onResetPosition += ResetPositionBoard;

    }

    void Update()
    {
        if (isActive)
        {
            if (Input.touchCount == 1)
            {
                Touch screenTouch = Input.GetTouch(0);
 
                if (screenTouch.phase == TouchPhase.Moved)
                {
                    if (isInverted)
                    {
                        transform.Rotate(0f, screenTouch.deltaPosition.x * speedRotate, 0f);
                    }
                    else
                    {
                        transform.Rotate(0f, -screenTouch.deltaPosition.x * speedRotate, 0f);
                    }
                }
 
                if (screenTouch.phase == TouchPhase.Ended)
                {
                    isActive = false;
                }
            }
            else if (Input.touchCount == 2)
            {
                var touchZero = Input.GetTouch(0); 
                var touchOne = Input.GetTouch(1);

                // if one of the touches Ended or Canceled do nothing
                if(touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled  
                                                       || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled) 
                {
                    return;
                }

                // It is enough to check whether one of them began since we
                // already excluded the Ended and Canceled phase in the line before
                if(touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                {
                    // track the initial values
                    initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    initialScale = this.transform.localScale;
                }
                // else now is any other case where touchZero and/or touchOne are in one of the states
                // of Stationary or Moved
                else
                {
                    // otherwise get the current distance
                    var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                    // A little emergency brake ;)
                    if(Mathf.Approximately(initialDistance, 0)) return;

                    // get the scale factor of the current distance relative to the inital one
                    var factor = currentDistance / initialDistance;

                    // apply the scale
                    // instead of a continuous addition rather always base the 
                    // calculation on the initial and current value only
                    this.transform.localScale = initialScale * factor;
                }  
            }
        }
    }
    
    private void ResetPositionBoard()
    {
        transform.DOLocalRotate(new Vector3(0f, 0, 0f), 0.8f);
    }
}
