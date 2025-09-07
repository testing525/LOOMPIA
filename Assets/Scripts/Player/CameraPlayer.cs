using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    [Header("Target to follow")]
    public Transform target;

    [Header("Camera Settings")]
    public float smoothSpeed = 5f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        desiredPosition.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
