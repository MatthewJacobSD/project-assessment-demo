using UnityEngine;

public class WasteItem : MonoBehaviour
{
    [Header("Waste Properties")]
    public WasteType wasteType;
    public string itemName = "Item";

    [Header("Hold Position")]
    public Vector3 holdPosition = new Vector3(0f, -0.3f, 0.5f);
    public Vector3 holdRotation = Vector3.zero;
}
