using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private float dist;
    private bool dragging = false;
    private Vector3 offset;
    private Transform toDrag;

    public TMP_Text debugText;
    [Range(0, 2)]
    public float slideSpeed = 0.05f;

    private void Start()
    {
        if (debugText == null)
        {
            debugText = GameObject.Find("Debug").GetComponent<TMP_Text>();
        }
    }

    void FixedUpdate () {
        Vector3 v3;
 
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
                toDrag = hit.transform;

                offset = new Vector3(toDrag.position.x, toDrag.position.y, transform.position.z);
                dragging = true;
            }
        }
        if (dragging && touch.phase == TouchPhase.Moved) {
            transform.Translate(touch.deltaPosition.x * slideSpeed, 0f, 0f);
            

        }
        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)) {
            dragging = false;
        }
        pos = transform.position;
        debugText.text = transform.position.ToString();

        // Clamping is still Brokern
        pos.x =  Mathf.Clamp(transform.position.x, -2.0f, 2.0f);
        transform.position = pos;

        
    }
}
