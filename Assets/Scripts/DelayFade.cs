using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayFade : MonoBehaviour
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

        // Start the coroutine to fade out the canvas after the delay
        StartCoroutine(FadeOutCanvas());
    }

    IEnumerator FadeOutCanvas()
    {
        // Wait for the delay
        yield return new WaitForSeconds(delay);

        // Calculate the fade step based on the duration
        float fadeStep = 1f / fadeDuration;
        float currentOpacity = canvasGroup.alpha;

        // Gradually reduce the opacity
        while (currentOpacity > 0)
        {
            currentOpacity -= fadeStep * Time.deltaTime;
            canvasGroup.alpha = currentOpacity;
            yield return null;
        }

        // Ensure the opacity is set to 0 at the end
        canvasGroup.alpha = 0;
    }
}
