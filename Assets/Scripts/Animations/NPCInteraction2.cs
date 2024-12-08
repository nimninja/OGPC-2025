using UnityEngine;

public class NPCInteraction2 : MonoBehaviour
{
    private bool isPlayerNearby = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; 
        }
    }

    private void Update()
    {
        
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.T))
        {
            
            Destroy(gameObject);
        }
    }
}
