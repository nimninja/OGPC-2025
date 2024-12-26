using UnityEngine;

public class RotateTexture : MonoBehaviour
{
    public Renderer objectRenderer;
    public float rotationAngle;

    void Start()
    {
        if (!objectRenderer)
            objectRenderer = GetComponent<Renderer>();

        // Rotates the texture by modifying the UVs
        objectRenderer.material.mainTextureOffset = new Vector2(0, 0);
        objectRenderer.material.mainTextureScale = new Vector2(1, 1);
        // Set the rotation here in shader or by UV adjustment
    }
}
