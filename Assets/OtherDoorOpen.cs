using UnityEngine;
using System.Collections;

public class OtherDoorOpen : MonoBehaviour
{
    [Header("Other Door Settings")]
    private GameObject otherDoor; // The door object to rotate
    public float rotationSpeed = 2.0f; // Speed of the rotation
    public float rotationAmount = -140f; // Rotation amount for "OtherDoor" (-140 degrees)

    private bool otherisAnimating = false; // Prevent interaction while the door is moving
    private Coroutine otheranimationCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OtherDoor"))
        {
            otherDoor = other.gameObject; // Assign the door object
            Debug.Log("OtherDoor detected and assigned in OnTriggerEnter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("OtherDoor") && other.gameObject == otherDoor)
        {
            Debug.Log("OtherDoor exited trigger");
            // Do not set to null here; let the animation handle it
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && otherDoor != null && !otherisAnimating)
        {
            if (otheranimationCoroutine != null)
            {
                StopCoroutine(otheranimationCoroutine);
            }

            // Start the rotation coroutine
            otheranimationCoroutine = StartCoroutine(DoRotation());
        }
    }

    private IEnumerator DoRotation()
    {
        otherisAnimating = true; // Lock interaction

        if (otherDoor == null)
        {
            otherisAnimating = false;
            yield break; // Exit coroutine if the door is null
        }

        // Determine if the door is currently open or closed
        float currentYRotation = NormalizeAngle(otherDoor.transform.localEulerAngles.y);
        float targetYRotation;

        if (Mathf.Approximately(currentYRotation, NormalizeAngle(-90f)))
        {
            // Door is at -90 degrees, open it
            targetYRotation = NormalizeAngle(currentYRotation - 140f);
        }
        else if (Mathf.Approximately(currentYRotation, NormalizeAngle(-230f)))
        {
            // Door is at 50 degrees, close it
            targetYRotation = NormalizeAngle(currentYRotation + 140f);
        }
        else
        {
            Debug.LogWarning("OtherDoor rotation is in an unexpected state");
            otherisAnimating = false;
            yield break; // Exit if the door is in an unexpected state
        }

        float startYRotation = currentYRotation;
        float elapsedTime = 0f;
        while (elapsedTime < rotationSpeed)
        {
            if (otherDoor == null)
            {
                otherisAnimating = false;
                yield break; // Exit coroutine if the door is null during animation
            }

            float currentYRotationInterpolated = Mathf.LerpAngle(startYRotation, targetYRotation, elapsedTime / rotationSpeed);
            otherDoor.transform.localEulerAngles = new Vector3(
                otherDoor.transform.localEulerAngles.x, // Preserve X rotation
                currentYRotationInterpolated, // Modify Y rotation
                otherDoor.transform.localEulerAngles.z // Preserve Z rotation
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the door ends at the exact target rotation
        otherDoor.transform.localEulerAngles = new Vector3(
            otherDoor.transform.localEulerAngles.x,
            targetYRotation,
            otherDoor.transform.localEulerAngles.z
        );

        Debug.Log("Animation finished. Setting otherDoor to null.");
        otherDoor = null; // Clear the door reference after animation finishes
        otherisAnimating = false; // Unlock interaction
    }

    private float NormalizeAngle(float angle)
    {
        while (angle > 360f) angle -= 360f;
        while (angle < 0f) angle += 360f;
        return angle;
    }
}
