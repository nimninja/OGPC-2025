using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class treeHarvest : MonoBehaviour
{
    public GameObject axePanel; // UI panel containing the axe UI elements
    public Image axeFill; // The "colored axe" progress bar
    public Image axeSilhouette; // The silhouette axe (always visible)
    public TextMeshProUGUI harvestText; // The "Harvest" text
    public GameObject woodManager; // Reference to the empty object with the AddWood function

    public float harvestTime = 3f; // Time required to complete the harvest
    private float holdTimer = 0f; // Timer to track button hold duration
    private bool isHarvesting = false; // To avoid conflicts

    private GameObject currentTree; // Store the tree being harvested

    void Update()
    {
        if (isHarvesting && Input.GetKey(KeyCode.H))
        {
            holdTimer += Time.deltaTime;

            axeFill.fillAmount = holdTimer / harvestTime;

            if (holdTimer >= harvestTime)
            {
                CompleteHarvest();
            }
        }
        else if (isHarvesting && Input.GetKeyUp(KeyCode.H))
        {
            ResetHarvest();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            currentTree = other.gameObject; // Store the tree being harvested
            axePanel.SetActive(true); // Show the UI
            ResetHarvest(); // Reset for a new tree
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            currentTree = null;
            axePanel.SetActive(false); // Hide the UI
            ResetHarvest();
        }
    }

    private void CompleteHarvest()
    {
        Debug.Log("Harvest complete!");

        // Call AddWood function on the referenced woodManager GameObject
        if (woodManager != null)
        {
            var woodScript = woodManager.GetComponent<amountManager>(); // Replace YourScriptName with the script's name
            if (woodScript != null)
            {
                woodScript.AddWood(3); // Pass the value 3 to the AddWood function
            }
        }

        // Destroy the tree
        if (currentTree != null)
        {
            Destroy(currentTree);
        }

        // Hide the UI and reset
        ResetHarvest();
        axePanel.SetActive(false);
    }

    private void ResetHarvest()
    {
        holdTimer = 0f;
        axeFill.fillAmount = 0f;
        isHarvesting = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tree") && Input.GetKeyDown(KeyCode.H))
        {
            isHarvesting = true; // Start harvesting
        }
    }
}
