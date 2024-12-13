using UnityEngine;
using UnityEngine.UI;

public class open_world1 : MonoBehaviour
{
    // Assign these in the Unity Inspector
    public Canvas currentCanvas; // The canvas currently displayed
    public Canvas targetCanvas;  // The canvas to open

    // This function will be called when the button is pressed
    public void SwitchCanvas()
    {
        if (currentCanvas != null)
        {
            currentCanvas.gameObject.SetActive(false); // Deactivate the current canvas
        }

        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(true);  // Activate the target canvas
        }
    }
}