using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager.Team team;
    [Space(20)]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform groundCheck;
    [Space(10)]
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeigh;
    [Space(10)]
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private KeyCode jumpCode;
    [Space(10)]
    private Vector3 velocity;
    [SerializeField] private bool isGrounded;
    [Space(10)]
    [SerializeField] private Animator animator;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float directionX = -Input.GetAxis("Vertical");
        float directionZ = Input.GetAxis("Horizontal");

        if(directionX != 0 || directionZ != 0) 
        {
            animator.SetBool("isRunning", true);
            Vector3 move = Vector3.right * directionX + Vector3.forward * directionZ;
            controller.Move(speed * Time.deltaTime * move);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * 15f);
        }
        else animator.SetBool("isRunning", false);


        if (Input.GetKey(jumpCode) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeigh * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
