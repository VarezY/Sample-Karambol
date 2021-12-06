using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text blueScore, yellowScore;

    private int blueInt, yellowInt;
    
    private void Start()
    {
        blueInt = 0;
        yellowInt = 0;
        Event3D.current.onScoring += AddScore;
    }

    private void AddScore(string _tag)
    {
        if (_tag.Equals("Blue"))
        {
            blueInt += 1;
            blueScore.text = blueInt.ToString();
        }
        else if (_tag.Equals("Yellow"))
        {
            yellowInt += 1;
            yellowScore.text = yellowInt.ToString();
        }
    }
}
