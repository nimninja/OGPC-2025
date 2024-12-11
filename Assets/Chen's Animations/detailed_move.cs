using UnityEngine;

public class detailed_move : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Reset all movement-related triggers
        animator.ResetTrigger("Run Left");
        animator.ResetTrigger("Right Run");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Back Run");
        animator.ResetTrigger("idli"); // Ensure idli trigger is reset before setting it

        // Check input and activate corresponding animations
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetTrigger("Run Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetTrigger("Right Run");
        }
        else if (Input.GetKey(KeyCode.W))
        {
            animator.SetTrigger("Run");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetTrigger("Back Run");
        }
        else
        {
            // Set idli trigger if no movement keys are pressed
            animator.SetTrigger("idli");
        }
    }
}
