using UnityEngine;

public class SetTutorial : MonoBehaviour
{
    static SetTutorial instance;
    public ObjectiveManager manager;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        if (manager != null)
        {
            if (manager.objective >= manager.objectiveNeeded)
            {
                DoneTutorial();
            }
        }
    }
    public void DoneTutorial()
    {
        PlayerPrefs.SetInt("TutorialDone", 1);
        PlayerPrefs.Save();
    }

    public void ResetTutorial()
    {
        PlayerPrefs.DeleteKey("TutorialDone");
    }

}
