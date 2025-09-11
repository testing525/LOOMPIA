using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameStarted = false;

    [Header("UI References")]
    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;

    [Header("Other References")]
    public QuestionManager questionManager;
    public TimerManager timerManager;
    public UIManager uiManager;
    public NotifyScript notifyScript;
    public PlayerAutoMove playerScript;
    [SerializeField] private Animator playerAnimator;

    private readonly List<string> subjects = new List<string>() { "Math", "Literature", "Science" };

    private readonly Queue<string> subjectQueue = new Queue<string>();
    private string currentTrashcanSubject;

    [Header("UI Groups (RectTransform)")]
    public RectTransform subjChoicesGroup;
    public RectTransform problemChoicesGroup;

    [Header("NPC Settings")]
    public EventSpawnManager npcManager;
    public string currentNPCType;

    public Vector2 subjStartPos;
    public Vector2 problemHiddenPos;
    public Vector2 offscreenLeft = new Vector2(-2000, 0);

    [Header("Trashcan Prefabs")]
    public GameObject mathTrashcan;
    public GameObject literatureTrashcan;
    public GameObject scienceTrashcan;
    public GameObject currentTrashcan; 

    private Dictionary<string, GameObject> trashcanPrefabMap;

    [Header("Placeholders (empty scene objects, left→right)")]
    public Transform[] placeholders;

    [Header("Round Settings")]
    public int minTrashcans = 1;
    public int maxTrashcans = 4;
    public int rounds;
    public int roundsForActivity = 3;

    public readonly List<GameObject> spawnedTrashcans = new List<GameObject>();
    private Dictionary<string, Color> subjectColors;

    private const string SubjectPrompt = "What subject is this trashcan?";

    private void Awake()
    {
        trashcanPrefabMap = new Dictionary<string, GameObject>()
        {
            { "Math",       mathTrashcan },
            { "Literature", literatureTrashcan },
            { "Science",    scienceTrashcan }
        };

        subjectColors = new Dictionary<string, Color>()
        {
            { "Math",       new Color(0.451f, 0.882f, 1.000f) }, 
            { "Literature", new Color(0.553f, 1.000f, 0.643f) },   
            { "Science",    new Color(1.000f, 0.447f, 0.392f) }   
        };
    }

    private void Start()
    {
        rounds = 0;
        subjStartPos = subjChoicesGroup.anchoredPosition;
        problemHiddenPos = new Vector2(subjStartPos.x, 800);
        problemChoicesGroup.anchoredPosition = problemHiddenPos;

        SceneTransition.Instance.StartCoroutine(SceneTransition.Instance.FadeOut());

        //StartNewRound();
    }

    public void StartNewRound()
    {
        if (!gameStarted) return;

        if (rounds >= roundsForActivity)
        {
            StopSpawning();

            if (npcManager != null && npcManager.npcs.Length > 0)
            {
                int randomIndex = Random.Range(0, npcManager.npcs.Length);
                currentNPCType = npcManager.npcs[randomIndex].npcName;

                npcManager.SpawnNPC(currentNPCType);

                uiManager.ToggleUI(uiManager.activityMenuFrame);

                StartCoroutine(ReadyActivityEvent());

            }

            return;
        }
        rounds++;


        // round setup 
        for (int i = spawnedTrashcans.Count - 1; i >= 0; i--)
        {
            if (spawnedTrashcans[i] != null) Destroy(spawnedTrashcans[i]);
        }
        spawnedTrashcans.Clear();
        subjectQueue.Clear();
        notifyScript.TriggerNotify();

        questionText.text = SubjectPrompt;

        int maxAllowed = Mathf.Min(maxTrashcans, placeholders.Length);
        int count = Random.Range(minTrashcans, maxAllowed + 1);

        List<string> roundSubjects = new List<string>(count);
        for (int i = 0; i < count; i++)
        {
            string subj = subjects[Random.Range(0, subjects.Count)];
            roundSubjects.Add(subj);
            subjectQueue.Enqueue(subj);
        }

        currentTrashcanSubject = subjectQueue.Peek();

        int startIndex = ComputeStartIndex(count, placeholders.Length);
        for (int i = 0; i < count; i++)
        {
            string subj = roundSubjects[i];
            var prefab = trashcanPrefabMap[subj];

            Transform slot = placeholders[startIndex + i];
            GameObject spawned = Instantiate(prefab, slot.position, slot.rotation);
            spawned.transform.localScale = Vector3.one;

            spawnedTrashcans.Add(spawned);
        }

        foreach (var trashcan in spawnedTrashcans)
        {
            if (trashcan != null)
            {
                Trashcan tc = trashcan.GetComponent<Trashcan>();
                if (tc != null) tc.Close();
            }
        }

        // setup choice buttons
        List<string> shuffledChoices = new List<string>(subjects);

        for (int i = 0; i < shuffledChoices.Count; i++)
        {
            int r = Random.Range(i, shuffledChoices.Count);
            (shuffledChoices[i], shuffledChoices[r]) = (shuffledChoices[r], shuffledChoices[i]);
        }

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int idx = i;
            string subject = shuffledChoices[idx];

            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = subject;

            if (subjectColors.TryGetValue(subject, out Color subjColor))
            {
                choiceButtons[i].GetComponent<Image>().color = subjColor;
            }

            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => OnAnswerSelected(subject));
        }


        timerManager.RestartTimer();

        Debug.Log("Subjects this round: " + string.Join(", ", roundSubjects));
    }

    private IEnumerator ReadyActivityEvent()
    {
        yield return new WaitForSeconds(2f); 

        GameSpeed.Instance.SetStop();
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isRunning", false);
        }

        if (npcManager != null && !string.IsNullOrEmpty(currentNPCType))
        {
            ShowActivityDialogue(currentNPCType);
        }
    }

    private void ShowActivityDialogue(string npcType)
    {
        if (uiManager == null) return;

        uiManager.ToggleUI(uiManager.activityMenuFrame);

        Transform holder = uiManager.activityMenuFrame.transform.Find("Holder/Dialogue");
        if (holder != null)
        {
            TMPro.TextMeshProUGUI dialogueText = holder.GetComponent<TMPro.TextMeshProUGUI>();
            if (dialogueText != null)
            {
                dialogueText.text = GetDialogueForNPC(npcType);
            }
        }

        Button acceptButton = uiManager.activityMenuFrame.transform.Find("Accept")?.GetComponent<Button>();
        Button refuseButton = uiManager.activityMenuFrame.transform.Find("Refuse")?.GetComponent<Button>();

        if (acceptButton != null)
        {
            acceptButton.onClick.RemoveAllListeners();
            acceptButton.onClick.AddListener(() =>
            {
                npcManager.OnPlayerAcceptRequest(npcType);
            });
        }

        if (refuseButton != null)
        {
            refuseButton.onClick.RemoveAllListeners();
            refuseButton.onClick.AddListener(() =>
            {
                uiManager.HideUI(uiManager.activityMenuFrame);
                ResumeSpawning();
            });
        }
    }

    private string GetDialogueForNPC(string npcType)
    {
        switch (npcType)
        {
            case "LibraryEvent":
                return "Hey! Could you lend me a hand arranging these books? There are so few people left to help.";
            case "CulinaryEvent":
                return "Ah, young one! My hands are too weary for cooking today. Would you mind assisting me in the kitchen?";
            case "ClinicEvent":
                return "You look like you’ve studied nursing. Please, come help us! We’re running short on staff!";
            case "BeachEvent":
                return "Excuse me, dear. Could you help me by the shore? The trash has started piling up.";
            case "GardenEvent":
                return "I could use a hand with the planting. Would you be willing to help me in the garden?";
            default:
                return "Hello there! Are you ready to take on this activity?";
        }
    }


    private int ComputeStartIndex(int count, int totalSlots)
    {
        if (totalSlots == 6)
        {
            switch (count)
            {
                case 1:
                case 2:
                case 3: return 2; 
                case 4:
                case 5: return 1; 
                case 6: return 0;
            }
        }
        return Mathf.Clamp((totalSlots - count) / 2, 0, totalSlots - count);
    }

    private void OnAnswerSelected(string chosen)
    {
        if (subjectQueue.Count == 0) return;

        if (!gameStarted || subjectQueue.Count == 0) return;

        if (chosen == currentTrashcanSubject)
        {
            foreach (var btn in choiceButtons)
            {
                btn.interactable = false;

            }
            GameSpeed.Instance.SetSlowmo();
            currentTrashcan = spawnedTrashcans[0];
            AudioManager.FireSFX(AudioManager.SFXSignal.TrashOpen);
            Trashcan tc = currentTrashcan.GetComponent<Trashcan>();
            playerScript.SetSlowAnimSpeed();
            if (tc != null) tc.Open(); 

            string solvedSubject = currentTrashcanSubject;
            subjectQueue.Dequeue();

            if (subjectQueue.Count > 0)
            {
                currentTrashcanSubject = subjectQueue.Peek();
            }

            StartCoroutine(TransitionManager.MoveUI(
                subjChoicesGroup, offscreenLeft, 0.5f, () =>
                {
                    problemChoicesGroup.anchoredPosition = problemHiddenPos;
                    StartCoroutine(TransitionManager.MoveUI(problemChoicesGroup, subjStartPos, 0.5f));
                }));

            questionManager.GetQuestionFor(solvedSubject);

            foreach (var btn in choiceButtons)
            {
                btn.interactable = false;
            }
        }
        else
        {
            Debug.Log("Wrong subject guess!");
            ScoreManager.instance.ResetStreak();
            HealthManager.Instance.DecreaseHP();
            AudioManager.FireSFX(AudioManager.SFXSignal.Wrong);


            if (spawnedTrashcans.Count > 0)
            {
                GameObject trashcanToRemove = spawnedTrashcans[0];
                spawnedTrashcans.RemoveAt(0);

                if (trashcanToRemove != null)
                {
                    StartCoroutine(questionManager.FadeAndDestroy(trashcanToRemove, 0.5f));
                }
            }

            if (subjectQueue.Count > 0)
            {
                subjectQueue.Dequeue();
                currentTrashcanSubject = subjectQueue.Count > 0 ? subjectQueue.Peek() : null;
                foreach (var btn in choiceButtons)
                {
                    btn.interactable = true;

                }
            }

            if (subjectQueue.Count == 0)
            {
                StartNewRound();
            }
        }

    }

    public void OnReturnFromProblem()
    {
        if (subjectQueue.Count > 0)
        {
            questionText.text = SubjectPrompt;
        }
        else
        {
            StartNewRound();
        }
    }

    public void OnTrashcanExited(GameObject trashcan)
    {
        if (!spawnedTrashcans.Contains(trashcan)) return;

        Debug.Log("Trashcan exited camera: " + trashcan.name);

        if (trashcan == currentTrashcan)
        {
            currentTrashcan = null;

            if (questionManager != null)
            {
                questionManager.CancelCurrentProblem();
            }
        }

        GameSpeed.Instance.SetNormal();
        spawnedTrashcans.Remove(trashcan);
        Destroy(trashcan);

        if (subjectQueue.Count > 0)
        {
            subjectQueue.Dequeue();
            currentTrashcanSubject = subjectQueue.Count > 0 ? subjectQueue.Peek() : null;
        }

        if (subjectQueue.Count == 0)
        {
            StartNewRound();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");

        gameStarted = false;
        rounds = 0;
        ScoreManager.instance.Restart();
        AudioManager.FireMusic(AudioManager.MusicSignal.StartMenu);

        foreach (var trashcan in spawnedTrashcans)
        {
            if (trashcan != null) Destroy(trashcan);
        }
        spawnedTrashcans.Clear();
        subjectQueue.Clear();
        currentTrashcan = null;
        currentTrashcanSubject = null;

        if (questionManager != null)
        {
            questionManager.CancelCurrentProblem();
        }

        if (timerManager != null)
        {
            timerManager.StopTimer();
        }

        foreach (var btn in choiceButtons)
        {
            btn.onClick.RemoveAllListeners();
        }

        uiManager.HideAllUI();

        FindAnyObjectByType<Menu>()?.DisplayMenu(true, "gameOverMenu");

    }

    public void StopSpawning()
    {
        gameStarted = false;
        uiManager.ToggleUI(uiManager.activityMenuFrame);
        uiManager.HideAllUI();
        timerManager.StopTimer(); 
        Debug.Log("Spawning has been stopped.");
    }

    public void ResumeSpawning()
    {
        rounds = 0;
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("isRunning", true);
        }
        uiManager.ToggleAllUI();
        uiManager.HideUI(uiManager.activityMenuFrame);
        gameStarted = true;
        timerManager.RestartTimer();
        Debug.Log("Spawning has resumed.");
        GameSpeed.Instance.SetNormal();
        StartNewRound();
    }


}