using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalEvent : MonoBehaviour
{
    public TMP_Text userScore, enemyScore;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide with " + other.tag);
        Event3D.current.Scoring(other.tag);
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
    // private void AddScore()
}
