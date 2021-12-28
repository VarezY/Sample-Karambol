using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    [SerializeField] private GameObject Pins;
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
        transform.DOLocalRotate(new Vector3(0, 180*2, 0), 3.5f, RotateMode.FastBeyond360);
        transform.DOScale(new Vector3(1, 1, 1), 3.5f).OnComplete(() => Pins.SetActive(true));
        
        // Pins.SetActive(true);


    }

    
}
