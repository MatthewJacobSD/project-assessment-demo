using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Camera")]
    public Camera cam;

    [Header("Sensitivity")]
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    private float xRotation = 0f;

    private void Start()
    {
        // Lock cursor into the game view
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Receives mouse input from InputManager and applies camera/player rotation
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // Calculate camera up/down rotation
        xRotation -= mouseY * Time.deltaTime * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // Apply camera up/down rotation
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate player left/right
        transform.Rotate(mouseX * Time.deltaTime * xSensitivity * Vector3.up);
    }
}