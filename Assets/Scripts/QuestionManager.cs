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

    private int correctIndex;

    public void GetQuestionFor(string subject)
    {
        QuestionData data = null;

        string difficulty = DifficultyManager.GetDifficultyType();

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

            foreach (var btn in problemButtons)
                btn.gameObject.SetActive(true);
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

                Vector3 targetWorldPos = solvedTrashcan.transform.position + Vector3.up * 0.6f + Vector3.right * -1.3f;


                flyerManager.SpawnAndMove(startWorldPos, targetWorldPos, 0.7f, 0.0f, (flyer) =>
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
                StartCoroutine(TransitionManager.MoveUI(
                    gameManager.subjChoicesGroup, gameManager.subjStartPos, 0.5f, () =>
                    {
                        gameManager.OnReturnFromProblem();
                    }));
            }));
    }



}