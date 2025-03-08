using UnityEngine;
using TMPro;
using System.Collections;

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
    private bool isNear = false;
    private bool isTalking = false;
    private Coroutine flashCoroutine;

    private void Start()
    {
        if (textComponent != null)
        {
            textComponent.text = ""; // Start with no text
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        bool wasNear = isNear;
        isNear = distance <= triggerRadius;

        if (isNear && !isTalking)
        {
            if (!wasNear) // Start flashing only when entering range
            {
                flashCoroutine = StartCoroutine(FlashPressT());
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                if (flashCoroutine != null)
                {
                    StopCoroutine(flashCoroutine);
                }
                textComponent.text = dialogues[0]; // Show first dialogue
                isTalking = true;
                dialogueIndex = 0;
            }
        }
        else if (!isNear)
        {
            ResetDialogue(); // Hide text and reset dialogue when leaving
        }

        if (isTalking && Input.GetKeyDown(KeyCode.T))
        {
            NextDialogue();
        }
    }

    private void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogues.Length)
        {
            textComponent.text = dialogues[dialogueIndex]; // Show next dialogue
        }
        else
        {
            ResetDialogue(); // Hide text when all dialogues are done
        }
    }

    private void ResetDialogue()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        textComponent.text = ""; // Hide text
        isTalking = false;
        dialogueIndex = 0; // Reset dialogue index
    }

    private IEnumerator FlashPressT()
    {
        while (!isTalking)
        {
            textComponent.text = "Press T to talk";
            yield return new WaitForSeconds(0.5f);
            textComponent.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
