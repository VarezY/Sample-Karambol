using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
        transform.DOLocalRotate(new Vector3(0, 180*2, 0), 3.5f, RotateMode.FastBeyond360);
        transform.DOScale(new Vector3(1, 1, 1), 3.5f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
