using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Manager3D : MonoBehaviour
{
    [SerializeField] private Button shootButton, resetButton, resetPosition;

    public GameObject yellowPin, bluePin;
    public Button randomSpawn;

    [SerializeField] private bool _isPinLaunched;
    private void Start()
    {
        shootButton.onClick.AddListener(() => Event3D.current.ShootPin());
        resetButton.onClick.AddListener(() =>
        {
            Application.LoadLevel(Application.loadedLevel);
        });
        resetPosition.onClick.AddListener(() => Event3D.current.ResetPosition());
        randomSpawn.onClick.AddListener(() => SpawnPin());
    }

    private void SpawnPin()
    {
        var ran = Random.value;
        if (ran > 0.5f)
        {
            Instantiate(yellowPin, new Vector3(-14.88f, 3.63f, -0.07f), Quaternion.identity);
        }
        else
        {
            Instantiate(bluePin, new Vector3(-14.88f, 3.63f, -0.07f), Quaternion.identity);

        }
    }
    
    
}
