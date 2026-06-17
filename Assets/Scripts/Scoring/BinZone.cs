using UnityEngine;

public class BinZone : MonoBehaviour
{
    public static bool IsPlayerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInRange = true;
            Debug.Log("Player entered bin zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInRange = false;
            Debug.Log("Player left bin zone");
        }
    }
}
