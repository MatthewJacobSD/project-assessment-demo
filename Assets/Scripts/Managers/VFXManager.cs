using UnityEngine;

public static class VFXManager
{
    public static void PlayPickupVFX(Vector3 position)
    {
        Debug.Log("VFX: Play pickup effect at " + position + " (no particle assigned yet)");
    }

    public static void PlayScoreVFX(Vector3 position)
    {
        Debug.Log("VFX: Play score effect at " + position + " (no particle assigned yet)");
    }

    public static void PlayDropVFX(Vector3 position)
    {
        Debug.Log("VFX: Play drop effect at " + position + " (no particle assigned yet)");
    }
}
