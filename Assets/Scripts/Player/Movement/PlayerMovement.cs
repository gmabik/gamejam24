using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;

    public float speed;
    public float gravity;
    public float jumpHeigh;

    public float groundDistance;
    public LayerMask groundMask;
    public KeyCode jumpCode;

    Vector3 velocity;
    public bool isGrounded;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float directionX = -Input.GetAxis("Vertical");
        float directionZ = Input.GetAxis("Horizontal");

        Vector3 move = Vector3.right * directionX + Vector3.forward * directionZ;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(jumpCode) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeigh * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
