using UnityEngine;
using System.Collections;

public class TutorialTrashcan : MonoBehaviour
{
    private TutorialManagerScript tutorialManager;

    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite closedSprite;
    public Sprite openSprite;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float exitOffset = 5f;

    private Camera mainCam;

    private void Start()
    {
        tutorialManager = FindAnyObjectByType<TutorialManagerScript>();
        if (spriteRenderer != null && closedSprite != null)
        {
            spriteRenderer.sprite = closedSprite;
        }

        mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        float trashcanSpeed = moveSpeed * GameSpeed.Instance.GetMultiplier();
        transform.position += Vector3.left * trashcanSpeed * Time.deltaTime;

        float leftEdge = mainCam.ViewportToWorldPoint(new Vector3(0, 0.5f, mainCam.nearClipPlane)).x;

        if (transform.position.x < leftEdge - exitOffset)
        {
            WrongAnswer();
        }
    }

    // ✅ Subject correct → just open
    public void CorrectAnswer()
    {
        if (spriteRenderer != null && openSprite != null)
        {
            spriteRenderer.sprite = openSprite;
        }
    }

    // ✅ Problem correct → close then fade out (no respawn)
    public void CorrectProblemAnswer()
    {
        if (spriteRenderer != null && closedSprite != null)
        {
            spriteRenderer.sprite = closedSprite;
        }

        StartCoroutine(FadeOutAndDestroy(false));
    }

    // ❌ Wrong subject or problem → fade out and respawn
    public void WrongAnswer()
    {
        StartCoroutine(FadeOutAndDestroy(true));
    }

    private IEnumerator FadeOutAndDestroy(bool respawn = true)
    {
        float duration = 1f;
        float time = 0f;
        Color startColor = spriteRenderer.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, time / duration);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        if (respawn && tutorialManager != null)
        {
            tutorialManager.SpawnTrashcan();
        }

        Destroy(gameObject);
    }
}
