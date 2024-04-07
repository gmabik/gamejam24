using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float zOffset;
    private void Update()
    {
        transform.position = new Vector3(GameManager.GetInstance().mainPlayer.transform.position.x, transform.position.y, GameManager.GetInstance().mainPlayer.transform.position.z + zOffset);
    }
}
