using UnityEngine;

public class TiledBackground : MonoBehaviour
{
    [Header("Growth Settings")]
    public float growthAmount = 1f;   // How much to increase per step
    public float interval = 5f;       // Time in seconds per step

    private SpriteRenderer spriteRenderer;
    private bool isGrowing = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer.drawMode != SpriteDrawMode.Tiled)
        {
            Debug.LogWarning("SpriteRenderer is not set to Tiled draw mode!");
        }

        // Start repeating the growth
        InvokeRepeating(nameof(IncreaseTile), interval, interval);
    }

    void IncreaseTile()
    {
        if (!isGrowing || spriteRenderer == null) return;

        Vector2 size = spriteRenderer.size;
        size.x += growthAmount; // Only grow to the right
        spriteRenderer.size = size;
    }

    // Call this to stop growth
    public void StopGrowth()
    {
        isGrowing = false;
    }

    // Optional: Call this to resume growth
    public void ResumeGrowth()
    {
        isGrowing = true;
    }
}