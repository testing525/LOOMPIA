using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Image timerCountdownImage;
    public int max = 20;
    private float currentTime;
    private bool isRunning = true;

    [Header("References")]
    public GameManager gameManager;
    public ScoreManager scoreManager;

    void Start()
    {
        currentTime = max;
    }

    void Update()
    {
        if (!isRunning || currentTime <= 0) return;

        if (gameManager == null || !gameManager.gameStarted) return;

        float multiplier = GameSpeed.Instance != null ? GameSpeed.Instance.GetMultiplier() : 1f;
        currentTime -= Time.deltaTime * multiplier;
        UpdateTimerUI();

        if (currentTime <= 0)
        {
            currentTime = 0;
            isRunning = false;
            DifficultyManager.WrongAnswerOrTimeout();
            GameOver();
        }
    }


    void UpdateTimerUI()
    {
        timerCountdownImage.fillAmount = currentTime / max;
    }

    public void AddTime(int amount = 3)
    {
        if (!isRunning) return;

        currentTime += amount;
        if (currentTime > max)
            currentTime = max;

        UpdateTimerUI();
    }

    public void GameOver()
    {
        Debug.Log("Game Over (Timer)!");
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
        AudioManager.FireSFX(AudioManager.SFXSignal.GameOver);
        scoreManager.Restart();
    }

    public void StopTimer()
    {
        isRunning = false;
    }


    public void RestartTimer()
    {
        currentTime = max;
        isRunning = true;
        UpdateTimerUI();
    }
}
