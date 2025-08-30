using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    public Transform player; // drag your player here

    [Header("Offset")]
    public Vector2 offset = new Vector2(3f, 1f);

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 newPos = new Vector3(
            player.position.x + offset.x,
            player.position.y + offset.y,
            transform.position.z
        );

        transform.position = newPos;
    }
}
