using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [Header("UI (Only in Scene 4)")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI streakText;
    public TextMeshProUGUI highscoreText; 
    public ScoreStreakFX scoreStreakFX;

    private static ScoreManager instance;

    private int currentScore = 0;
    private int currentStreak = 0;
    private int highestScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (!PlayerPrefs.HasKey("HighestScore"))
            PlayerPrefs.SetInt("HighestScore", 0);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        highestScore = PlayerPrefs.GetInt("HighestScore");
        RefreshUIReferences();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshUIReferences();
    }

    private void RefreshUIReferences()
    {
        scoreText = null;
        streakText = null;
        highscoreText = null;
        scoreStreakFX = null;

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            GameObject uiCanvas = GameObject.Find("UI");
            if (uiCanvas != null)
            {
                Transform scoreObj = uiCanvas.transform.Find("ScoreText");
                if (scoreObj != null)
                {
                    scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
                }

                Transform streakObj = uiCanvas.transform.Find("ScoreText/StreakText");
                if (streakObj != null)
                {
                    streakText = streakObj.GetComponent<TextMeshProUGUI>();
                }

                Transform highscoreObj = uiCanvas.transform.Find("StartMenu/Highscore/HighscoreText");
                if (highscoreObj != null)
                {
                    highscoreText = highscoreObj.GetComponent<TextMeshProUGUI>();
                }

                Transform fxObj = uiCanvas.transform.Find("ScoreText");
                if (fxObj != null)
                {
                    scoreStreakFX = fxObj.GetComponent<ScoreStreakFX>();
                }
            }

            if (highscoreText != null)
            {
                highscoreText.text = highestScore.ToString();
            }

            UpdateScoreUI();
            UpdateStreakUI();
        }
    }



    private void SaveHighscore()
    {
        if (currentScore > highestScore)
        {
            highestScore = currentScore;
            PlayerPrefs.SetInt("HighestScore", highestScore);

            if (highscoreText != null)
            {
                highscoreText.text = highestScore.ToString();
            }
        }
    }

    public void AddScore()
    {
        currentStreak++;
        int pointsToAdd = 10 * currentStreak;
        currentScore += pointsToAdd;

        if (scoreStreakFX != null)
        {
            scoreStreakFX.PlayBounceShake();
        }

        UpdateScoreUI();
        UpdateStreakUI();

        SaveHighscore();

        Debug.Log($"Correct! Streak: {currentStreak}, Points Added: {pointsToAdd}, Total Score: {currentScore}");
    }

    public void ResetStreak()
    {
        currentStreak = 0;
        UpdateStreakUI();

        if (scoreStreakFX != null)
        {
            scoreStreakFX.ResetColor();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    private void UpdateStreakUI()
    {
        if (streakText != null)
        {
            streakText.text = currentStreak.ToString();
        }
    }

    public void Restart()
    {
        currentScore = 0;
        currentStreak = 0;
        UpdateScoreUI();
        UpdateStreakUI();
    }

    public int GetScore() => currentScore;
    public int GetStreak() => currentStreak;
}
