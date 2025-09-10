using UnityEngine;
using System.Collections.Generic;

public static class MathQuestionProvider
{
    public static QuestionData GetQuestion(string type)
    {
        if (type == "easy")
            return GenerateEasy();
        else if (type == "medium")
            return GenerateMedium();
        else if (type == "hard")
            return GenerateHard();
        else
            return GenerateEasy(); 
    }

    private static QuestionData GenerateEasy()
    {
        int a = Random.Range(1, 10);
        int b = Random.Range(1, 10);

        string question = "";
        int answer = 0;

        int op = Random.Range(0, 4);
        switch (op)
        {
            case 0: 
                question = $"{a} + {b}";
                answer = a + b;
                break;
            case 1: 
                if (a < b) (a, b) = (b, a);
                question = $"{a} - {b}";
                answer = a - b;
                break;
            case 2: 
                question = $"{a} × {b}";
                answer = a * b;
                break;
            case 3:
                answer = a;
                int dividend = a * b;
                question = $"{dividend} ÷ {b}";
                break;
        }

        return BuildQuestion(question, answer);
    }

    private static QuestionData GenerateMedium()
    {
        int a = Random.Range(1, 5);
        int b = Random.Range(1, 5);
        int c = Random.Range(1, 5);

        int pattern = Random.Range(0, 3);
        string question = "";
        int answer = 0;

        switch (pattern)
        {
            case 0:
                question = $"({a} + {b}) × {c}";
                answer = (a + b) * c;
                break;
            case 1:
                question = $"({a} × {b}) - {c}";
                answer = (a * b) - c;
                break;
            case 2:
                if ((a + b) % c == 0) 
                {
                    question = $"({a} + {b}) ÷ {c}";
                    answer = (a + b) / c;
                }
                else
                {
                    question = $"({a} × {b}) + {c}";
                    answer = (a * b) + c;
                }
                break;
        }

        return BuildQuestion(question, answer);
    }

    private static QuestionData GenerateHard()
    {
        int a = Random.Range(1, 5);
        int b = Random.Range(1, 5);
        int c = Random.Range(1, 5);
        int d = Random.Range(1, 5);

        int pattern = Random.Range(0, 3);
        string question = "";
        int answer = 0;

        switch (pattern)
        {
            case 0:
                question = $"({a} + {b}) × ({c} - {d})";
                answer = (a + b) * (c - d);
                break;
            case 1:
                if (c != 0 && (a * b) % c == 0)
                {
                    question = $"({a} × {b}) ÷ {c} + {d}";
                    answer = (a * b) / c + d;
                }
                else
                {
                    question = $"{a} + {b} × {c} - {d}";
                    answer = a + (b * c) - d;
                }
                break;
            case 2:
                question = $"{a} × ({b} + {c}) - {d}";
                answer = a * (b + c) - d;
                break;
        }

        return BuildQuestion(question, answer);
    }

    private static QuestionData BuildQuestion(string question, int correctAnswer)
    {
        HashSet<int> options = new HashSet<int> { correctAnswer };
        while (options.Count < 3)
        {
            options.Add(correctAnswer + Random.Range(-3, 4));
        }

        List<string> choiceList = new List<string>();
        foreach (int val in options)
            choiceList.Add(val.ToString());

        Shuffle(choiceList);

        int correctIndex = choiceList.IndexOf(correctAnswer.ToString());

        return new QuestionData
        {
            question = question,
            choices = choiceList.ToArray(),
            correctAnswerIndex = correctIndex
        };
    }

    private static void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
}
