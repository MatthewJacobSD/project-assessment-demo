using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectInteractor : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public Transform holdParent;

    [Header("Pickup Settings")]
    public float pickupRange = 3f;
    public LayerMask pickupLayer = -1;

    private WasteItem heldItem;
    private Rigidbody heldRigidbody;

    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            TryPickup();
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            TryRelease();
        }

        UpdateUI();
    }

    private void TryPickup()
    {
        if (heldItem != null) return;

        Ray ray = new(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, pickupLayer))
        {
            WasteItem item = hit.collider.GetComponentInParent<WasteItem>();
            if (item != null)
            {
                PickUp(item);
            }
        }
    }

    private void PickUp(WasteItem item)
    {
        heldItem = item;
        heldItem.transform.SetParent(holdParent);
        heldItem.transform.localPosition = item.holdPosition;
        heldItem.transform.localEulerAngles = item.holdRotation;

        heldRigidbody = heldItem.GetComponent<Rigidbody>();
        if (heldRigidbody != null)
        {
            heldRigidbody.isKinematic = true;
        }

        VFXManager.PlayPickupVFX(item.transform.position);
        AudioManager.PlayPickupSFX();

        Debug.Log("Picked up: " + item.itemName);
    }

    private void TryRelease()
    {
        if (heldItem == null) return;

        if (BinZone.IsPlayerInRange)
        {
            GameManager.Instance.ScoreItem(heldItem);
            heldItem = null;
            heldRigidbody = null;
            return;
        }

        DropItem();
    }

    private void DropItem()
    {
        if (heldItem == null) return;

        heldItem.transform.SetParent(null);

        if (heldRigidbody != null)
        {
            heldRigidbody.isKinematic = false;
        }

        heldItem = null;
        heldRigidbody = null;
    }

    private void UpdateUI()
    {
        if (GameUI.Instance == null) return;

        if (heldItem != null)
        {
            GameUI.Instance.SetPrompt("Press Q to release into bin");
            return;
        }

        Ray ray = new(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, pickupLayer))
        {
            WasteItem item = hit.collider.GetComponentInParent<WasteItem>();
            if (item != null)
            {
                GameUI.Instance.SetPrompt("Press E to pick up " + item.itemName);
                return;
            }
        }

        GameUI.Instance.SetPrompt("");
    }
}
