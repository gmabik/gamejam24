using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public bool isBeingKicked;
    [SerializeField] private float kickCooldown;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if(isBeingKicked) StartCoroutine(ResetKickFlag());
    }

    private IEnumerator ResetKickFlag()
    {
        yield return new WaitForSeconds(kickCooldown);
        isBeingKicked = false;
    }

    public void ResetPos()
    {
        transform.position = startPos;
    }
}
