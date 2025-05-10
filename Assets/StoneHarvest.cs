using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class rockHarvest : MonoBehaviour
{
    public GameObject pickaxePanel;
    public Image pickaxeFill;
    public Image pickaxeSilhouette;
    public TextMeshProUGUI harvestText;
    public GameObject resourceManager;  // renamed to be generic, handles both wood and stone
    public GameObject hotbarManager;

    public float harvestTime = 3f;
    private float holdTimer = 0f;
    private bool isHarvesting = false;
    private GameObject currentRock;

void Update()
{
    var hotbarScript = hotbarManager.GetComponent<hotbar>();
    bool correctSlotSelected = (hotbarScript != null && hotbarScript.GetCurrentSlot() == 2); // Pickaxe slot

    if (currentRock != null)
    {
        if (!isHarvesting) // Not harvesting yet
        {
            if (correctSlotSelected && Input.GetKeyDown(KeyCode.H))
            {
                isHarvesting = true;
            }
        }
        else // Already harvesting
        {
            if (Input.GetKey(KeyCode.H))
            {
                holdTimer += Time.deltaTime;
                pickaxeFill.fillAmount = holdTimer / harvestTime;

                if (holdTimer >= harvestTime)
                {
                    CompleteHarvest();
                }
            }
            if (Input.GetKeyUp(KeyCode.H))
            {
                ResetHarvest();
            }
        }
    }
}


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stone"))
        {
            currentRock = other.gameObject;
            pickaxePanel.SetActive(true);
            ResetHarvest();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stone"))
        {
            currentRock = null;
            pickaxePanel.SetActive(false);
            ResetHarvest();
        }
    }

    private void CompleteHarvest()
    {
        Debug.Log("Stone harvest complete!");

        if (resourceManager != null)
        {
            var amountScript = resourceManager.GetComponent<amountManager>();
            amountScript?.AddStone(3); // New method
        }

        if (currentRock != null)
        {
            Destroy(currentRock);
        }

        ResetHarvest();
        pickaxePanel.SetActive(false);
    }

    private void ResetHarvest()
    {
        holdTimer = 0f;
        pickaxeFill.fillAmount = 0f;
        isHarvesting = false;
    }
}
