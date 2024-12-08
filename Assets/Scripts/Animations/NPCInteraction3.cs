using UnityEngine;
using TMPro; // Required for TextMeshPro

public class NPCInteraction3 : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public TMP_Text taskText; // TextMeshPro text field

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true; // Player is in contact with the NPC
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object leaving the trigger is the player
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; // Player left the NPC's proximity
        }
    }

    private void Update()
    {
        // Check if the player is nearby and the "T" key is pressed
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.T))
        {
            // Assign a task to the player
            GiveTask();
        }
    }

    private void GiveTask()
    {
        // Example task message
        taskText.text = "Find the hidden treasure in the forest!";
    }
}
