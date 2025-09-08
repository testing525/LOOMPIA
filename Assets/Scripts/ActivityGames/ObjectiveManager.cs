using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;  // Singleton

    [Header("Objective Tracking")]
    public int objectiveNeeded = 3; 
    public int objective = 0;      

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddObjective()
    {
        objective++;
        Debug.Log("Objective Progress: " + objective + "/" + objectiveNeeded);

        if (objective >= objectiveNeeded)
        {
            DialogueManager.Instance.StartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // make sure player has "Player" tag
        {
            if (objective >= objectiveNeeded)
            {
                Debug.Log("All objectives met! Loading Scene 0...");
                SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log("Objectives not yet completed: " + objective + "/" + objectiveNeeded);
            }
        }
    }
}
