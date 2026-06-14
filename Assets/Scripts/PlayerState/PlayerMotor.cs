using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    [Header("Movement")]
    public float speed = 7f;
    public float gravity = -30f;
    public float jumpHeight = 0.6f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Receives movement input from InputManager and applies movement to the CharacterController
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = new(input.x, 0f, input.y);

        // Prevent faster diagonal movement
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        // Move player horizontally based on input direction
        controller.Move(speed * Time.deltaTime * transform.TransformDirection(moveDirection));

        // Keep player grounded when standing on the floor
        if (controller.isGrounded && playerVelocity.y < 0f)
        {
            playerVelocity.y = -2f;
        }

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    // Called by InputManager when jump input is pressed
    public void Jump()
    {
        Debug.Log("Jump called. Grounded: " + controller.isGrounded);

        if (controller.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}