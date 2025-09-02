using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    [Header("Categories")]
    public ObjectSpeed[] clouds;
    public ObjectSpeed[] behind;
    public ObjectSpeed[] far;
    public ObjectSpeed[] mid;
    public ObjectSpeed[] front;
    public ObjectSpeed[] fences;
    public ObjectSpeed[] platforms;
    public ObjectSpeed[] npcs;

    [Header("Category Speeds")]
    public float cloudSpeed = 0.2f;
    public float behindSpeed = 0.5f;
    public float farSpeed = 0.8f;
    public float midSpeed = 1.2f;
    public float frontSpeed = 2f;
    public float fenceSpeed = 3f;
    public float platformSpeed = 3f;
    public float npcSpeed = 3.2f;

    [Header("Recycle Settings")]
    public Camera mainCamera;
    public float recycleOffset = 2f;   // how far past screen-left before recycle
    public float extraOffset = 0.0f;   // additional buffer before recycling
    public float seamFix = 0.001f;     // tiny nudge to avoid visible seams

    private float screenLeft;

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).x;
    }

    void Update()
    {
        // Floating recycle (no need to snap exactly)
        MoveEachObjects(clouds, cloudSpeed);
        MoveEachObjects(far, farSpeed);
        MoveEachObjects(mid, midSpeed);
        MoveEachObjects(front, frontSpeed);
        MoveEachObjects(npcs, npcSpeed);

        // Seamless snapping recycle
        SnapRecycle(behind, behindSpeed);
        SnapRecycle(fences, fenceSpeed);
        SnapRecycle(platforms, platformSpeed);
    }

    // Simple recycle: only resets X to a preset value
    private void MoveEachObjects(ObjectSpeed[] objects, float speed)
    {
        foreach (var o in objects)
        {
            if (o.obj == null) continue;
            float objectSpeed = speed * GameSpeed.Instance.GetMultiplier();
            o.obj.transform.Translate(Vector3.left * objectSpeed * Time.deltaTime);

            if (!TryGetBoundsX(o.obj, out _, out float right, out _, out _)) continue;

            if (right < screenLeft - recycleOffset - extraOffset)
            {
                var pos = o.obj.transform.position;
                pos.x = o.recycleX;
                o.obj.transform.position = pos;
            }
        }
    }

    // Seamless: place recycled tile immediately after the rightmost tile
    private void SnapRecycle(ObjectSpeed[] objects, float speed)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            var o = objects[i];
            if (o.obj == null) continue;

            float objectSpeed = speed * GameSpeed.Instance.GetMultiplier();
            o.obj.transform.Translate(Vector3.left * objectSpeed * Time.deltaTime);

            if (!TryGetBoundsX(o.obj, out _, out float right, out _, out float halfWidth)) continue;

            if (right < screenLeft - recycleOffset - extraOffset)
            {
                // Find the furthest right edge among the other tiles
                float maxRight = float.NegativeInfinity;
                for (int j = 0; j < objects.Length; j++)
                {
                    var other = objects[j];
                    if (other.obj == null || other.obj == o.obj) continue;
                    if (!TryGetBoundsX(other.obj, out _, out float r, out _, out _)) continue;
                    if (r > maxRight) maxRight = r;
                }
                if (float.IsNegativeInfinity(maxRight)) maxRight = right; // fallback

                float newCenterX = maxRight + halfWidth + seamFix;
                var pos = o.obj.transform.position;
                pos.x = newCenterX;
                o.obj.transform.position = pos;
            }
        }
    }

    private bool TryGetBoundsX(GameObject go, out float left, out float right, out float center, out float halfWidth)
    {
        left = right = center = halfWidth = 0f;
        var renderers = go.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return false;

        Bounds b = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++) b.Encapsulate(renderers[i].bounds);

        left = b.min.x;
        right = b.max.x;
        center = b.center.x;
        halfWidth = b.extents.x; 
        return true;
    }
}
