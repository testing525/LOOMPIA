using UnityEngine;

public class BirdPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;

    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
    }

    void Update()
    {
        // Move toward the current point
        if (currentPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }

        // Flip based on movement direction
        if (rb.linearVelocity.x > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1); // facing right
        }
        else if (rb.linearVelocity.x < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1); // facing left
        }

        // Switch direction if close enough to the point
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            currentPoint = (currentPoint == pointB.transform) ? pointA.transform : pointB.transform;
        }
    }

    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(pointA.transform.position, 0.3f);
            Gizmos.DrawWireSphere(pointB.transform.position, 0.3f);
            Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
        }
    }
}