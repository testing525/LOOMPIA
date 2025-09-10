using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TutorialQuestionManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button[] problemButtons;

    private int correctIndex;
    private System.Action onCorrectAnswer;

    public void GetQuestionFor(string subject, System.Action onCorrect)
    {
        onCorrectAnswer = onCorrect;

        QuestionData data = GenerateTutorialQuestion(subject);

        if (data != null)
        {
            DisplayQuestion(data);
            foreach (var btn in problemButtons)
            {
                btn.gameObject.SetActive(true);
            }
        }
    }

    private QuestionData GenerateTutorialQuestion(string subject)
    {
        switch (subject)
        {
            case "Math":
                return new QuestionData
                {
                    question = "What is 2 + 2?",
                    choices = new string[] { "3", "4", "5" },
                    correctAnswerIndex = 1
                };
            case "Literature":
                return new QuestionData
                {
                    question = "Who wrote 'Romeo and Juliet'?",
                    choices = new string[] { "Shakespeare", "Homer", "Tolstoy" },
                    correctAnswerIndex = 0
                };
            case "Science":
                return new QuestionData
                {
                    question = "What planet is known as the Red Planet?",
                    choices = new string[] { "Venus", "Mars", "Jupiter" },
                    correctAnswerIndex = 1
                };
        }
        return null;
    }

    private void DisplayQuestion(QuestionData data)
    {
        questionText.text = data.question;
        correctIndex = data.correctAnswerIndex;

        for (int i = 0; i < problemButtons.Length; i++)
        {
            int index = i;
            problemButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = data.choices[i];
            problemButtons[i].onClick.RemoveAllListeners();
            problemButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }
    }

    private void OnAnswerSelected(int index)
    {
        if (index == correctIndex)
        {
            Debug.Log("Tutorial: Correct problem answer!");
            onCorrectAnswer?.Invoke();

            foreach (var btn in problemButtons)
                btn.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Tutorial: Wrong answer! Try again.");
        }
    }
}
