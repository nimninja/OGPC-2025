using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Make the NPC disappear
            Destroy(gameObject);
        }
    }
}
