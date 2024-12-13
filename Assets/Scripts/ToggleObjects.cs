using UnityEngine;

public class ToggleObjects : MonoBehaviour
{
    public GameObject cube; // Reference to the Cube
    public GameObject sphere; // Reference to the Sphere

    public void OnButtonPress()
    {
        if (cube != null && sphere != null)
        {
            cube.SetActive(false); // Hide the Cube
            sphere.SetActive(true); // Show the Sphere
        }
    }
}
