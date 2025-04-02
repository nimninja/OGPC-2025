using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectCycler : MonoBehaviour
{
    public GameObject[] objects;  // Assign Cube, Cylinder, and Sphere in the Inspector
    public GameObject upgradePanel; // Assign your Upgrade Panel (UI) in the Inspector
    public TextMeshProUGUI interactionText; // Drag your TextMeshProUGUI for "Press F to Upgrade"
    public float detectionRange = 3f; // Distance to trigger message

    private Transform player;
    private bool isNear = false;
    private bool panelOpened = false;
    private int currentIndex = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Ensure your player has the "Player" tag
        upgradePanel.SetActive(false); // Hide the upgrade UI initially
        interactionText.gameObject.SetActive(false); // Hide the interaction text initially
    }

    void Update()
    {
        // Check distance to the closest object
        isNear = false;
        foreach (GameObject obj in objects)
        {
            if (Vector3.Distance(player.position, obj.transform.position) < detectionRange)
            {
                isNear = true;
                break;
            }
        }

        // Show interaction text when near an object
        interactionText.gameObject.SetActive(isNear && !panelOpened);

        // Open upgrade panel when pressing "F" near an object
        if (isNear && Input.GetKeyDown(KeyCode.F) && !panelOpened)
        {
            OpenUpgradePanel();
        }
    }

    void OpenUpgradePanel()
    {
        upgradePanel.SetActive(true);
        interactionText.gameObject.SetActive(false); // Hide "Press F to Upgrade" text
        panelOpened = true;
    }

    public void CycleObjects()
    {
        // Disable the current object
        objects[currentIndex].SetActive(false);

        // Move to the next object
        currentIndex = (currentIndex + 1) % objects.Length;

        // Enable the new object
        objects[currentIndex].SetActive(true);

        // Close the UI panel after the first press
        upgradePanel.SetActive(false);
        panelOpened = false; // Allow re-opening when close again
    }
}
