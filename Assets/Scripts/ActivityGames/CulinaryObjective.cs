using System.Collections;
using UnityEngine;

public class CulinaryObjective : MonoBehaviour
{
    [Header("Objective Settings")]
    public int remainingObjectives = 6;
    public int currentWave = 1;

    [Header("References")]
    public SpriteRenderer spriteRenderer;

    [Header("Sprites")]
    public Sprite ogSprite;      
    public Sprite[] waveSprites;   

    private void Start()
    {
        if (spriteRenderer != null && ogSprite != null)
            spriteRenderer.sprite = ogSprite;


        AudioManager.FireMusic(AudioManager.MusicSignal.Activity);

        SceneTransition.Instance.StartCoroutine(SceneTransition.Instance.FadeOut());

    }

    private bool IsValidItem(int itemID)
    {
        switch (currentWave)
        {
            case 1: return itemID == 1;
            case 2: return itemID == 2;
            case 3: return itemID == 3;
            case 4: return itemID == 4;
            default: return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PickAndDrop player = other.GetComponent<PickAndDrop>();
        if (player != null && player.HeldItem != null)
        {
            PickupItem item = player.HeldItem.GetComponent<PickupItem>();
            if (item != null && IsValidItem(item.itemID))
            {
                AudioManager.FireSFX(AudioManager.SFXSignal.ActivityCorrect);

                Debug.Log("Correct item " + item.itemID + " delivered at Wave " + currentWave);

                Destroy(player.HeldItem); 

                remainingObjectives--;
                Debug.Log("Remaining objectives: " + remainingObjectives);

                if (spriteRenderer != null && currentWave - 1 < waveSprites.Length)
                {
                    spriteRenderer.sprite = waveSprites[currentWave - 1];
                }

                if (currentWave == 1)
                {
                    DialogueManager.Instance.Chat("Great! add the dry pasta next.");
                }
                else if (currentWave == 2)
                {
                    DialogueManager.Instance.Chat("Good! Finally add the tomato sauce to complete the pasta!");
                    
                }

                if (currentWave == 3)
                {
                    remainingObjectives = 0;
                    DialogueManager.Instance.Chat("Now that is a pasta! Come back here anytime if you want to help!");

                }

                Debug.Log("remaining objectives: " + remainingObjectives);
                currentWave++;
            }
            else if (item != null)
            {
                Debug.Log("Item " + item.itemID + " not valid for Wave " + currentWave);
                AudioManager.FireSFX(AudioManager.SFXSignal.ActivityWrong);

                DialogueManager.Instance.Chat("Wrong ingredient!");

                item.ReturnToOriginalPosition();

                player.ClearHeldItem();
            }

        }
    }

}
