using System.Collections;
using UnityEngine;

public class Objective : MonoBehaviour
{
    [Header("Objective Settings")]
    public int remainingObjectives = 6;
    public int currentWave = 1;

    [Header("References")]
    public DialogueScript dialogueScript;
    public SpriteRenderer spriteRenderer;  
    public Sprite defaultSprite;            

    [Header("Item Sprites")]
    public Sprite item1Sprite;
    public Sprite item2Sprite;
    public Sprite item3Sprite;
    public Sprite item4Sprite;
    public Sprite item5Sprite;
    public Sprite item6Sprite;

    [Header("Wave Sprites")]
    public Sprite wave1Sprite;
    public Sprite wave2Sprite;
    public Sprite wave3Sprite;

    private Coroutine dialogueCoroutine;
    private int dialogueSession = 0;

    private void Start()
    {
        dialogueScript.SetDialogue(dialogueScript.dialogueData, 0);
        spriteRenderer.sprite = defaultSprite;

        SceneTransition.Instance.StartCoroutine(SceneTransition.Instance.FadeOut());
        AudioManager.FireMusic(AudioManager.MusicSignal.Activity);
    }

    private bool IsValidItem(int itemID)
    {
        switch (currentWave)
        {
            case 1: return itemID == 1 || itemID == 2;
            case 2: return itemID == 3 || itemID == 4;
            case 3: return itemID == 5 || itemID == 6;
            default: return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PickAndDrop player = other.GetComponent<PickAndDrop>();
        if (player != null && player.HeldItem != null)
        {
            PickupItem item = player.HeldItem.GetComponent<PickupItem>();
            if (item != null)
            {
                if (IsValidItem(item.itemID))
                {
                    AudioManager.FireSFX(AudioManager.SFXSignal.ActivityCorrect);
                    Debug.Log("Correct item " + item.itemID + " delivered at Wave " + currentWave);

                    Destroy(player.HeldItem);
                    remainingObjectives--;
                    Debug.Log("Remaining objectives: " + remainingObjectives);

                    // Show item sprite
                    ShowItemSprite(item.itemID);

                    if (remainingObjectives <= 0)
                    {
                        StartCoroutine(ShowWaveSpriteThenReset(wave3Sprite));
                        PlayDialogue(dialogueScript.dialogueData, 5, 3f, 6);
                    }
                    else if (remainingObjectives == 2)
                    {
                        currentWave = 3;
                        StartCoroutine(ShowWaveSpriteThenReset(wave2Sprite));
                        PlayDialogue(dialogueScript.dialogueData, 3, 3f, 4);
                    }
                    else if (remainingObjectives == 4)
                    {
                        currentWave = 2;
                        StartCoroutine(ShowWaveSpriteThenReset(wave1Sprite));
                        PlayDialogue(dialogueScript.dialogueData, 1, 3f, 2);
                        Debug.Log(remainingObjectives);
                    }
                }
                else
                {
                    AudioManager.FireSFX(AudioManager.SFXSignal.ActivityWrong);
                    Debug.Log("Item " + item.itemID + " not valid for Wave " + currentWave);

                    item.ReturnToOriginalPosition();
                    player.ClearHeldItem();
                }
            }
        }
    }

    private void ShowItemSprite(int itemID)
    {
        switch (itemID)
        {
            case 1: spriteRenderer.sprite = item1Sprite; break;
            case 2: spriteRenderer.sprite = item2Sprite; break;
            case 3: spriteRenderer.sprite = item3Sprite; break;
            case 4: spriteRenderer.sprite = item4Sprite; break;
            case 5: spriteRenderer.sprite = item5Sprite; break;
            case 6: spriteRenderer.sprite = item6Sprite; break;
            default: spriteRenderer.sprite = defaultSprite; break;
        }
    }

    private IEnumerator ShowWaveSpriteThenReset(Sprite waveSprite)
    {
        spriteRenderer.sprite = waveSprite;
        yield return new WaitForSeconds(3f);
        spriteRenderer.sprite = defaultSprite;
    }

    private void PlayDialogue(ActivityDialogueData data, int firstIndex, float delay, int nextIndex)
    {
        dialogueSession++;

        if (dialogueCoroutine != null)
            StopCoroutine(dialogueCoroutine);

        dialogueCoroutine = StartCoroutine(ShowDialogueWithDelay(data, firstIndex, delay, nextIndex, dialogueSession));
    }

    IEnumerator ShowDialogueWithDelay(ActivityDialogueData data, int firstIndex, float delay, int nextIndex, int sessionId)
    {
        dialogueScript.SetDialogue(data, firstIndex);

        if (delay > 0f)
        {
            yield return new WaitForSeconds(delay);

            if (sessionId == dialogueSession)
            {
                dialogueScript.SetDialogue(data, nextIndex);
            }
        }
    }
}
