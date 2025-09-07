using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueUIManager : MonoBehaviour
{
    public static DialogueUIManager Instance;

    [Header("UI References")]
    public GameObject dialogueContainer;  
    public TMP_Text dialogueText;          
    public Button acceptButton;
    public Button declineButton;
    public PlayerMovement playerMovement;


    private int pendingSceneIndex = -1;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        dialogueContainer.SetActive(false);
        acceptButton.gameObject.SetActive(false);

        acceptButton.onClick.AddListener(OnAcceptQuest);
    }

    public void ShowDialogue(NPCDialogueSO dialogueData, int minigameScene)
    {
        if (dialogueData == null || dialogueData.dialogueLines.Length == 0) return;

        dialogueText.text = dialogueData.dialogueLines[0];

        playerMovement.speed = 0f;

        pendingSceneIndex = minigameScene;

        dialogueContainer.SetActive(true);
        acceptButton.gameObject.SetActive(true);
    }

    private void OnAcceptQuest()
    {
        if (pendingSceneIndex > 0)
        {
            Debug.Log("Loading Minigame Scene: " + pendingSceneIndex);
            SceneManager.LoadScene(pendingSceneIndex);
        }

        CloseDialogue();
    }

    public void CloseDialogue()
    {
        playerMovement.speed = 5f;
        dialogueContainer.SetActive(false);
        acceptButton.gameObject.SetActive(false);
    }
}
