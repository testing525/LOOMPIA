using UnityEngine;

public class BookBox : MonoBehaviour
{
    [Header("Settings")]
    public Bookshelf.ShelfType boxType;

    [Header("Visuals")]
    public GameObject highlight; 
    private void Awake()
    {
        if (highlight != null)
        {
            highlight.SetActive(false);

        }
    }

    public void ShowHighlight(bool state)
    {
        if (highlight != null)
        {
            highlight.SetActive(state);

        }
    }
}
