using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    public static GameSpeed Instance;

    [Header("Speed Multipliers")]
    [Range(0.1f, 2f)] public float normalMultiplier = 1f;
    [Range(0.1f, 2f)] public float slowmoMultiplier = 0.2f;

    private float currentMultiplier;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        currentMultiplier = normalMultiplier;
    }

    public float GetMultiplier()
    {
        return currentMultiplier;
    }

    public void SetNormal()
    {
        currentMultiplier = normalMultiplier;
    }

    public void SetSlowmo()
    {
        currentMultiplier = slowmoMultiplier;
    }

    public void SetStop()
    {
        currentMultiplier = 0f;
    }
}
