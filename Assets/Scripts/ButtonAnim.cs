using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonAnim : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [Header("Button Sprites")]
    public Sprite normalSprite;
    public Sprite pressedSprite;

    private Image buttonImage;
    private RectTransform textRect;

    void Awake()
    {
        buttonImage = GetComponent<Image>();

        if (normalSprite != null)
            buttonImage.sprite = normalSprite;

        // Find the TMP text inside (child)
        TextMeshProUGUI tmpText = GetComponentInChildren<TextMeshProUGUI>();
        if (tmpText != null)
        {
            textRect = tmpText.GetComponent<RectTransform>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (pressedSprite != null)
            buttonImage.sprite = pressedSprite;

        if (textRect != null)
        {
            textRect.offsetMax = new Vector2(textRect.offsetMax.x, -8f); 
            textRect.offsetMin = new Vector2(textRect.offsetMin.x, -8f); 
        }

        AudioManager.FireSFX(AudioManager.SFXSignal.Button);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Down");
        if (normalSprite != null)
            buttonImage.sprite = normalSprite;

        if (textRect != null)
        {
            textRect.offsetMax = new Vector2(textRect.offsetMax.x, 8f); 
            textRect.offsetMin = new Vector2(textRect.offsetMin.x, 8f);   
        }
    }
}
