using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI promptText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (GameManager.Instance == null) return;

        scoreText.text = "Score: " + GameManager.Instance.Score;

        int minutes = Mathf.FloorToInt(GameManager.Instance.TimeRemaining / 60f);
        int seconds = Mathf.FloorToInt(GameManager.Instance.TimeRemaining % 60f);
        timerText.text = string.Format("{0}:{1:D2}", minutes, seconds);

        if (GameManager.Instance.IsGameOver)
        {
            gameOverPanel.SetActive(true);
            finalScoreText.text = "Final Score: " + GameManager.Instance.Score;
            promptText.text = "";
        }
    }

    public void SetPrompt(string text)
    {
        promptText.text = text;
    }
}
