using System.Collections;
using UnityEngine;

public class NotifyScript : MonoBehaviour
{
    [Header("Settings")]
    public float targetX = 630f;        
    public float smoothTime = 0.3f;     
    public float stayDuration = 2f;     

    private RectTransform rectTransform;
    private Vector2 originalPos;
    private Vector2 velocity = Vector2.zero;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPos = rectTransform.anchoredPosition; 
    }

    public void TriggerNotify()
    {
        StopAllCoroutines();
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        Vector2 targetPos = new Vector2(targetX, originalPos.y);

        while (Vector2.Distance(rectTransform.anchoredPosition, targetPos) > 0.1f)
        {
            rectTransform.anchoredPosition = Vector2.SmoothDamp(rectTransform.anchoredPosition, targetPos, ref velocity, smoothTime);
            yield return null;
        }

        rectTransform.anchoredPosition = targetPos;

        yield return new WaitForSeconds(stayDuration);

        velocity = Vector2.zero; 
        while (Vector2.Distance(rectTransform.anchoredPosition, originalPos) > 0.1f)
        {
            rectTransform.anchoredPosition = Vector2.SmoothDamp(rectTransform.anchoredPosition, originalPos, ref velocity, smoothTime);
            yield return null;
        }

        rectTransform.anchoredPosition = originalPos; 
    }
}
