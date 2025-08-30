using UnityEngine;

public class Trashcan : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite closedSprite;
    public Sprite openSprite;

    private void Reset()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Open()
    {
        if (spriteRenderer != null && openSprite != null)
        {
            spriteRenderer.sprite = openSprite;
        }
    }

    public void Close()
    {
        if (spriteRenderer != null && closedSprite != null)
        {
            spriteRenderer.sprite = closedSprite;

        }
    }
}
