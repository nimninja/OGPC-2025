using UnityEngine;
using UnityEngine.UI;

public class FillProgress : MonoBehaviour
{
    public Image fillImage; // The UI Image to animate
    public float fillDuration = 3f; // Duration to fill completely (3 seconds)

    private float holdTimer = 0f;

    void Start()
    {
        // Ensure the fill starts at 0%
        if (fillImage != null)
        {
            fillImage.fillAmount = 0f;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            // Increment timer and set fill amount
            holdTimer += Time.deltaTime;
            fillImage.fillAmount = Mathf.Clamp01(holdTimer / fillDuration);
        }
        else if (Input.GetKeyUp(KeyCode.H))
        {
            // Reset timer and fill when H is released
            holdTimer = 0f;
            fillImage.fillAmount = 0f;
        }
    }
}
