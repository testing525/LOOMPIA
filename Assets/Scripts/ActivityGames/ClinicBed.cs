using UnityEngine;
using System.Collections;

public class ClinicBed : MonoBehaviour
{
    public enum ToolType { Syringe, Saliva, CottonSwab }

    [Header("References")]
    public GameObject highlight;
    public Sprite syringeIcon;
    public Sprite salivaIcon;
    public Sprite cottonSwabIcon;

    [Header("Sample Sprites")]
    public Sprite bloodSampleSprite;
    public Sprite salivaSampleSprite;
    public Sprite swabSampleSprite;

    private ToolType requiredTool;
    private SpriteRenderer iconRenderer;
    private bool completed = false;
    private Coroutine interactionCoroutine;

    private void Start()
    {
        requiredTool = (ToolType)Random.Range(0, System.Enum.GetValues(typeof(ToolType)).Length);

        if (highlight != null)
        {
            highlight.SetActive(false);

            iconRenderer = highlight.transform.Find("Icon").GetComponent<SpriteRenderer>();
            if (iconRenderer != null)
            {
                iconRenderer.sprite = GetToolSprite(requiredTool);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !completed)
        {
            highlight.SetActive(true);

            ClinicPlayerItemHandler playerHandler = other.GetComponent<ClinicPlayerItemHandler>();

            if (playerHandler != null && playerHandler.GetHeldTool() == (ClinicEnum.ToolType)requiredTool)
            {
                interactionCoroutine = StartCoroutine(FillAndConfirm(playerHandler));
            }
            else
            {
                DialogueManager.Instance.Chat("That is not the right tool for this kind of sample.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            highlight.SetActive(false);
            FillBarManager.Instance.ResetFill();

            if (interactionCoroutine != null)
            {
                StopCoroutine(interactionCoroutine);
                interactionCoroutine = null;
            }
        }
    }

    private IEnumerator FillAndConfirm(ClinicPlayerItemHandler playerHandler)
    {
        float duration = 4f;
        float timer = 0f;

        while (timer < duration)
        {
            FillBarManager.Instance.Fill(Time.deltaTime, duration);
            timer += Time.deltaTime;
            yield return null;

            if (!highlight.activeSelf) yield break;
        }

        if (!completed)
        {
            playerHandler.ChangeToSample((ClinicEnum.ToolType)requiredTool);
            completed = true;
            FillBarManager.Instance.ResetFill();
 
            Debug.Log("Sample collected: " + requiredTool);
        }
    }

    private Sprite GetToolSprite(ToolType tool)
    {
        switch (tool)
        {
            case ToolType.Syringe: return syringeIcon;
            case ToolType.Saliva: return salivaIcon;
            case ToolType.CottonSwab: return cottonSwabIcon;
            default: return null;
        }
    }

    public ToolType GetRequiredTool()
    {
        return requiredTool;
    }
}
