using UnityEngine;

public class TreeDetector : MonoBehaviour
{
    // This method is called when another collider enters the trigger collider
    void Start()
    {
        Debug.Log("Script is active");
    }


    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object has the tag "Tree"
       
        if (other.CompareTag("Tree"))
        {
            Debug.Log("You found a tree");
        }
    }
}
