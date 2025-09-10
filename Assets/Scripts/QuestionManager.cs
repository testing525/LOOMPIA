using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class QuestionManager : MonoBehaviour
{
    public GameManager gameManager;
    public TimerManager timeManager;
    public ScoreManager scoreManager;
    public UIButtonFlyer flyerManager;
    public PlayerAutoMove playerScript;

    [Header("UI Elements")]
    public TextMeshProUGUI questionText;
    public Button[] problemButtons;
    public Button[] subjectButtons;


    [Header("Subject Assets")]
    public Sprite mathSprite;
    public Sprite litSprite;
    public Sprite scienceSprite;

    public GameObject mathFlyerPrefab;
    public GameObject litFlyerPrefab;
    public GameObject scienceFlyerPrefab;

    private int correctIndex;
    private string currentSubject; // keep track of subject

    public void GetQuestionFor(string subject)
    {
        QuestionData data = null;

        foreach (var btn in problemButtons)
        {
            btn.interactable = true;
        }

        string difficulty = DifficultyManager.GetDifficultyType();
        currentSubject = subject; // remember subject

        switch (subject)
        {
            case "Math":
                data = MathQuestionProvider.GetQuestion(difficulty);
                break;
            case "Literature":
                data = LitQuestionProvider.GetQuestion(difficulty);
                break;
            case "Science":
                data = ScienceQuestionProvider.GetQuestion(difficulty);
                break;
        }

        if (data != null)
        {
            DisplayQuestion(data);

            // Apply subject-specific button sprite
            Sprite chosenSprite = null;
            switch (subject)
            {
                case "Math": chosenSprite = mathSprite; break;
                case "Literature": chosenSprite = litSprite; break;
                case "Science": chosenSprite = scienceSprite; break;
            }

            foreach (var btn in problemButtons)
            {
                btn.gameObject.SetActive(true);
                if (chosenSprite != null)
                    btn.image.sprite = chosenSprite;
            }
        }
    }

    private void DisplayQuestion(QuestionData data)
    {
        questionText.text = data.question;
        correctIndex = data.correctAnswerIndex;

        for (int i = 0; i < problemButtons.Length; i++)
        {
            int index = i;
            problemButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = data.choices[i];
            problemButtons[i].onClick.RemoveAllListeners();
            problemButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }
    }

    private void OnAnswerSelected(int index)
    {
        if (gameManager.spawnedTrashcans.Count == 0) return;
        GameObject trashcan = gameManager.spawnedTrashcans[0];
        Trashcan trashcanComp = trashcan?.GetComponent<Trashcan>();
        playerScript.SetNormalAnimSpeed();

        if (index == correctIndex)
        {
            Debug.Log("Correct answer!");
            AudioManager.FireSFX(AudioManager.SFXSignal.Correct);

            timeManager.AddTime(5);
            scoreManager.AddScore();
            DifficultyManager.CorrectAnswer();
            Button selectedButton = problemButtons[index];
            selectedButton.gameObject.SetActive(false);

            if (gameManager.currentTrashcan != null)
            {
                GameObject solvedTrashcan = gameManager.currentTrashcan;
                trashcanComp = solvedTrashcan.GetComponent<Trashcan>();

                gameManager.spawnedTrashcans.Remove(solvedTrashcan);
                gameManager.currentTrashcan = null;

                Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, problemButtons[index].transform.position);
                Vector3 startWorldPos = Camera.main.ScreenToWorldPoint(screenPos);
                startWorldPos.z = 0f;

                Vector3 targetWorldPos = solvedTrashcan.transform.position + Vector3.up * 0.6f + Vector3.right * -4f;

                // pick correct prefab
                GameObject chosenPrefab = null;
                switch (currentSubject)
                {
                    case "Math": chosenPrefab = mathFlyerPrefab; break;
                    case "Literature": chosenPrefab = litFlyerPrefab; break;
                    case "Science": chosenPrefab = scienceFlyerPrefab; break;
                }

                // use new function with prefab
                flyerManager.SpawnAndMoveWithPrefab(chosenPrefab, startWorldPos, targetWorldPos, 0.75f, 1.25f, (flyer) =>
                {
                    if (trashcanComp != null) trashcanComp.Close();
                    StartCoroutine(FadeAndDestroy(solvedTrashcan, 0.5f, () =>
                    {
                        selectedButton.gameObject.SetActive(true);
                    }));
                });
            }
        }
        else
        {
            Debug.Log("Wrong answer!");
            HealthManager.Instance.DecreaseHP();
            scoreManager.ResetStreak();
            AudioManager.FireSFX(AudioManager.SFXSignal.Wrong);
            DifficultyManager.WrongAnswerOrTimeout();

            if (gameManager.currentTrashcan != null)
            {
                GameObject wrongTrashcan = gameManager.currentTrashcan;

                gameManager.spawnedTrashcans.Remove(wrongTrashcan);
                gameManager.currentTrashcan = null;

                StartCoroutine(FadeAndDestroy(wrongTrashcan, 0.5f));
            }
        }

        StartCoroutine(TransitionManager.MoveUI(
            gameManager.problemChoicesGroup,
            gameManager.offscreenLeft, 0.5f, () =>
            {
                gameManager.subjChoicesGroup.anchoredPosition = gameManager.problemHiddenPos;
                StartCoroutine(TransitionManager.MoveUI(gameManager.subjChoicesGroup, gameManager.subjStartPos, 0.5f, () =>
                {
                    gameManager.OnReturnFromProblem();
                }));
            }));

        GameSpeed.Instance.SetNormal();

        foreach (var btn in problemButtons)
        {
            btn.interactable = false;
        }

        foreach (var btn in subjectButtons)
        {
            btn.interactable = true;
        }
    }

    public IEnumerator FadeAndDestroy(GameObject obj, float delay, System.Action onDestroyed = null)
    {
        if (obj == null)
        {
            onDestroyed?.Invoke();
            yield break;
        }

        yield return new WaitForSeconds(delay);

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        float fadeTime = 0.5f;

        if (sr != null)
        {
            Color startColor = sr.color;
            float elapsed = 0f;

            while (elapsed < fadeTime)
            {
                elapsed += Time.deltaTime;
                sr.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startColor.a, 0f, elapsed / fadeTime));
                yield return null;
            }
        }
        Destroy(obj);
        onDestroyed?.Invoke();
    }

    public void CancelCurrentProblem()
    {
        Debug.Log("Problem canceled because trashcan exited the camera.");

        foreach (var btn in problemButtons)
        {
            btn.gameObject.SetActive(false);
        }

        StartCoroutine(TransitionManager.MoveUI(
            gameManager.problemChoicesGroup,
            gameManager.offscreenLeft, 0.5f, () =>
            {
                gameManager.subjChoicesGroup.anchoredPosition = gameManager.problemHiddenPos;
                StartCoroutine(TransitionManager.MoveUI(gameManager.subjChoicesGroup, gameManager.subjStartPos, 0.5f, () =>
                {
                    gameManager.OnReturnFromProblem();
                }));
            }));
    }
}
