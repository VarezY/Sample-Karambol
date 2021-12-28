using System.Collections;
using System.Collections.Generic;
using InternshipUnity3D.Widget;
using TMPro;
using UnityEngine;

public class CoinFlipEvent : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    [Range(0, 10)]
    [SerializeField] private float _time;
    
    // Start is called before the first frame update
    void Start()
    {
        // WidgetProxy.Root.ShowPopup("You Go First");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
