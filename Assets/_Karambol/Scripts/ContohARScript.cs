using System;
using System.Collections;
using System.Collections.Generic;
using Niantic.ARDK.AR.Anchors;
using TMPro;
using UnityEngine;

public class ContohARScript : MonoBehaviour
{
    public GameObject _karambol;
    public GameObject cube;
    public TMP_Text _Text;
    public GameObject _placholder;
    public IARAnchor Anchor;

    private void Start()
    {
    }

    public void ScaleKarambol()
    {
        _karambol.transform.localScale *= 0.03f;
    }
    
    public void SetObject()
    {
        _placholder = GameObject.FindWithTag("Placeholder");
        if (_placholder == null)
        {
            _Text.text = "Error";
        }
        else
        {
            _Text.text = _placholder.transform.position.ToString();
        }

        // Instantiate(cube, _placholder.position, _placholder.rotation);
        _karambol.transform.position = _placholder.transform.position;
        _karambol.transform.rotation = new Quaternion(_placholder.transform.rotation.x,
            -_placholder.transform.rotation.y, _placholder.transform.rotation.z, _placholder.transform.rotation.w);
        _placholder.SetActive(false);
    }

    public void ScaleKarambolNormal()
    {
        _karambol.transform.localScale = new Vector3(1f, 1f, 1f);
        _karambol.transform.position = new Vector3(-1.09f, 0f, 0.79f);
    }
}
