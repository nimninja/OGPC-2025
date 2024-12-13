using UnityEngine;
using UnityEngine.UI; // If you're using the Unity Text component (not TextMeshPro)
using TMPro; // If you're using TextMeshProUGUI

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject cube;         // The cube that is initially shown
    public GameObject sphere;       // The sphere object
    public GameObject cylinder;     // The cylinder object
    public GameObject capsule;      // The capsule object
    public GameObject uiPanel;      // The UI panel that contains buttons and texts
    public TextMeshProUGUI promptText; // The text that says "Press F to open UI"
    public Transform player;        // Reference to the player object
    public Transform block;         // The block (cube) that triggers the UI when near
    public float interactionDistance = 5f; // Distance to show the prompt text and interact

    private bool isUIPanelOpen = false; // To track if the UI panel is open or not

    void Start()
    {
        // Hide all game objects (Cube, Sphere, Cylinder, Capsule)
        HideAll();

        // Initially, show the Cube
        cube.SetActive(true);

        // Hide the UI panel initially
        uiPanel.SetActive(false);

        // Hide the prompt text initially
        promptText.gameObject.SetActive(false);

        // Lock the cursor initially (it won't be shown or move)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Check the distance between the player and the block
        if (Vector3.Distance(player.position, block.position) < interactionDistance)
        {
            // Show the "Press F to open UI" text
            promptText.gameObject.SetActive(true);

            // If the player presses 'F', toggle the UI panel
            if (Input.GetKeyDown(KeyCode.F))
            {
                ToggleUI();
            }
        }
        else
        {
            // Hide the "Press F to open UI" text if player is far away
            promptText.gameObject.SetActive(false);
        }
    }

    // Toggle the visibility of the UI panel
    void ToggleUI()
    {
        isUIPanelOpen = !isUIPanelOpen;
        uiPanel.SetActive(isUIPanelOpen);

        // If the UI is open, unlock the cursor and make it visible
        if (isUIPanelOpen)
        {
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Make the cursor visible
        }
        else
        {
            // If the UI is closed, lock the cursor again
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
            Cursor.visible = false; // Hide the cursor
        }
    }

    // Show the Sphere and hide all other objects
    public void ShowSphere()
    {
        HideAll();
        sphere.SetActive(true);
    }

    // Show the Cylinder and hide all other objects
    public void ShowCylinder()
    {
        HideAll();
        cylinder.SetActive(true);
    }

    // Show the Capsule and hide all other objects
    public void ShowCapsule()
    {
        HideAll();
        capsule.SetActive(true);
    }

    // Hide all objects (Cube, Sphere, Cylinder, Capsule)
    private void HideAll()
    {
        cube.SetActive(false);
        sphere.SetActive(false);
        cylinder.SetActive(false);
        capsule.SetActive(false);
    }
}
