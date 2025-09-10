using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public GameObject warningSign;
    public NPCDialogueSO dialogueData;

    [Header("Minigame Settings")]
    public int MinigameScene = -1; 

    private bool playerInRange = false;

    private void Start()
    {
        if (warningSign != null)
            warningSign.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogueUIManager.Instance.ShowDialogue(dialogueData, MinigameScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (warningSign != null)
                warningSign.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (warningSign != null)
                warningSign.SetActive(false);
        }
    }
}
