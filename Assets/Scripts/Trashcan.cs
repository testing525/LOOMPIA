using UnityEngine;

public class Trashcan : MonoBehaviour
{
    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite closedSprite;
    public Sprite openSprite;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float exitOffset = 5f; 

    private GameManager gm;
    private Camera mainCam;

    private void Reset()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
        mainCam = Camera.main;
    }

    private void Update()
    {
        float trashcanSpeed = moveSpeed * GameSpeed.Instance.GetMultiplier(); ;
        transform.position += Vector3.left * trashcanSpeed * Time.deltaTime;

        float leftEdge = mainCam.ViewportToWorldPoint(new Vector3(0, 0.5f, mainCam.nearClipPlane)).x;



        if (transform.position.x < leftEdge - exitOffset)
        {
            if (gm != null)
                gm.OnTrashcanExited(gameObject);

            Destroy(gameObject);
            HealthManager.Instance.DecreaseHP();
        }
    }

    public void Open()
    {
        if (spriteRenderer != null && openSprite != null)
        {
            spriteRenderer.sprite = openSprite;
        }
    }

    public void Close()
    {
        if (spriteRenderer != null && closedSprite != null)
        {
            spriteRenderer.sprite = closedSprite;
        }
    }
}
