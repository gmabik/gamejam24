using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset_Ball : MonoBehaviour
{
    public GameObject Ball;

    private Vector3 BallPosition;

    private void Start()
    {
        BallPosition = Ball.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Ball)
        {
            Ball.transform.position = BallPosition;
        }
    }
}
