using UnityEngine;

public class TrashcanPlaceFollow : MonoBehaviour
{
    [Header("References")]
    public Transform player; 
    public Vector3 offset = new Vector3(10f, 0, 0);

    void Update()
    {
        if (player == null) return;

        transform.position = new Vector3(
            player.position.x + offset.x,
            player.position.y + offset.y,
            transform.position.z
        );
    }
}
