using UnityEngine;

public class NPCInteraction2 : MonoBehaviour
{
    private bool isPlayerNearby = false;

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
            // Make the NPC disappear
            Destroy(gameObject);
        }
    }
}
