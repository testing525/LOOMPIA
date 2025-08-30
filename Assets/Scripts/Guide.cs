using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Guide : MonoBehaviour
{
    [Header("Target Group")]
    public GameObject guide;        

    [Header("Fade Settings")]
    public float fadeDuration = 1f; 
    public float stayDuration = 4f; 

    private Image[] childImages;
    private TextMeshProUGUI[] childTexts;

    void Awake()
    {
        childImages = guide.GetComponentsInChildren<Image>(includeInactive: true);
        childTexts = guide.GetComponentsInChildren<TextMeshProUGUI>(includeInactive: true);
    }

    public void PlayGuide()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInOutRoutine());
    }

    private IEnumerator FadeInOutRoutine()
    {
        guide.SetActive(true);

        yield return StartCoroutine(FadeRoutine(0f, 1f, fadeDuration));

        yield return new WaitForSeconds(stayDuration);

        yield return StartCoroutine(FadeRoutine(1f, 0f, fadeDuration));

        guide.SetActive(false);
    }

    private IEnumerator FadeRoutine(float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(endAlpha);
    }

    private void SetAlpha(float alpha)
    {
        foreach (var img in childImages)
        {
            if (img != null)
            {
                Color c = img.color;
                c.a = alpha;
                img.color = c;
            }
        }

        foreach (var txt in childTexts)
        {
            if (txt != null)
            {
                Color c = txt.color;
                c.a = alpha;
                txt.color = c;
            }
        }
    }
}
