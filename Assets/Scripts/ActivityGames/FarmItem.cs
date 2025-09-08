using UnityEngine;

public class FarmItem : MonoBehaviour
{
    public FarmPlayerItemHandler.ItemType itemType;
    public GameObject highlight; // child for highlight
    [HideInInspector] public Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
        {
            if (highlight != null) highlight.SetActive(false);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && highlight != null)
        {
            highlight.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && highlight != null)
        {
            highlight.SetActive(false);

        }
    }
}
