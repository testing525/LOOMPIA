using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Target to Follow")]
    public Transform target; 

    void Update()
    {
        if (target != null)
        {
            Vector3 newPos = transform.position;

            newPos.x = target.position.x;

            transform.position = newPos;
        }
    }
}
