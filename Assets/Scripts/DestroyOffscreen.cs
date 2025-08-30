using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    public Camera cam;

    private void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

    }

    void Update()
    {
        if (cam == null) return;

        Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);


        if (viewportPos.x < 0f)  // only left side
        {
            Destroy(gameObject);
        }
    }
}
