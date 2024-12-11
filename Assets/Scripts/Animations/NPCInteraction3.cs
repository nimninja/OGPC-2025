using UnityEngine;
using TMPro; 
public class NPCInteraction3 : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public TMP_Text taskText; 

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
            
            GiveTask();
        }
    }

    private void GiveTask()
    {
        
        taskText.text = "Find the hidden treasure in the forest!";
    }
}
