using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerLook))]
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        // Listen for jump input from the Input System
        onFoot.Jump.performed += OnJump;
    }

    private void FixedUpdate()
    {
        // Read WASD / Arrow key movement input
        Vector2 movement = onFoot.Movement.ReadValue<Vector2>();

        // Send movement input to PlayerMotor
        motor.ProcessMove(movement);
    }

    private void LateUpdate()
    {
        // Read mouse movement input
        Vector2 lookInput = onFoot.Look.ReadValue<Vector2>();

        // Send mouse input to PlayerLook
        look.ProcessLook(lookInput);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump input fired");
        motor.Jump();
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Jump.performed -= OnJump;
        onFoot.Disable();
    }
}