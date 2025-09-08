using UnityEngine;
using System.Collections;

public class RepeatAnimation : MonoBehaviour
{
    public Animator animator;        // Animator reference
    public string triggerName;       // Trigger parameter name in Animator
    public float pauseTime = 30f;    // Pause between animations
    public float animDuration = 3f;  // Duration of the animation

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        StartCoroutine(PlayAnimationLoop());
    }

    IEnumerator PlayAnimationLoop()
    {
        while (true)
        {
            // Play animation
            animator.SetTrigger(triggerName);

            // Wait for animation to finish (2–3 sec)
            yield return new WaitForSeconds(animDuration);

            // Pause before next animation
            yield return new WaitForSeconds(pauseTime);
        }
    }
}
