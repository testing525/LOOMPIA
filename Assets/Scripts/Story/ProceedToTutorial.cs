using UnityEngine;
using UnityEngine.SceneManagement;

public class ProceedToTutorial : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialogueManager.Instance.OnInstructionsFinished += proceed;

    }

    void proceed()
    {
        SceneManager.LoadScene(2);
    }
}
