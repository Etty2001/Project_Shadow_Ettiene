using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    // Crouching Variables
    private bool isCrouching;
    private bool lerpCrouch;
    private float crouchTimer;

    // Sprinting Variables
    private bool sprinting;

    public float gravity = -9f;
    public float jumpHeight = 3f;

    [SerializeField] private float speed = 5f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;

            float p = crouchTimer / 1;

            p *= p;

            if (isCrouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0;
            }
        }
    }

    // Receive the input from our ImputManager.cs and aaply them to our character controller.
    public void ProcessMovement(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;

        moveDirection.x = input.x;
        moveDirection.z = input.y;

        //controller.Move(transform.TransformDirection(moveDirection));
        //controller.Move(moveDirection * speed * Time.deltaTime);
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        playerVelocity.y += gravity * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        isCrouching = !isCrouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;

        if (sprinting)
            speed += 4;
        else
            speed -= 4;
    }
}
