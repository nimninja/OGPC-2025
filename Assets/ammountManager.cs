using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class amountManager : MonoBehaviour
{
    private int woodCount = 0;
    public TextMeshProUGUI woodText;
    public Image woodImage;

    void Start()
    {
        UpdateUI();
    }

    public void AddWood(int amount)
    {
        woodCount += amount;
        UpdateUI();
    }

    public void RemoveWood(int amount)
    {
        woodCount -= amount;
        if (woodCount < 0) woodCount = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (woodCount > 0)
        {
            woodImage.gameObject.SetActive(true);
            woodText.gameObject.SetActive(true);
            woodText.text = woodCount.ToString();
        }
        else
        {
            woodImage.gameObject.SetActive(false);
            woodText.gameObject.SetActive(false);
        }
    }
}
