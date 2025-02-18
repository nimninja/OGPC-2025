using UnityEngine;
using UnityEngine.UI;

public class toggleInventory : MonoBehaviour
{
    public GameObject inventoryImage;
    public Image map;

    // Start is called before the first frame update
    void Start()
    {

        if (inventoryImage != null)
        {
            inventoryImage.gameObject.SetActive(false);
            map.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventoryImage != null)
            {
                inventoryImage.gameObject.SetActive(!inventoryImage.gameObject.activeSelf);
                map.gameObject.SetActive(!map.gameObject.activeSelf);
            }
        }
    }
}
