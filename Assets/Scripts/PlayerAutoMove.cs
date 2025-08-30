using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    [Header("Animation")]
    public float normalAnimSpeed = 1f;
    public float slowAnimSpeed = 0.2f;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SetNormalAnimSpeed();
    }

    void FixedUpdate()
    {
        float multiplier = GameSpeed.Instance != null ? GameSpeed.Instance.GetMultiplier() : 1f;
        rb.velocity = new Vector2(moveSpeed * multiplier, rb.velocity.y);
    }

    public void SetNormalAnimSpeed()
    {
        if (anim != null)
            anim.speed = normalAnimSpeed;
    }

    public void SetSlowAnimSpeed()
    {
        if (anim != null)
            anim.speed = slowAnimSpeed;
    }
}
