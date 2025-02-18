using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class treeHarvest : MonoBehaviour
{
    public GameObject axePanel;
    public Image axeFill;
    public Image axeSilhouette;
    public TextMeshProUGUI harvestText;
    public GameObject woodManager;
    public GameObject hotbarManager;

    public float harvestTime = 3f;
    private float holdTimer = 0f;
    private bool isHarvesting = false;
    private GameObject currentTree;

    void Update()
    {
        if (currentTree != null)  // Only check input if player is near a tree
        {
            var hotbarScript = hotbarManager.GetComponent<hotbar>();
            bool correctSlotSelected = (hotbarScript != null && hotbarScript.GetCurrentSlot() == 1);

            if (correctSlotSelected && Input.GetKeyDown(KeyCode.H))
            {
                isHarvesting = true; // Now always starts on first key press
            }

            if (isHarvesting && Input.GetKey(KeyCode.H))
            {
                holdTimer += Time.deltaTime;
                axeFill.fillAmount = holdTimer / harvestTime;

                if (holdTimer >= harvestTime)
                {
                    CompleteHarvest();
                }
            }

            if (isHarvesting && Input.GetKeyUp(KeyCode.H))
            {
                ResetHarvest();
            }
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
            woodScript?.AddWood(3);
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
}
