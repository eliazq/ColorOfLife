using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObjectIn : MonoBehaviour
{
    public Renderer objectRenderer; // Attach the Renderer component of the game object
    public float delay = 2f; // Delay in seconds before starting the fade
    public float fadeDuration = 3f; // Duration of the fade effect in seconds

    void Start()
    {
        // Ensure the Renderer component is attached to the game object
        if (objectRenderer == null)
        {
            objectRenderer = GetComponent<Renderer>();
        }

        // Set initial opacity to 0 (fully transparent)
        SetObjectOpacity(0);

        // Start the coroutine to fade in the game object after the delay
        StartCoroutine(FadeInObject());
    }

    IEnumerator FadeInObject()
    {
        // Wait for the delay
        yield return new WaitForSeconds(delay);

        // Calculate the fade step based on the duration
        float fadeStep = 1f / fadeDuration;
        Color objectColor = objectRenderer.material.color;
        float currentOpacity = objectColor.a;

        // Gradually increase the opacity
        while (currentOpacity < 1)
        {
            currentOpacity += fadeStep * Time.deltaTime;
            SetObjectOpacity(currentOpacity);
            yield return null;
        }

        // Ensure the opacity is set to 1 at the end
        SetObjectOpacity(1);
    }

    // Helper method to set the opacity of the game object
    void SetObjectOpacity(float opacity)
    {
        Color color = objectRenderer.material.color;
        color.a = opacity;
        objectRenderer.material.color = color;
    }
}