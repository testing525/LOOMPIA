using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreStreakFX : MonoBehaviour
{
    [Header("Assign TextMeshProUGUI references")]
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;

    private Vector3 originalScale1;
    private Vector3 originalScale2;

    private int colorIndex = 0;
    private Color[] colors = new Color[]
    {
        Color.white,
        Color.yellow,
        new Color(1f, 0.5f, 0f),
        Color.red,
        new Color(0.7f, 0f, 0.2f),
        Color.magenta, 
        new Color(0.4f, 0f, 0.8f), 
        Color.blue
    };

    void Start()
    {
        if (text1 != null) originalScale1 = text1.transform.localScale;
        if (text2 != null) originalScale2 = text2.transform.localScale;
    }

    public void PlayBounceShake(float duration = 0.5f, float scaleMultiplier = 1.2f, float shakeIntensity = 5f)
    {
        if (text1 != null) StartCoroutine(BounceShakeCoroutine(text1, originalScale1, duration, scaleMultiplier, shakeIntensity));
        if (text2 != null) StartCoroutine(BounceShakeCoroutine(text2, originalScale2, duration, scaleMultiplier, shakeIntensity));

        ApplyNextColor();
    }

    private IEnumerator BounceShakeCoroutine(TextMeshProUGUI tmp, Vector3 originalScale, float duration, float scaleMultiplier, float shakeIntensity)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            float scale = Mathf.Lerp(scaleMultiplier, 1f, t);
            tmp.transform.localScale = originalScale * scale;

            float shake = Mathf.Sin(Time.time * 50f) * (shakeIntensity * (1 - t));
            tmp.rectTransform.localRotation = Quaternion.Euler(0, 0, shake);

            yield return null;
        }

        tmp.transform.localScale = originalScale;
        tmp.rectTransform.localRotation = Quaternion.identity;
    }

    private void ApplyNextColor()
    {
        if (colorIndex < colors.Length - 1)
            colorIndex++;

        Color newColor = colors[colorIndex];
        if (text1 != null) text1.color = newColor;
        if (text2 != null) text2.color = newColor;
    }
    public void ResetColor()
    {
        colorIndex = 0;

        if (text1 != null) text1.color = Color.white;
        if (text2 != null) text2.color = Color.white;
    }
}
