using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI text;
    public GameObject uiBox; 

    [Header("Dialogue Data")]
    public ActivityDialogueData dialogueData;
    public ActivityDialogueData errorDialogueData;

    [Header("Transition Settings")]
    public float offsetY = -100f;   
    public float smoothTime = 0.3f; 
    private int currentIndex = 0;
    private RectTransform boxTransform;
    private Vector2 originalPos;
    private Vector2 velocity = Vector2.zero;

    void Start()
    {
        boxTransform = uiBox.GetComponent<RectTransform>();
        originalPos = boxTransform.anchoredPosition;

        if (dialogueData != null && dialogueData.dialogues.Count > 0)
        {
            text.text = dialogueData.dialogues[0];
        }

        boxTransform.anchoredPosition = originalPos + new Vector2(0, offsetY);
        StartCoroutine(SlideUpToOriginal());
    }

    private IEnumerator SlideUpToOriginal()
    {
        while (Vector2.Distance(boxTransform.anchoredPosition, originalPos) > 0.1f)
        {
            boxTransform.anchoredPosition =
                Vector2.SmoothDamp(boxTransform.anchoredPosition, originalPos, ref velocity, smoothTime);
            yield return null;
        }

        boxTransform.anchoredPosition = originalPos; 
    }

    public void SetDialogue(int index)
    {
        SetDialogue(dialogueData, index);
    }

    public void SetDialogue(ActivityDialogueData data, int index)
    {
        if (data != null && index >= 0 && index < data.dialogues.Count)
        {
            text.text = data.dialogues[index];
            currentIndex = index;
        }
        else
        {
            Debug.LogWarning("Invalid dialogue index: " + index);
        }
    }
}
