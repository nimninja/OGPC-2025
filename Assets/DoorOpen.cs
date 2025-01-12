using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour
{
    [Header("Door Settings")]
    public GameObject door; // The door object to rotate
    public float rotationSpeed = 2.0f; // Speed of the rotation
    public float rotationAmount = 140f; // How much the door should rotate

    private bool isOpen = false; // Track if the door is open
    private Coroutine animationCoroutine;
    private bool isAnimating = false; // Prevent interaction while door is moving
    private Vector3 forward;
    private float initialXRotation;
    private float initialZRotation;

    void Start()
    {
        // Set the forward direction to the right of the door's frame
        forward = transform.right;

        // Capture the initial X and Z rotations of the door
        initialXRotation = door.transform.eulerAngles.x;
        initialZRotation = door.transform.eulerAngles.z;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isAnimating)
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }

            float dot = Vector3.Dot(forward, transform.InverseTransformPoint(transform.position).normalized);
            animationCoroutine = StartCoroutine(DoRotationOpen(dot));
        }
    }

    private IEnumerator DoRotationOpen(float forwardAmount)
    {
        isAnimating = true; // Lock interaction

        Quaternion startRotation = Quaternion.Euler(initialXRotation, door.transform.eulerAngles.y, initialZRotation);
        Quaternion endRotation;

        if (!isOpen)
        {
            endRotation = Quaternion.Euler(initialXRotation, door.transform.eulerAngles.y + rotationAmount, initialZRotation);
            isOpen = true;
        }
        else
        {
            endRotation = Quaternion.Euler(initialXRotation, door.transform.eulerAngles.y - rotationAmount, initialZRotation);
            isOpen = false;
        }

        float elapsedTime = 0f;
        float duration = rotationSpeed; // Use rotationSpeed as duration for consistency
        while (elapsedTime < duration)
        {
            door.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door.transform.rotation = endRotation; // Ensure exact final position

        isAnimating = false; // Unlock interaction
    }
}
