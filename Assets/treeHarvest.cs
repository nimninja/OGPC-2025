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
    public GameObject hotbarManager; // Reference to the object with the hotbar script

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
            currentTree = other.gameObject;
            axePanel.SetActive(true);
            ResetHarvest();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            currentTree = null;
            axePanel.SetActive(false);
            ResetHarvest();
        }
    }

    private void CompleteHarvest()
    {
        Debug.Log("Harvest complete!");

        if (woodManager != null)
        {
            var woodScript = woodManager.GetComponent<amountManager>();
            if (woodScript != null)
            {
                woodScript.AddWood(3);
            }
        }

        if (currentTree != null)
        {
            Destroy(currentTree);
        }

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
            // Check if the first hotbar slot is selected before starting harvesting
            var hotbarScript = hotbarManager.GetComponent<hotbar>(); 
            if (hotbarScript != null && hotbarScript.GetCurrentSlot() == 1)
            {
                isHarvesting = true;
            }
            else
            {
                Debug.Log("You need to have Slot 1 selected to harvest!");
            }
        }
    }
}
