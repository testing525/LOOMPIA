using UnityEngine;
using System.Collections;

public class ClinicSampleContainer : MonoBehaviour
{
    private Coroutine storeCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ClinicPlayerItemHandler playerHandler = other.GetComponent<ClinicPlayerItemHandler>();
        if (playerHandler != null && playerHandler.IsHoldingSample())
        {
            storeCoroutine = StartCoroutine(FillAndStore(playerHandler));
        }
        else
        {
            DialogueManager.Instance.Chat("You aren't holding any sample, come back here once you have it");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (storeCoroutine != null)
        {
            StopCoroutine(storeCoroutine);
            storeCoroutine = null;
            FillBarManager.Instance.ResetFill();
        }
    }

    private IEnumerator FillAndStore(ClinicPlayerItemHandler playerHandler)
    {
        float duration = 4f;
        float timer = 0f;


        while (timer < duration)
        {
            FillBarManager.Instance.Fill(Time.deltaTime, duration);
            timer += Time.deltaTime;
            yield return null;

            if (storeCoroutine == null) yield break;
        }

        if (playerHandler.placeholder != null)
        {
            SpriteRenderer placeholderRenderer = playerHandler.placeholder.GetComponent<SpriteRenderer>();
            if (placeholderRenderer != null)
            {
                placeholderRenderer.sprite = null;
            }
        }
        FillBarManager.Instance.ResetFill();

        playerHandler.ForceClearSample();
        ObjectiveManager.Instance.AddObjective();

    }
}
