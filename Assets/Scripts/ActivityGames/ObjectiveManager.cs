using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;  // Singleton

    [Header("Objective Tracking")]
    public int objectiveNeeded = 3; 
    public int objective = 0;
    public Image exitImage;

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
    }

    private void Start()
    {
        exitImage.gameObject.SetActive(false);
    }

    public void AddObjective()
    {
        objective++;
        Debug.Log("Objective Progress: " + objective + "/" + objectiveNeeded);

        if (objective >= objectiveNeeded)
        {
            DialogueManager.Instance.StartDialogue();
            exitImage.gameObject.SetActive(true);
            HealthManager.Instance.IncreaseHP();

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (objective >= objectiveNeeded)
            {
                Debug.Log("All objectives met! Loading Scene 0...");
                SceneManager.LoadScene(3);
            }
            else
            {
                Debug.Log("Objectives not yet completed: " + objective + "/" + objectiveNeeded);
            }
        }
    }
}
