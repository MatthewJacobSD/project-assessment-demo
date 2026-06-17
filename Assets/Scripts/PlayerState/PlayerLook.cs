using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Camera")]
    public Camera cam;

    [Header("Sensitivity")]
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    [Header("Cursor Settings")]
    public bool startLocked = true;        // Change to false if you want confined by default

    private float xRotation = 0f;
    private bool isCursorLocked = true;

    private void Start()
    {
        if (startLocked)
            LockCursor();
        else
            ConfineCursor();
    }

    private void Update()
    {
        // Toggle cursor lock with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void ProcessLook(Vector2 input)
    {
        // Only process look input when cursor is locked
        if (!isCursorLocked) return;

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

    // Lock cursor to center (standard FPS style)
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorLocked = true;
    }

    // Confine cursor inside game window but allow free movement inside
    public void ConfineCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        isCursorLocked = false;
    }

    // Completely free the cursor (can leave the game window)
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorLocked = false;
    }
}