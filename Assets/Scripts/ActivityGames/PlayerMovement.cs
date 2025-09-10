using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Needed for PointerDown/Up

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public float speed = 5f;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [Header("Mobile Buttons")]
    public Button upButton;
    public Button downButton;
    public Button leftButton;
    public Button rightButton;

    private Vector3 initialScale;

    // Mobile input values
    private float mobileX = 0f;
    private float mobileY = 0f;

    void Start()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }
        body.gravityScale = 0f; // Disable gravity for top-down movement

        initialScale = transform.localScale;


        // Add event listeners if buttons are assigned
        if (upButton != null) AddEventTriggers(upButton, () => MoveUp(true), () => MoveUp(false));
        if (downButton != null) AddEventTriggers(downButton, () => MoveDown(true), () => MoveDown(false));
        if (leftButton != null) AddEventTriggers(leftButton, () => MoveLeft(true), () => MoveLeft(false));
        if (rightButton != null) AddEventTriggers(rightButton, () => MoveRight(true), () => MoveRight(false));
    }

    void Update()
    {
        // --- PC Input ---
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        // --- Merge Mobile + PC Input ---
        float finalX = Mathf.Clamp(xInput + mobileX, -1f, 1f);
        float finalY = Mathf.Clamp(yInput + mobileY, -1f, 1f);

        Vector2 input = new Vector2(finalX, finalY).normalized;

        // Apply velocity
        body.linearVelocity = input * speed;

        // Debug movement values
        if (mobileX != 0 || mobileY != 0)
        {
            Debug.Log($"Mobile input active: X={mobileX}, Y={mobileY}");
        }

        // Flip player direction
        if (finalX != 0)
        {
            if (speed <= 0) { return; }

            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(finalX) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        // Animator
        if (input.magnitude > 0)
        {
            if (speed <= 0) { return; }
            animator.SetBool("isRunning", true);
            transform.localScale = new Vector3(
                Mathf.Sign(transform.localScale.x) * Mathf.Abs(initialScale.x),
                initialScale.y,
                initialScale.z
            );
        }
        else
        {
            if (speed <= 0) { return; }
            animator.SetBool("isRunning", false);
            transform.localScale = new Vector3(
                Mathf.Sign(transform.localScale.x) * Mathf.Abs(initialScale.x),
                initialScale.y,
                initialScale.z
            );
        }
    }

    public void MoveUp(bool pressed)
    {
        mobileY = pressed ? 1f : 0f;
        Debug.Log("MoveUp pressed=" + pressed);
    }

    public void MoveDown(bool pressed)
    {
        mobileY = pressed ? -1f : 0f;
        Debug.Log("MoveDown pressed=" + pressed);
    }

    public void MoveLeft(bool pressed)
    {
        mobileX = pressed ? -1f : 0f;
        Debug.Log("MoveLeft pressed=" + pressed);
    }

    public void MoveRight(bool pressed)
    {
        mobileX = pressed ? 1f : 0f;
        Debug.Log("MoveRight pressed=" + pressed);
    }

    private void AddEventTriggers(Button button, System.Action onDown, System.Action onUp)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null) trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entryDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        entryDown.callback.AddListener((data) => { onDown(); });
        trigger.triggers.Add(entryDown);

        EventTrigger.Entry entryUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        entryUp.callback.AddListener((data) => { onUp(); });
        trigger.triggers.Add(entryUp);
    }
}
