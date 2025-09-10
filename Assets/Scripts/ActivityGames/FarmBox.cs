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
    public GameObject highlight;         
    public SpriteRenderer iconRenderer;   
    public Sprite fertilizerIcon;
    public Sprite seedIcon;
    public Sprite waterIcon;

    private void Start()
    {
        if (plantSprite != null)
        {
            plantSprite.sprite = null;
        }

        currentState = FarmState.NeedsFertilizer;

        if (highlight != null)
        {
            highlight.SetActive(false); 

        }
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
            Debug.Log("fertilizer added.");
        }
        else if (currentState == FarmState.NeedsPlant && player.heldItem == FarmPlayerItemHandler.ItemType.SeedBox)
        {
            currentState = FarmState.NeedsWater;
            player.ClearItem();
            if (plantSprite != null)
            {
                plantSprite.sprite = seedSprite;
                plantSprite.color = Color.white;
            }
            Debug.Log("seeds planted.");
        }
        else if (currentState == FarmState.NeedsWater && player.heldItem == FarmPlayerItemHandler.ItemType.WaterCan)
        {
            currentState = FarmState.Done;
            ObjectiveManager.Instance.AddObjective();
            player.ClearItem();
            if (blockSprite != null)
            {
                plantSprite.color = new Color32(195, 195, 195, 255);
            }

            if (highlight != null)
            {
                highlight.SetActive(false);
            }

            Debug.Log("plant watered! Farm box is complete.");
        }
        else
        {
            Debug.Log("wrong item or already finished.");
        }

        UpdateHighlightIcon();
    }

    public void ShowHighlight(bool show)
    {
        if (highlight != null)
        {
            highlight.SetActive(show && currentState != FarmState.Done);
            if (show) 
            { 
                UpdateHighlightIcon(); 
            }
        }
    }

    private void UpdateHighlightIcon()
    {
        if (iconRenderer == null) return;

        if (currentState == FarmState.NeedsFertilizer)
        {
            iconRenderer.sprite = fertilizerIcon;
        }
        else if (currentState == FarmState.NeedsPlant)
        {
            iconRenderer.sprite = seedIcon;
        }
        else if (currentState == FarmState.NeedsWater)
        {
            iconRenderer.sprite = waterIcon;
        }
        else
        {
            iconRenderer.sprite = null;
        }
    }
}
