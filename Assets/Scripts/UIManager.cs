using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject problemChoices;
    public GameObject subjectChoices;
    public GameObject questionText;
    public GameObject scoreText;
    public GameObject streakText;
    public GameObject livesFrame;
    public GameObject timerFrame;
    public GameObject activityMenuFrame;
    public void ShowUI(GameObject uiElement)
    {
        if (uiElement != null)
        {
            uiElement.SetActive(true);
        }
    }

    public void HideUI(GameObject uiElement)
    {
        if (uiElement != null)
        {
            uiElement.SetActive(false);
        }
    }

    public void ToggleUI(GameObject uiElement)
    {
        if (uiElement != null)
        {
            uiElement.SetActive(!uiElement.activeSelf);
        }
    }

    public void HideAllUI()
    {
        HideUI(problemChoices);
        HideUI(subjectChoices);
        HideUI(questionText);
        HideUI(streakText);
        HideUI(livesFrame);
        HideUI(timerFrame);
        HideUI(scoreText);
    }

    public void ToggleAllUI()
    {
        ToggleUI(problemChoices);
        ToggleUI(subjectChoices);
        ToggleUI(questionText);
        ToggleUI(streakText);
        ToggleUI(livesFrame);
        ToggleUI(timerFrame);
        ToggleUI(scoreText);
    }
}
