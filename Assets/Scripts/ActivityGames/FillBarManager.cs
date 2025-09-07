using UnityEngine;

public class FillBarManager : MonoBehaviour
{
    public static FillBarManager Instance;

    [Header("Fill Settings")]
    public GameObject fillBar;       
    public float maxScaleX = 0.4f;   
    private float currentFill = 0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (fillBar != null)
        {
            fillBar.transform.localScale = new Vector3(0f, fillBar.transform.localScale.y, fillBar.transform.localScale.z);

        }
    }

    public bool Fill(float value, float maxFill)
    {
        currentFill += value;
        currentFill = Mathf.Clamp(currentFill, 0f, maxFill);

        if (fillBar != null)
        {
            float scaleX = (currentFill / maxFill) * maxScaleX;
            fillBar.transform.localScale = new Vector3(scaleX, fillBar.transform.localScale.y, fillBar.transform.localScale.z);
        }

        return currentFill >= maxFill;
    }

    public void ResetFill()
    {
        currentFill = 0f;
        if (fillBar != null)
        {
            fillBar.transform.localScale = new Vector3(0f, fillBar.transform.localScale.y, fillBar.transform.localScale.z);

        }
    }
}
