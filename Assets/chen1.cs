using UnityEngine;

public class chen1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object has the tag "Tree"
        Debug.Log("You found a tree");
        
    }
}
