using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceActive : MonoBehaviour
{
    [SerializeField] GameObject[] _objects1;
    [SerializeField] GameObject[] _objects2;
    [SerializeField] GameObject[] _objects3;
    [SerializeField] float[] _delayTime;
    void OnEnable()
    {
        StartCoroutine(SetActivateObjects(false, false));
        StartCoroutine(SetActivateObjects(true, true));
    }

    void OnDisable()
    {
        StartCoroutine(SetActivateObjects(false, false));
    }

    IEnumerator SetActivateObjects(bool _isActive, bool withDelay)
    {
        if (withDelay)
            yield return new WaitForEndOfFrame();
        for (int index = 0; index < _objects1.Length; index++)
        {
            if (!_isActive && index == 0)
            {
                ActivateObject(index, true);
            }
            else
            {
                ActivateObject(index, _isActive);
            }
            if (withDelay)
            {
                if (index < _delayTime.Length)
                    yield return new WaitForSeconds(_delayTime[index]);
            }
        }
    }

    void ActivateObject(int index, bool _isActive)
    {
        _objects1[index].SetActive(_isActive);
        if (index < _objects2.Length)
        {
            if (_objects2[index] != null)
            {
                _objects2[index].SetActive(_isActive);
            }
        }
        if (index < _objects3.Length)
        {
            if (_objects3[index] != null)
            {
                _objects3[index].SetActive(_isActive);
            }
        }
    }
}
