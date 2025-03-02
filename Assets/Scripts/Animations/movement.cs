using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 60f;
    public float jumpPower = 7f;
    public float gravity = 20f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;
    public float slideSpeed = 6f; // Speed for sliding on steep slopes
    public float slopeLimit = 45f; // Maximum angle before sliding occurs

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        // Lock the cursor and make it invisible when the game starts
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        // Enable step climbing
        characterController.stepOffset = 0.5f;
        characterController.slopeLimit = slopeLimit; // Prevent climbing very steep slopes
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Movement
        float curSpeedX = canMove ? walkSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? walkSpeed * Input.GetAxis("Horizontal") : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (characterController.isGrounded)
        {
            if (Input.GetButton("Jump") && canMove && !IsOnSteepSlope())
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = -1f; // Small downward force to keep grounded
            }

            // Sliding on steep slopes
            if (IsSliding())
            {
                Vector3 slideDirection = new Vector3(hitInfo.normal.x, -hitInfo.normal.y, hitInfo.normal.z);
                moveDirection += slideDirection * slideSpeed;
            }
        }
        else
        {
            moveDirection.y = movementDirectionY - (gravity * Time.deltaTime);
        }

        // Crouching
        if (Input.GetKey(KeyCode.LeftShift) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
        }

        // Move the character
        characterController.Move(moveDirection * Time.deltaTime);

        // Camera and player rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    // Detect if the player is on a steep slope
    private RaycastHit hitInfo;
    private bool IsSliding()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, characterController.height / 2 + 0.1f))
        {
            float slopeAngle = Vector3.Angle(hitInfo.normal, Vector3.up);
            return slopeAngle > characterController.slopeLimit;
        }
        return false;
    }

    // Check if the player is on a steep slope to prevent jumping
    private bool IsOnSteepSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, characterController.height / 2 + 0.1f))
        {
            float slopeAngle = Vector3.Angle(hitInfo.normal, Vector3.up);
            return slopeAngle > slopeLimit;
        }
        return false;
    }
}
