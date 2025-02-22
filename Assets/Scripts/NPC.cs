using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Assign the TextMeshPro text field
    public Transform player;
    public float triggerRadius = 5f;
    
    private string[] dialogues = {
        "Hello, traveler!",
        "The world is full of secrets.",
        "Beware of the dangers ahead...",
        "Good luck on your journey!"
    };
    
    private int dialogueIndex = 0;

    private void Start()
    {
        if (textComponent != null)
        {
            textComponent.text = ""; // Start with an empty text
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        bool isActive = distance <= triggerRadius;

        // Only show text when within range
        if (isActive)
        {
            if (textComponent != null)
            {
                textComponent.text = dialogues[dialogueIndex]; // Update text
            }

            if (Input.GetKeyDown(KeyCode.T)) // If player is close and presses T
            {
                NextDialogue();
            }
        }
        else
        {
            if (textComponent != null)
            {
                textComponent.text = ""; // Hide text when out of range
            }
        }
    }

    private void NextDialogue()
    {
        dialogueIndex = (dialogueIndex + 1) % dialogues.Length; // Loop through dialogues
        if (textComponent != null)
        {
            textComponent.text = dialogues[dialogueIndex]; // Update text
        }
    }
}