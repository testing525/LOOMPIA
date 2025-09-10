using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoryIntroductionManager : MonoBehaviour
{
    public static StoryIntroductionManager Instance;

    [Header("Image Settings")]
    [SerializeField] private Image storyImage;                // UI Image that will change
    [SerializeField] private Sprite[] storySprites;           // Sprites to display
    [SerializeField] private float fadeDuration = 1f;         // Fade speed

    private int currentImageIndex = -1;                       // Track current image index
    private Coroutine fadeRoutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (storyImage != null)
            storyImage.color = new Color(1, 1, 1, 0); // Start fully transparent
    }

    private void Update()
    {
        // Keep checking the DialogueManager’s instruction index
        if (DialogueManager.Instance != null)
        {
            int dialogueIndex = DialogueManager.Instance.CurrentInstructionIndex;

            // If index changed, update the image
            if (dialogueIndex != currentImageIndex)
            {
                currentImageIndex = dialogueIndex;
                ChangeImage(currentImageIndex);
            }
        }
    }

    private void ChangeImage(int index)
    {
        if (storySprites == null || storySprites.Length == 0) return;
        if (index < 0 || index >= storySprites.Length) return;

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeImageRoutine(storySprites[index]));
    }

    private IEnumerator FadeImageRoutine(Sprite newSprite)
    {
        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            storyImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        storyImage.color = new Color(1, 1, 1, 0f);

        // Change sprite
        storyImage.sprite = newSprite;

        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            storyImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        storyImage.color = new Color(1, 1, 1, 1f);
    }
}
