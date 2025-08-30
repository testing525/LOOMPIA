using System.Collections;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject activityMenu;
    public UIManager uiManager;
    public Guide guide;

    void Start()
    {
        uiManager.HideAllUI();

        AddCanvasGroup(startMenu);
        AddCanvasGroup(gameOverMenu);
    }

    public void Retry()
    {
        if (gameManager != null)
        {
            gameManager.gameStarted = true;
            gameManager.StartNewRound();
        }
        uiManager.ToggleAllUI();
        FadeOutMenu(gameOverMenu);
        HealthManager.Instance.ResetHealth();
        AudioManager.FireMusic(AudioManager.MusicSignal.Question);
        guide.PlayGuide();
    }

    public void Play()
    {
        if (gameManager != null)
        {
            gameManager.gameStarted = true;
            gameManager.StartNewRound();
            AudioManager.FireMusic(AudioManager.MusicSignal.Subject);

        }

        uiManager.ToggleAllUI();
        //healthManager.ResetHealth();
        FadeOutMenu(startMenu);
        guide.PlayGuide();
    }

    public void ShowActivityMenu()
    {
        if (activityMenu != null)
        {
            activityMenu.SetActive(true); 
        }
    }

    public void HideActivityMenu()
    {
        if (activityMenu != null)
        {
            activityMenu.SetActive(false);
        }
    }

    public void Home()
    {
        FadeOutMenu(gameOverMenu);
        FadeInMenu(startMenu);
    }

    public void DisplayMenu(bool state, string menu)
    {
        switch (menu)
        {
            case "startMenu":
                if (startMenu != null)
                {
                    if (state) FadeInMenu(startMenu);
                    else FadeOutMenu(startMenu);
                }
                break;

            case "gameOverMenu":
                if (gameOverMenu != null)
                {
                    if (state) FadeInMenu(gameOverMenu);
                    else FadeOutMenu(gameOverMenu);
                }
                break;
        }
    }

    private void AddCanvasGroup(GameObject menu)
    {
        if (menu == null) return;
        if (menu.GetComponent<CanvasGroup>() == null)
        {
            menu.AddComponent<CanvasGroup>().alpha = 1f;
        }
    }

    private void FadeOutMenu(GameObject menu)
    {
        if (menu == null) return;
        CanvasGroup canvasGroup = menu.GetComponent<CanvasGroup>();
        StartCoroutine(FadeCoroutine(menu, canvasGroup, false));
    }

    private void FadeInMenu(GameObject menu)
    {
        if (menu == null) return;
        CanvasGroup canvasGroup = menu.GetComponent<CanvasGroup>();
        menu.SetActive(true);
        StartCoroutine(FadeCoroutine(menu, canvasGroup, true));
    }

    private IEnumerator FadeCoroutine(GameObject menu, CanvasGroup canvasGroup, bool fadeIn)
    {
        float duration = 0.5f;
        float startAlpha = canvasGroup.alpha;
        float targetAlpha = fadeIn ? 1f : 0f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t / duration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        if (!fadeIn) menu.SetActive(false); 
    }
}
