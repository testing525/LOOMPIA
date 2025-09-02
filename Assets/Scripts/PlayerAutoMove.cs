using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    [Header("Animation")]
    public float normalAnimSpeed = 1f;
    public float slowAnimSpeed = 0.2f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        SetNormalAnimSpeed();
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
