using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance { get; private set; }

    [Header("Transition Settings")]
    public bool isFadeIn = true;         
    public float transitionSpeed = 2f;    
    public Image image;                  

    [HideInInspector] public bool isReadyToChangeScene = false;

    private RectTransform rect;
    private Vector2 originalPos;

    private void Awake()
    {
        Instance = this;

        rect = image.GetComponent<RectTransform>();
        originalPos = rect.anchoredPosition; 
    }

    private void Start()
    {
        image.gameObject.SetActive(true);
    }

    public IEnumerator FadeIn()
    {
        rect.anchoredPosition = new Vector2(-2579f, 0f);
        Vector2 targetPos = new Vector2(0f, 0f);

        while (Vector2.Distance(rect.anchoredPosition, targetPos) > 0.1f)
        {
            Debug.Log("is fading in!");
            rect.anchoredPosition = Vector2.MoveTowards(
                rect.anchoredPosition,
                targetPos,
                transitionSpeed * Time.deltaTime * 1000f // scale if needed
            );
            yield return null;
        }

        Debug.Log("is ready!");
        rect.anchoredPosition = targetPos;
        isReadyToChangeScene = true;
    }

    public IEnumerator FadeOut()
    {
        rect.anchoredPosition = new Vector2(0f, 0f);
        Vector2 targetPos = new Vector2(-2579f, 0f);


        while (Vector2.Distance(rect.anchoredPosition, targetPos) > 0.1f)
        {
            rect.anchoredPosition = Vector2.MoveTowards(
                rect.anchoredPosition,
                targetPos,
                transitionSpeed * Time.deltaTime * 1000f // scale if needed
            );
            yield return null;
        }

        rect.anchoredPosition = targetPos;
        isReadyToChangeScene = true;
    }
}
