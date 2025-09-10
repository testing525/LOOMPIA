using UnityEngine;

public class ClinicPlayerItemHandler : MonoBehaviour
{
    [Header("References")]
    public GameObject placeholder;
    public KeyCode interactKey = KeyCode.E;

    [Header("Held Sprites")]
    public Sprite syringeSprite;
    public Sprite salivaSprite;
    public Sprite cottonSwabSprite;

    [Header("Sample Sprites")]
    public Sprite bloodSampleSprite;
    public Sprite salivaSampleSprite;
    public Sprite swabSampleSprite;

    private ClinicToolStorage nearbyStorage;
    private SpriteRenderer placeholderRenderer;

    private ClinicEnum.ToolType? heldTool = null;
    private bool holdingSample = false;

    private void Start()
    {
        if (placeholder != null)
        {
            placeholderRenderer = placeholder.GetComponent<SpriteRenderer>();
        }

        if (placeholderRenderer != null)
        {
            placeholderRenderer.sprite = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (holdingSample) return;

        if (heldTool == null && nearbyStorage != null)
        {
            PickUpTool(nearbyStorage);
        }
        else if (heldTool != null)
        {
            DropTool();
        }
    }

    private void PickUpTool(ClinicToolStorage storage)
    {
        heldTool = storage.GetToolType();

        if (placeholderRenderer != null)
        {
            switch (heldTool)
            {
                case ClinicEnum.ToolType.Syringe:
                    placeholderRenderer.sprite = syringeSprite;
                    break;
                case ClinicEnum.ToolType.Saliva:
                    placeholderRenderer.sprite = salivaSprite;
                    break;
                case ClinicEnum.ToolType.CottonSwab:
                    placeholderRenderer.sprite = cottonSwabSprite;
                    break;
            }
        }

        Debug.Log($"Picked up tool: {heldTool}");
    }

    private void DropTool()
    {
        heldTool = null;

        if (placeholderRenderer != null)
            placeholderRenderer.sprite = null;

        Debug.Log("Dropped tool.");
    }

    public void ChangeToSample(ClinicEnum.ToolType tool)
    {
        if (placeholderRenderer == null) return;

        switch (tool)
        {
            case ClinicEnum.ToolType.Syringe:
                placeholderRenderer.sprite = bloodSampleSprite;
                break;
            case ClinicEnum.ToolType.Saliva:
                placeholderRenderer.sprite = salivaSampleSprite;
                break;
            case ClinicEnum.ToolType.CottonSwab:
                placeholderRenderer.sprite = swabSampleSprite;
                break;
        }

        heldTool = null;
        holdingSample = true;

        Debug.Log($"Converted to sample: {tool}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ClinicToolStorage storage = other.GetComponent<ClinicToolStorage>();
        if (storage != null)
        {
            if (nearbyStorage != null && nearbyStorage != storage)
            {
                nearbyStorage.highlight.SetActive(false);
            }

            nearbyStorage = storage;
            nearbyStorage.highlight.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ClinicToolStorage storage = other.GetComponent<ClinicToolStorage>();
        if (storage != null && storage == nearbyStorage)
        {
            storage.highlight.SetActive(false);
            nearbyStorage = null;
        }
    }

    public void ForceClearSample()
    {
        holdingSample = false;
    }

    public ClinicEnum.ToolType? GetHeldTool() => heldTool;
    public bool IsHoldingSample() => holdingSample;
}
