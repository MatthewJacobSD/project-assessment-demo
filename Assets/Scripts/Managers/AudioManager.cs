using UnityEngine;

public static class AudioManager
{
    private static AudioSource audioSource;

    public static void Init(AudioSource source)
    {
        audioSource = source;
    }

    public static void PlayScoreSFX()
    {
        if (audioSource != null)
        {
            Debug.Log("Audio: Play score SFX (no clip assigned yet)");
        }
    }

    public static void PlayPickupSFX()
    {
        if (audioSource != null)
        {
            Debug.Log("Audio: Play pickup SFX (no clip assigned yet)");
        }
    }

    public static void PlayDropSFX()
    {
        if (audioSource != null)
        {
            Debug.Log("Audio: Play drop SFX (no clip assigned yet)");
        }
    }

    public static void PlayGameOverSFX()
    {
        if (audioSource != null)
        {
            Debug.Log("Audio: Play game over SFX (no clip assigned yet)");
        }
    }
}
