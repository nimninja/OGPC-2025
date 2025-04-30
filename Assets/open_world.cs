using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Canvas canvasToRemove;  // The canvas that starts on top and will be removed

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the canvas starts on top of everything else
        if (canvasToRemove != null)
        {
            canvasToRemove.sortingOrder = 100; // Set a high sorting order to keep it on top
        }
    }

    // Call this method when the button is clicked
    public void OnButtonClick()
    {
        if (canvasToRemove != null)
        {
            // Hide the canvas when the button is clicked
            canvasToRemove.gameObject.SetActive(false);
        }
    }
}
