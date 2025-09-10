using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TutorialManagerScript : MonoBehaviour
{
    [SerializeField] private NPCTutorialScript npcTutorial;

    [Header("UI Parents")]
    [SerializeField] private RectTransform subjectChoicesGroup;
    [SerializeField] private RectTransform problemChoicesGroup;

    [Header("Subject Choice Buttons")]
    [SerializeField] private Button[] subjectButtons;

    [Header("Problem UI")]
    [SerializeField] private TextMeshProUGUI problemText;
    [SerializeField] private Button[] problemButtons;

    [Header("Trashcan Prefab")]
    [SerializeField] private GameObject literatureTrashcanPrefab;
    [SerializeField] private Transform spawnPoint;

    [Header("Guide UI")]
    [SerializeField] private GameObject guide;


    private string[] subjects = { "Math", "Science", "Literature" };
    private string correctSubject = "Literature";

    private string[] problemChoices = { "is", "are", "am" };
    private string correctAnswer = "is";

    private GameObject currentTrashcan;

    private Vector2 subjInitialPos;
    private Vector2 problemInitialPos;

    private void Start()
    {
        subjInitialPos = subjectChoicesGroup.anchoredPosition;
        problemInitialPos = problemChoicesGroup.anchoredPosition;

        subjectChoicesGroup.anchoredPosition = subjInitialPos + new Vector2(0, 400f);
        problemChoicesGroup.anchoredPosition = problemInitialPos + new Vector2(0, 400f);

        foreach (var btn in subjectButtons) btn.interactable = false;

        for (int i = 0; i < subjectButtons.Length; i++)
        {
            int index = i;
            TMP_Text label = subjectButtons[i].GetComponentInChildren<TMP_Text>();
            label.text = subjects[i];

            subjectButtons[i].onClick.RemoveAllListeners();
            subjectButtons[i].onClick.AddListener(() => OnSubjectSelected(subjects[index]));
        }

        for (int i = 0; i < problemButtons.Length; i++)
        {
            int index = i;
            TMP_Text label = problemButtons[i].GetComponentInChildren<TMP_Text>();
            label.text = problemChoices[i];

            problemButtons[i].onClick.RemoveAllListeners();
            problemButtons[i].onClick.AddListener(() => OnProblemSelected(problemChoices[index]));
        }

        DialogueManager.Instance.OnInstructionsFinished += HandleInstructionsFinished;
    }

    private void HandleInstructionsFinished()
    {
        foreach (var btn in subjectButtons)
            btn.interactable = true;

        problemText.text = "What subject is this trashcan?";
        SpawnTrashcan();

        StartCoroutine(TransitionManager.MoveUI(subjectChoicesGroup, subjInitialPos, 0.5f));
    }

    private void OnDestroy()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.OnInstructionsFinished -= HandleInstructionsFinished;
        }
    }

    public void SpawnTrashcan()
    {
        ShowGuide();

        if (currentTrashcan != null) Destroy(currentTrashcan);

        currentTrashcan = Instantiate(literatureTrashcanPrefab, spawnPoint.position, spawnPoint.rotation);

        currentTrashcan.transform.localScale = spawnPoint.localScale;
    }

    private void OnSubjectSelected(string subject)
    {
        var tutorialTrashcan = currentTrashcan.GetComponent<TutorialTrashcan>();

        if (subject == correctSubject)
        {
            Debug.Log("Correct subject chosen: " + subject);
            tutorialTrashcan.CorrectAnswer();

            problemText.text = "She ___ excited to go home";

            StartCoroutine(TransitionManager.MoveUI(subjectChoicesGroup, subjInitialPos + new Vector2(-2000f, 0), 0.5f, () =>
            {
                StartCoroutine(TransitionManager.MoveUI(problemChoicesGroup, problemInitialPos, 0.5f));
            }));
        }
        else
        {
            DialogueManager.Instance.Chat("Wrong! Remember what I said!");
            tutorialTrashcan.WrongAnswer();
        }
    }

    private void OnProblemSelected(string choice)
    {
        var tutorialTrashcan = currentTrashcan.GetComponent<TutorialTrashcan>();

        if (choice == correctAnswer)
        {
            npcTutorial.TriggerSpawn();
            problemText.text = "She is excited to go home";
            DialogueManager.Instance.Chat("Nice! That is correct!");

            Button selectedButton = null;
            problemText.text = "";
            foreach (var btn in problemButtons)
            {
                if (btn.GetComponentInChildren<TMP_Text>().text == choice)
                {
                    selectedButton = btn;
                    selectedButton.gameObject.SetActive(false);
                    break;
                }
            }

            if (selectedButton != null)
            {
                Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, selectedButton.transform.position);
                Vector3 startWorldPos = Camera.main.ScreenToWorldPoint(screenPos);
                startWorldPos.z = 0f;

                Vector3 targetWorldPos = currentTrashcan.transform.position + Vector3.up * 0.6f + Vector3.right * -1.3f;

                FindFirstObjectByType<UIButtonFlyer>().SpawnAndMove(
                    startWorldPos, targetWorldPos, 0.7f, 0.0f, (flyer) =>
                    {
                        tutorialTrashcan.CorrectProblemAnswer();

                        foreach (var btn in subjectButtons) btn.interactable = false;
                        foreach (var btn in problemButtons) btn.interactable = false;

                        StartCoroutine(TransitionManager.MoveUI(problemChoicesGroup,
                            problemInitialPos + new Vector2(-2000f, 0), 0.5f));
                    });
            }
        }
        else
        {
            DialogueManager.Instance.Chat("Did you actually go to school?");
            ShowGuide();
            tutorialTrashcan.WrongAnswer(); 

            StartCoroutine(TransitionManager.MoveUI(problemChoicesGroup, problemInitialPos + new Vector2(-2000f, 0), 0.5f, () =>
            {
                problemText.text = "What subject is this trashcan?";

                SpawnTrashcan();

                foreach (var btn in subjectButtons) btn.interactable = true;

                StartCoroutine(TransitionManager.MoveUI(subjectChoicesGroup, subjInitialPos, 0.5f));
            }));
        }
    }



    public void ShowGuide(float fadeDuration = 2f, float stayDuration = 1f)
    {
        if (guide == null) return;

        guide.SetActive(true);
        StartCoroutine(FadeOutGuide(fadeDuration, stayDuration));
    }

    private IEnumerator FadeOutGuide(float fadeDuration, float stayDuration)
    {
        yield return new WaitForSeconds(stayDuration);

        Image[] images = guide.GetComponentsInChildren<Image>();
        TMP_Text[] texts = guide.GetComponentsInChildren<TMP_Text>();

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);

            foreach (var img in images)
            {
                if (img != null)
                {
                    Color c = img.color;
                    c.a = alpha;
                    img.color = c;
                }
            }

            foreach (var txt in texts)
            {
                if (txt != null)
                {
                    Color c = txt.color;
                    c.a = alpha;
                    txt.color = c;
                }
            }

            yield return null;
        }

        guide.SetActive(false);

        foreach (var img in images)
        {
            if (img != null)
            {
                Color c = img.color;
                c.a = 1f;
                img.color = c;
            }
        }

        foreach (var txt in texts)
        {
            if (txt != null)
            {
                Color c = txt.color;
                c.a = 1f;
                txt.color = c;
            }
        }
    }
}
