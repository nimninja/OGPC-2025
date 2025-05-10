using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFlasher : MonoBehaviour
{
    [Header("Flash Settings")]
    public Image flashImage;
    public float flashDuration = 0.5f;
    public Color flashColor = Color.white;

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        float halfDuration = flashDuration / 2f;
        Color transparent = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);
        Color opaque = new Color(flashColor.r, flashColor.g, flashColor.b, 1f);

        flashImage.color = transparent;

        // Fade in
        for (float t = 0; t < halfDuration; t += Time.deltaTime)
        {
            flashImage.color = Color.Lerp(transparent, opaque, t / halfDuration);
            yield return null;
        }

        flashImage.color = opaque;

        // Fade out
        for (float t = 0; t < halfDuration; t += Time.deltaTime)
        {
            flashImage.color = Color.Lerp(opaque, transparent, t / halfDuration);
            yield return null;
        }

        flashImage.color = transparent;
    }

    // 🔁 Auto flash randomly between 0.5 and 1 seconds
    void Start()
    {
        StartCoroutine(AutoFlashLoop());
    }

    private IEnumerator AutoFlashLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            Flash();
        }
    }
}
