using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public CanvasGroup canvasGroup; // Attach the CanvasGroup component to the canvas
    public float delay = 2f; // Delay in seconds before starting the fade
    public float fadeDuration = 3f; // Duration of the fade effect in seconds

    void Start()
    {
        // Ensure the CanvasGroup component is attached to the canvas
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        // Set initial opacity to 0 (fully transparent)
        canvasGroup.alpha = 0;

        // Start the coroutine to fade in the canvas after the delay
        StartCoroutine(FadeInCanvas());
    }

    IEnumerator FadeInCanvas()
    {
        // Wait for the delay
        yield return new WaitForSeconds(delay);

        // Calculate the fade step based on the duration
        float fadeStep = 1f / fadeDuration;
        float currentOpacity = canvasGroup.alpha;

        // Gradually increase the opacity
        while (currentOpacity < 1)
        {
            currentOpacity += fadeStep * Time.deltaTime;
            canvasGroup.alpha = currentOpacity;
            yield return null;
        }

        // Ensure the opacity is set to 1 at the end
        canvasGroup.alpha = 1;
    }
}
