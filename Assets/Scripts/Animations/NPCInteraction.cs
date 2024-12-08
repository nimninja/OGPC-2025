using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            Destroy(gameObject);
        }
    }
}
