using UnityEngine;
using UnityEngine.UI;
using TMPro; // Use this if you're using TextMeshPro

public class ObjectCycler : MonoBehaviour
{
    public GameObject[] objects;  // Assign Cube, Cylinder, and Sphere in the Inspector
    public Button cycleButton;    // Assign your UI Button in the Inspector
    public TextMeshProUGUI buttonText; // Drag your TextMeshProUGUI component here

    private int currentIndex = 0;

    void Start()
    {
        // Ensure only the first object is active at the start
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == currentIndex);
        }

        // Update button text
        UpdateButtonText();

        // Attach function to button click event
        cycleButton.onClick.AddListener(CycleObjects);
    }

    public void CycleObjects()
    {
        // Disable the current object
        objects[currentIndex].SetActive(false);

        // Move to the next object
        currentIndex = (currentIndex + 1) % objects.Length;

        // Enable the new object
        objects[currentIndex].SetActive(true);

        // Update button text to show the NEXT object in the cycle
        int nextIndex = (currentIndex + 1) % objects.Length;
        buttonText.text = "Purchase For X Coins:  " + objects[nextIndex].name;
    }



    void UpdateButtonText()
    {
        // Set the button text to show the NEXT object
        int nextIndex = (currentIndex + 1) % objects.Length;
        buttonText.text = "Putchase For X Coins: " + objects[nextIndex].name;
    }
}
