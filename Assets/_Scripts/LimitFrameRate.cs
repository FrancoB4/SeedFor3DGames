using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFrameRate : MonoBehaviour
{
    [SerializeField] private int framerate = 60;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = framerate;
    }


    void Update()
    {
        
    }
}
