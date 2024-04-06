using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset_Ball : MonoBehaviour
{
    private GameObject ball;

    private Vector3 ballPosition;

    private void Start()
    {
        ballPosition = ball.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball)
        {
            ball.transform.position = ballPosition;
        }
    }
}
