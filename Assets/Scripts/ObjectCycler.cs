using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectCycler : MonoBehaviour
{
    public GameObject[] objects;             // Assign Cube, Cylinder, Sphere
    public GameObject upgradePanel;          // UI Panel with the cycle button
    public TextMeshProUGUI interactionText;  // TMP text: "Press F to interact"
    public Transform player;                 // Player or Camera transform
    public float interactionDistance = 3f;
    public Button cycleButton;               // UI Button that cycles objects

    private int currentIndex = 0;
    private bool isPanelVisible = false;

    void Start()
    {
        // Activate only the first object
        for (int i = 0; i < objects.Length; i++)
            objects[i].SetActive(i == currentIndex);

        // Hide panel & interaction text
        if (upgradePanel != null)
            upgradePanel.SetActive(false);
        if (interactionText != null)
            interactionText.gameObject.SetActive(false);

        // Attach CycleObjects to button
        if (cycleButton != null)
            cycleButton.onClick.AddListener(CycleObjects);
    }

    void Update()
    {
        if (player == null || objects.Length == 0) return;

        float dist = Vector3.Distance(player.position, objects[currentIndex].transform.position);
        bool isClose = dist <= interactionDistance;

        // Show/hide interaction prompt
        if (interactionText != null)
            interactionText.gameObject.SetActive(isClose && !isPanelVisible);

        // Toggle panel on F press
        if (isClose && Input.GetKeyDown(KeyCode.F))
        {
            isPanelVisible = !isPanelVisible;
            if (upgradePanel != null)
                upgradePanel.SetActive(isPanelVisible);
        }
    }

    public void CycleObjects()
    {
        // Disable current object
        objects[currentIndex].SetActive(false);

        // Move to next object
        currentIndex = (currentIndex + 1) % objects.Length;

        // Enable new object
        objects[currentIndex].SetActive(true);
    }
}
        