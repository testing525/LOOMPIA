using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public enum Difficulty { Easy, Medium, Hard }

    public static Difficulty CurrentDifficulty = Difficulty.Easy;
    private static int correctStreak = 0;

    // Thresholds
    private const int easyToMedThreshold = 5;
    private const int medToHardThreshold = 10;

    public static void CorrectAnswer()
    {
        correctStreak++;

        if (CurrentDifficulty == Difficulty.Easy && correctStreak >= easyToMedThreshold)
        {
            CurrentDifficulty = Difficulty.Medium;
            correctStreak = 0;
        }
        else if (CurrentDifficulty == Difficulty.Medium && correctStreak >= medToHardThreshold)
        {
            CurrentDifficulty = Difficulty.Hard;
            correctStreak = 0;
        }
    }

    public static void WrongAnswerOrTimeout()
    {
        if (CurrentDifficulty == Difficulty.Hard)
        {
            CurrentDifficulty = Difficulty.Medium;
            correctStreak = 0;
        }
        else if (CurrentDifficulty == Difficulty.Medium)
        {
            CurrentDifficulty = Difficulty.Easy;
            correctStreak = 0;
        }
    }

    // Helper for providers
    public static string GetDifficultyType()
    {
        return CurrentDifficulty.ToString().ToLower();
    }
}
