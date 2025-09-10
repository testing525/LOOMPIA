using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoader : MonoBehaviour
{
    private void Start()
    {
        bool tutorialDone = PlayerPrefs.GetInt("TutorialDone", 0) == 1;

        if (!tutorialDone)
        {
            Debug.Log("Tutorial not done yet. Loading Scene 1...");
            SceneManager.LoadScene(1); 
        }
        else
        {
            Debug.Log("Tutorial already done. Loading Scene 3...");
            SceneManager.LoadScene(3); 
        }
    }
}
