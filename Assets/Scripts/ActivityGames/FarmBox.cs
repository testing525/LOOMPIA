using UnityEngine;

public class FarmBox : MonoBehaviour
{
    public enum FarmState { Empty, NeedsFertilizer, NeedsPlant, NeedsWater, Done }
    public FarmState currentState = FarmState.Empty;

    [Header("Plant Sprites")]
    public SpriteRenderer plantSprite;
    public Sprite fertilizedSprite;
    public Sprite seedSprite;
    public SpriteRenderer blockSprite;

    [Header("Highlight UI")]
    public GameObject highlight;          // highlight GameObject
    public SpriteRenderer iconRenderer;   // child SpriteRenderer for the icon
    public Sprite fertilizerIcon;
    public Sprite seedIcon;
    public Sprite waterIcon;

    private void Start()
    {
        if (plantSprite != null)
            plantSprite.sprite = null;

        currentState = FarmState.NeedsFertilizer;

        if (highlight != null)
            highlight.SetActive(false); // hidden until detected
    }

    public void Interact(FarmPlayerItemHandler player)
    {
        if (currentState == FarmState.NeedsFertilizer &&
            player.heldItem == FarmPlayerItemHandler.ItemType.Fertilizer)
        {
            currentState = FarmState.NeedsPlant;
            player.ClearItem();
            if (plantSprite != null)
            {
                plantSprite.sprite = fertilizedSprite;
                plantSprite.color = Color.white;
            }
            Debug.Log("🌱 Fertilizer added.");
        }
        else if (currentState == FarmState.NeedsPlant &&
                 player.heldItem == FarmPlayerItemHandler.ItemType.SeedBox)
        {
            currentState = FarmState.NeedsWater;
            player.ClearItem();
            if (plantSprite != null)
            {
                plantSprite.sprite = seedSprite;
                plantSprite.color = Color.white;
            }
            Debug.Log("🌱 Seeds planted.");
        }
        else if (currentState == FarmState.NeedsWater &&
                 player.heldItem == FarmPlayerItemHandler.ItemType.WaterCan)
        {
            currentState = FarmState.Done;
            player.ClearItem();
            if (blockSprite != null)
            {
                // Darken the farmbox's sprite to show it's watered
                plantSprite.color = new Color32(195, 195, 195, 255);
            }

            if (highlight != null)
                highlight.SetActive(false); // hide highlight since it's finished

            Debug.Log("💧 Plant watered! Farm box is complete.");
        }
        else
        {
            Debug.Log("❌ Wrong item or already finished.");
        }

        UpdateHighlightIcon();
    }

    // Called by player detection
    public void ShowHighlight(bool show)
    {
        if (highlight != null)
        {
            // If Done, keep highlight disabled
            highlight.SetActive(show && currentState != FarmState.Done);
            if (show) UpdateHighlightIcon();
        }
    }

    private void UpdateHighlightIcon()
    {
        if (iconRenderer == null) return;

        if (currentState == FarmState.NeedsFertilizer)
            iconRenderer.sprite = fertilizerIcon;
        else if (currentState == FarmState.NeedsPlant)
            iconRenderer.sprite = seedIcon;
        else if (currentState == FarmState.NeedsWater)
            iconRenderer.sprite = waterIcon;
        else
            iconRenderer.sprite = null; // hide if Done
    }
}
