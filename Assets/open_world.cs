using UnityEngine;

public class CanvasRemover : MonoBehaviour
{
    public GameObject canvasToDisable;

    public void SwitchCanvas()
    {
        if (canvasToDisable != null)
        {
            canvasToDisable.SetActive(false);
        }
    }
}
