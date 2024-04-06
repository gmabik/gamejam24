using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time : MonoBehaviour
{
    public float timeRemaining = 24;
    public bool timerIsRunning = false;

    private void Start()
    {
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timerIsRunning > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
}
