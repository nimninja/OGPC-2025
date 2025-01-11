using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour
{
    [Header("Door Settings")]
    private GameObject door; // The door object to rotate
    public float rotationSpeed = 2.0f; // Speed of the rotation
    public float defaultRotationAmount = 90f; // Default rotation amount (90 degrees)

    private bool isAnimating = false; // Prevent interaction while door is moving
    private Coroutine animationCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            door = other.gameObject; // Assign the door object
            Debug.Log("Door detected and assigned in OnTriggerEnter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door") && other.gameObject == door)
        {
            Debug.Log("Door exited trigger");
            // Do not set to null here; let the animation handle it
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && door != null && !isAnimating)
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }

            // Start the rotation coroutine
            animationCoroutine = StartCoroutine(DoRotation());
        }
    }

    private IEnumerator DoRotation()
    {
        isAnimating = true; // Lock interaction

        if (door == null)
        {
            isAnimating = false;
            yield break; // Exit coroutine if the door is null
        }

        // Determine if the door is currently open or closed
        float currentYRotation = NormalizeAngle(door.transform.localEulerAngles.y);
        float targetYRotation;

        if (Mathf.Approximately(currentYRotation, NormalizeAngle(-90f)))
        {
            // Door is at -90 degrees, open it to 50 degrees
            targetYRotation = NormalizeAngle(currentYRotation + 140f);
        }
        else if (Mathf.Approximately(currentYRotation, NormalizeAngle(50f)))
        {
            // Door is at 50 degrees, close it to -90 degrees
            targetYRotation = NormalizeAngle(currentYRotation - 140f);
        }
        else
        {
            Debug.LogWarning("Door rotation is in an unexpected state");
            isAnimating = false;
            yield break; // Exit if the door is in an unexpected state
        }

        float startYRotation = currentYRotation;
        float elapsedTime = 0f;
        while (elapsedTime < rotationSpeed)
        {
            if (door == null)
            {
                isAnimating = false;
                yield break; // Exit coroutine if the door is null during animation
            }

            float currentYRotationInterpolated = Mathf.LerpAngle(startYRotation, targetYRotation, elapsedTime / rotationSpeed);
            door.transform.localEulerAngles = new Vector3(
                door.transform.localEulerAngles.x, // Preserve X rotation
                currentYRotationInterpolated, // Modify Y rotation
                door.transform.localEulerAngles.z // Preserve Z rotation
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the door ends at the exact target rotation
        door.transform.localEulerAngles = new Vector3(
            door.transform.localEulerAngles.x,
            targetYRotation,
            door.transform.localEulerAngles.z
        );

        Debug.Log("Animation finished. Setting door to null.");
        door = null; // Clear the door reference after animation finishes
        isAnimating = false; // Unlock interaction
    }

    private float NormalizeAngle(float angle)
    {
        while (angle > 360f) angle -= 360f;
        while (angle < 0f) angle += 360f;
        return angle;
    }
}
