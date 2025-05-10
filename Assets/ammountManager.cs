using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class amountManager : MonoBehaviour
{
    private int woodCount = 0;
    private int stoneCount = 0;

    public TextMeshProUGUI woodText;
    public Image woodImage;
    public TextMeshProUGUI stoneText;
    public Image stoneImage;

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

    public void AddStone(int amount)
    {
        stoneCount += amount;
        UpdateUI();
    }

    public void RemoveStone(int amount)
    {
        stoneCount -= amount;
        if (stoneCount < 0) stoneCount = 0;
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

        if (stoneCount > 0)
        {
            stoneImage.gameObject.SetActive(true);
            stoneText.gameObject.SetActive(true);
            stoneText.text = stoneCount.ToString();
        }
        else
        {
            stoneImage.gameObject.SetActive(false);
            stoneText.gameObject.SetActive(false);
        }
    }
}
