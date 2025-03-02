using UnityEngine;

public class ToggleObjects : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;

    public void Toggle_Objects()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cube.SetActive(!cube.activeSelf);
        sphere.SetActive(!sphere.activeSelf);
    }
}
