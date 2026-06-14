using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMotor))]
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();

        onFoot.Jump.performed += OnJump;
    }

    private void FixedUpdate()
    {
        Vector2 movement = onFoot.Movement.ReadValue<Vector2>();
        motor.ProcessMove(movement);
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