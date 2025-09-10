using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public System.Action OnInstructionsFinished;
    public System.Action OnDialogueFinished;

    [Header("Chat Containers")]
    [SerializeField] private GameObject instructionsContainer;
    [SerializeField] private TMP_Text instructionsText;
    [SerializeField] private Button instructionsNextButton;

    [SerializeField] private GameObject dialogueContainer;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button dialogueNextButton;

    [SerializeField] private GameObject chatContainer;
    [SerializeField] private TMP_Text chatText;

    [Header("Dialogue Data")]
    [SerializeField] private DialogueData instructionsData;
    [SerializeField] private DialogueData dialogueData;


    private int instructionsIndex = 0;
    private int dialogueIndex = 0;
    public int CurrentInstructionIndex => instructionsIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }

        if(instructionsContainer != null)
        {
            instructionsContainer.SetActive(false);
        }

        dialogueContainer.SetActive(false);
        chatContainer.SetActive(false);
    }

    private void Start()
    {
        instructionsNextButton.onClick.AddListener(NextInstruction);
        dialogueNextButton.onClick.AddListener(NextDialogue);

        StartInstructions();
    }

    public void StartInstructions()
    {
        if (instructionsData == null || instructionsData.lines.Length == 0)
        {
            instructionsContainer.SetActive(false);
            return;
        }

        instructionsIndex = 0;
        if (instructionsContainer != null)
        {
            instructionsContainer.SetActive(true);
        }

        instructionsText.text = instructionsData.lines[instructionsIndex];
    }

    private void NextInstruction()
    {
        instructionsIndex++;
        if (instructionsIndex < instructionsData.lines.Length)
        {
            instructionsText.text = instructionsData.lines[instructionsIndex];
        }
        else
        {
            OnInstructionsFinished?.Invoke();
            instructionsContainer.SetActive(false);
        }
    }

    public void StartDialogue()
    {
        if (dialogueData == null || dialogueData.lines.Length == 0)
        {
            dialogueContainer.SetActive(false);
            return;
        }

        dialogueIndex = 0;
        dialogueContainer.SetActive(true);
        dialogueText.text = dialogueData.lines[dialogueIndex];
    }

    private void NextDialogue()
    {
        dialogueIndex++;
        if (dialogueIndex < dialogueData.lines.Length)
        {
            dialogueText.text = dialogueData.lines[dialogueIndex];
        }
        else
        {
            OnDialogueFinished?.Invoke();
            dialogueContainer.SetActive(false);
        }
    }

    public void Chat(string message, float duration = 2.5f)
    {
        StopAllCoroutines(); 
        StartCoroutine(ChatRoutine(message, duration));
    }

    private IEnumerator ChatRoutine(string message, float duration)
    {
        chatContainer.SetActive(true);
        chatText.text = message;
        yield return new WaitForSeconds(duration);
        chatContainer.SetActive(false);
    }
}
