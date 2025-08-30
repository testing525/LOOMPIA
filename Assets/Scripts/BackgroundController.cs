using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [Header("References")]
    public GameObject cam;           

    [Header("Parallax Settings")]
    public float parallaxEffect = -0.5f;

    [Header("Recycle Settings")]
    public bool isRecycle;
    public float recycleOffset = 2f;     
    public float detectOffset = 1f;      

    private Vector3 startPos;
    private float spriteWidth;

    void Start()
    {
        startPos = transform.position;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            spriteWidth = sr.bounds.size.x;
        else
            spriteWidth = 0;
    }

    void LateUpdate()
    {
        if (cam == null) return;

        float camX = cam.transform.position.x;

        Vector3 newPos = transform.position;
        newPos.x = startPos.x + (camX - startPos.x) * parallaxEffect;
        transform.position = newPos;

        if (isRecycle)
        {
            if (newPos.x + spriteWidth < camX - detectOffset)
            {
                startPos.x += spriteWidth + recycleOffset;
            }
        }

        
    }
}
