using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("Pickup Identity")]
    public int itemID;

    [Header("Rendering")]
    public SpriteRenderer spriteRenderer;

    [HideInInspector] public int defaultOrder;
    [HideInInspector] public Vector3 originalPosition;
    [HideInInspector] public Vector3 originalScale; // store original scale

    void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer != null)
        {
            defaultOrder = spriteRenderer.sortingOrder;
        }

        originalPosition = transform.position;
        originalScale = transform.localScale; // store original scale
    }

    public void SetPickedUp(bool pickedUp)
    {
        if (spriteRenderer == null) return;

        if (pickedUp)
        {
            spriteRenderer.sortingLayerName = "Front";
            spriteRenderer.sortingOrder = 6;
        }
        else
        {
            spriteRenderer.sortingLayerName = "Decorations";
            spriteRenderer.sortingOrder = defaultOrder;
        }
    }

    public void ReturnToOriginalPosition()
    {
        transform.SetParent(null);
        transform.position = originalPosition;
        transform.localScale = originalScale; // restore original scale
    }
}
