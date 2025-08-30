using UnityEngine;

public class TrashcanVisibilityDetector : MonoBehaviour
{
    private GameManager gameManager;
    private HealthManager healthManager;
    private Camera mainCam;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        mainCam = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject canvas = GameObject.Find("UI"); 
        if (canvas != null)
        {
            Transform livesFrame = canvas.transform.Find("LivesFrame");
            if (livesFrame != null)
            {
                healthManager = livesFrame.GetComponent<HealthManager>();
            }
        }
    }

    void Update()
    {
        if (mainCam == null || gameManager == null || spriteRenderer == null || healthManager == null) return;

        float camLeftEdge = mainCam.transform.position.x - mainCam.orthographicSize * mainCam.aspect;
        float trashcanRightEdge = spriteRenderer.bounds.max.x;

        if (trashcanRightEdge < camLeftEdge)
        {
            Debug.Log("Trashcan fully exited left side");

            gameManager.OnTrashcanExited(gameObject);
            healthManager.DecreaseHP();

            Destroy(this); 
        }
    }
}
