using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouch : MonoBehaviour
{
    private GameObject _currentGameObject;
    public GameObject _karambol;
    private DragAndRotate objectScript;
    private Manager3D manager3D;
    void Start()
    {
        Debug.Log("Initialize Touch Object Script");
        objectScript = _karambol.GetComponent<DragAndRotate>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                Debug.Log(raycastHit.collider.gameObject);
                Event3D.current.ObjectClicked(raycastHit.collider.gameObject);
                objectScript.isActive = false;
            }
            else 
            {
                objectScript.isActive = true;
                Event3D.current.ObjectClicked(null);
            }
        }
#else
       if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                Debug.Log(raycastHit.collider.gameObject);
                Event3D.current.ObjectClicked(raycastHit.collider.gameObject);
                objectScript.isActive = false;
            }
            else 
            {
                objectScript.isActive = true;
                Event3D.current.ObjectClicked(null);
            }
        } 
#endif
    }
}
