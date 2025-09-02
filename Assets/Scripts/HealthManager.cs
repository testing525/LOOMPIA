using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    [Header("Health Settings")]
    public int maxHealth = 5;
    public int currentHealth;

    [Header("UI References (Scene 0 only)")]
    public Image healthImage;
    public Sprite hp4Image;
    public Sprite hp3Image;
    public Sprite hp2Image;
    public Sprite hp1Image;
    public Sprite hp0Image;

    [Header("Other References")]
    public TimerManager timerManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        ResetHealth();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (timerManager == null)
            {
                timerManager = FindAnyObjectByType<TimerManager>();

            }

            GameObject uiCanvas = GameObject.Find("UI");
            if (uiCanvas != null)
            {
                Transform hpObj = uiCanvas.transform.Find("hpBar");
                if (hpObj != null)
                    healthImage = hpObj.GetComponent<Image>();
            }

            if (currentHealth < maxHealth)
            {
                IncreaseHP();
            }

            RefreshLivesUI();
        }
    }

    public void DecreaseHP()
    {
        Debug.Log("decrease hp!");
        currentHealth = Mathf.Max(0, currentHealth - 1);
        RefreshLivesUI();

        if (currentHealth <= 0)
        {
            if (timerManager != null && timerManager.gameManager != null)
            {
                timerManager.gameManager.GameOver();
                AudioManager.FireSFX(AudioManager.SFXSignal.GameOver);
            }
        }
    }

    public void IncreaseHP()
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + 1);
        RefreshLivesUI();
    }

    public void ResetHealth()
    {
        currentHealth = 4; // You might want to use maxHealth here for flexibility
        RefreshLivesUI();
    }

    private void RefreshLivesUI()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 || healthImage == null)
            return;

        switch (currentHealth)
        {
            case 4: healthImage.sprite = hp4Image; break;
            case 3: healthImage.sprite = hp3Image; break;
            case 2: healthImage.sprite = hp2Image; break;
            case 1: healthImage.sprite = hp1Image; break;
            case 0: healthImage.sprite = hp0Image; break;
        }
    }

    public int GetHealth() => currentHealth;
}
