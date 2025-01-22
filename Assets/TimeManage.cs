using System;
using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Texture2D skyboxNight;
    [SerializeField] private Texture2D skyboxSunrise;
    [SerializeField] private Texture2D skyboxDay;
    [SerializeField] private Texture2D skyboxSunset;

    [SerializeField] private Gradient graddientNightToSunrise;
    [SerializeField] private Gradient graddientSunriseToDay;
    [SerializeField] private Gradient graddientDayToSunset;
    [SerializeField] private Gradient graddientSunsetToNight;

    [SerializeField] private Light globalLight;

    public int minutes;
    public int hours = 0; // Start at 0 hours (nighttime).
    public int days;

    public float tempSecond;
    private bool isBlending = false;

    public void Start()
    {
        // Initialize the skybox and fog for nighttime.
        InitializeSkyboxAndFog();
    }

    public void Update()
    {
        tempSecond += Time.deltaTime;

        if (tempSecond >= 1)
        {
            Minutes += 1;
            tempSecond = 0;
        }
    }

    public int Minutes
    {
        get { return minutes; }
        set
        {
            minutes = value;
            OnMinutesChange(value);
        }
    }

    public int Hours
    {
        get { return hours; }
        set
        {
            hours = value;
            OnHoursChange(value);
        }
    }

    public int Days
    {
        get { return days; }
        set { days = value; }
    }

    private void InitializeSkyboxAndFog()
    {
        // Start the game at night.
        SetSkyboxInstant(skyboxNight, graddientSunsetToNight.Evaluate(1));
    }

    private void SetSkyboxInstant(Texture2D skybox, Color fogColor)
    {
        RenderSettings.skybox.SetTexture("_Texture1", skybox);
        RenderSettings.skybox.SetFloat("_Blend", 0); // Ensure no blending initially.
        RenderSettings.fogColor = fogColor;
        globalLight.color = fogColor;
    }

    private void OnMinutesChange(int value)
    {
        globalLight.transform.Rotate(Vector3.up, (1f / (1440f / 4f)) * 360f, Space.World);

        if (!isBlending) // Prevent overlapping blends.
        {
            if (value == 5)
            {
                Debug.Log("Blending from Night to Sunrise.");
                StartCoroutine(BlendSkybox(skyboxNight, skyboxSunrise, graddientNightToSunrise, 5f));
            }
            else if (value == 15)
            {
                Debug.Log("Blending from Sunrise to Day.");
                StartCoroutine(BlendSkybox(skyboxSunrise, skyboxDay, graddientSunriseToDay, 5f));
            }
            else if (value == 25)
            {
                Debug.Log("Blending from Day to Sunset.");
                StartCoroutine(BlendSkybox(skyboxDay, skyboxSunset, graddientDayToSunset, 5f));
            }
            else if (value == 35)
            {
                Debug.Log("Blending from Sunset to Night.");
                StartCoroutine(BlendSkybox(skyboxSunset, skyboxNight, graddientSunsetToNight, 5f));
            }
        }

        if (value >= 60)
        {
            Hours++;
            minutes = 0;
        }

        if (Hours >= 24)
        {
            Hours = 0;
            Days++;
        }
    }

    private void OnHoursChange(int value)
    {
        Debug.Log($"Hour changed to: {value}");
    }

    private IEnumerator BlendSkybox(Texture2D a, Texture2D b, Gradient lightGradient, float time)
    {
        isBlending = true;

        // Assign textures explicitly for proper blending.
        RenderSettings.skybox.SetTexture("_Texture1", a);
        RenderSettings.skybox.SetTexture("_Texture2", b);
        RenderSettings.skybox.SetFloat("_Blend", 0); // Start blending from 0.

        // Smoothly blend between textures over time.
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            float progress = i / time;
            RenderSettings.skybox.SetFloat("_Blend", progress);

            // Update fog and light colors based on the gradient.
            Color blendedColor = lightGradient.Evaluate(progress);
            globalLight.color = blendedColor;
            RenderSettings.fogColor = blendedColor;

            Debug.Log($"Blending Skybox: Progress = {progress}");
            yield return null;
        }

        // Finalize blending by setting Texture1 to the new texture and locking Blend to 1.
        RenderSettings.skybox.SetTexture("_Texture1", b);
        RenderSettings.skybox.SetFloat("_Blend", 1);
        globalLight.color = lightGradient.Evaluate(1);
        RenderSettings.fogColor = lightGradient.Evaluate(1);

        Debug.Log("Skybox blending complete.");
        isBlending = false;
    }
}
