using UnityEngine;
using UnityEngine.UI;

public class hotbar : MonoBehaviour
{
    public Image[] hotbarSlots; // Array of 4 slot images
    public Color selectedColor = new Color(0.42f, 0.41f, 0.35f, 1f); // Hex: #6B6859
    public Color normalColor = new Color(0.75f, 0.71f, 0.51f, 1f);   // Hex: #BEB483

    private int currentSlot = 1; // Tracks the currently selected slot (default to slot 1)

    void Start()
    {
        UpdateHotbarUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectSlot(4);
        }
    }

    private void SelectSlot(int slotNumber)
    {
        if (slotNumber >= 1 && slotNumber <= hotbarSlots.Length)
        {
            currentSlot = slotNumber;
            UpdateHotbarUI();
        }
    }

    private void UpdateHotbarUI()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (i == currentSlot - 1)
            {
                hotbarSlots[i].color = selectedColor; // Set the selected slot color
            }
            else
            {
                hotbarSlots[i].color = normalColor; // Set non-selected slots to normal color
            }
        }
    }

    public int GetCurrentSlot()
    {
        return currentSlot;
    }
}
