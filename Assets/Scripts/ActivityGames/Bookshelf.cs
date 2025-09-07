using UnityEngine;
using System.Collections;

public class Bookshelf : MonoBehaviour
{
    public enum ShelfType { History, Language, Technology }
    public ShelfType shelfIdentity;

    [Header("Sprites")]
    public Sprite fullSprite;
    public Sprite emptySprite;

    [Header("Fill Settings")]
    public float fillDuration = 4f;

    private SpriteRenderer spriteRenderer;
    private bool isEmpty = false;
    private Coroutine fillCoroutine = null;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = isEmpty ? emptySprite : fullSprite;
    }

    public void SetEmpty(bool empty)
    {
        isEmpty = empty;
        if (spriteRenderer != null)
            spriteRenderer.sprite = isEmpty ? emptySprite : fullSprite;

        if (isEmpty && GetComponent<Collider2D>() == null)
        {
            BoxCollider2D trigger = gameObject.AddComponent<BoxCollider2D>();
            trigger.isTrigger = true;
            trigger.size = new Vector2(1f, 1.5f);
            trigger.offset = new Vector2(0f, -0.8f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEmpty) return;

        LibraryPlayerItemHandler player = other.GetComponent<LibraryPlayerItemHandler>();
        if (player != null && player.IsHolding())
        {
            Bookshelf.ShelfType? heldType = player.GetHeldType();
            if (heldType.HasValue && heldType.Value == shelfIdentity)
            {
                fillCoroutine = StartCoroutine(FillShelfCoroutine(player));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        LibraryPlayerItemHandler player = other.GetComponent<LibraryPlayerItemHandler>();
        if (player != null && fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
            fillCoroutine = null;
            FillBarManager.Instance.ResetFill();
        }
    }

    private IEnumerator FillShelfCoroutine(LibraryPlayerItemHandler player)
    {
        float elapsed = 0f;

        while (elapsed < fillDuration)
        {
            if (!player.IsHolding() || player.GetHeldType() != shelfIdentity)
            {
                FillBarManager.Instance.ResetFill();
                yield break;
            }

            elapsed += Time.deltaTime;

            FillBarManager.Instance.Fill(Time.deltaTime, fillDuration);

            yield return null;
        }

        FillShelf(player);
        FillBarManager.Instance.ResetFill();
        fillCoroutine = null;
    }

    private void FillShelf(LibraryPlayerItemHandler player)
    {
        isEmpty = false;
        spriteRenderer.sprite = fullSprite;
        player.ClearHeldBox();
    }

    public bool IsEmpty() => isEmpty;
}
