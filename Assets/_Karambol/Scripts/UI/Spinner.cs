using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Spinner : MonoBehaviour
{
    public GameObject loadingSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        var _image = loadingSprite.GetComponent<Image>();
        _image.transform.DOLocalRotate(new Vector3(0f, 0f, 360f), 0.7f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
