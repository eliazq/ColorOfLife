using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;


public class MainButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] float changingTime = 1f;
    [SerializeField] Color glowColor = Color.cyan;
    [SerializeField] float glowPower = 0.5f;
    [SerializeField] float glowOuter = 0.5f;

    private float oldAlpha;
    private Coroutine alphaCoroutine;

    private void Start()
    {
        oldAlpha = buttonText.color.a;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var material = buttonText.fontMaterial;
        material.EnableKeyword("GLOW_ON");
        material.SetColor("_GlowColor", glowColor);
        material.SetFloat("_GlowPower", glowPower);
        material.SetFloat("_GlowOuter", glowOuter);
        buttonText.UpdateMeshPadding();

        if (alphaCoroutine != null)
        {
            StopCoroutine(alphaCoroutine);
        }
        alphaCoroutine = StartCoroutine(ChangeTextAlphaOverTime(1f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var material = buttonText.fontMaterial;
        material.DisableKeyword("GLOW_ON");
        buttonText.UpdateMeshPadding();

        if (alphaCoroutine != null)
        {
            StopCoroutine(alphaCoroutine);
        }
        alphaCoroutine = StartCoroutine(ChangeTextAlphaOverTime(oldAlpha));
    }

    private IEnumerator ChangeTextAlphaOverTime(float targetAlpha)
    {
        float startAlpha = buttonText.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < changingTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / changingTime);
            buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, newAlpha);
            yield return null;
        }

        // Ensure the final alpha value is set
        buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, targetAlpha);
    }
}
