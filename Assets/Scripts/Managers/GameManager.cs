using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public float gameTime = 120f;
    public int correctScore = 10;
    public int wrongScore = -5;

    public int Score { get; private set; }
    public float TimeRemaining { get; private set; }
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        TimeRemaining = gameTime;
        IsGameOver = false;
        Score = 0;
    }

    private void Update()
    {
        if (IsGameOver) return;

        TimeRemaining -= Time.deltaTime;

        if (TimeRemaining <= 0f)
        {
            TimeRemaining = 0f;
            IsGameOver = true;
            Debug.Log("Game Over! Final Score: " + Score);
        }
    }

    public void ScoreItem(WasteItem item)
    {
        if (IsGameOver || item == null) return;

        if (item.wasteType == WasteType.Recyclable)
        {
            Score += correctScore;
            Debug.Log("+10: Recycled " + item.itemName + " correctly!");
        }
        else
        {
            Score += wrongScore;
            Debug.Log("-5: " + item.itemName + " is not recyclable!");
        }

        VFXManager.PlayScoreVFX(item.transform.position);
        AudioManager.PlayScoreSFX();
        Destroy(item.gameObject);
    }
}
