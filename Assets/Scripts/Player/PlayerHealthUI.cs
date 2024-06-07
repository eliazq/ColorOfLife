using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthBarImage; // 0.625 max fill amount
    [SerializeField] private Image iconImage;
    [SerializeField] private Sprite deadIcon;
    private Sprite iconSprite;
    private float maxFillAmount = 0.625f;
    [SerializeField] private Volume globalVolume;

    private void Start()
    {
        iconSprite = iconImage.sprite;
        Player.Instance.OnDamageTaken += Instance_OnDamageTaken;
        Player.Instance.OnDead += Instance_OnDead;
        Player.Instance.OnDamageGiven += Instance_OnDamageGiven;
        globalVolume = FindAnyObjectByType<Volume>();
    }

    private void Instance_OnDamageGiven(object sender, System.EventArgs e)
    {
        UpdateUI();
        iconImage.sprite = iconSprite;
    }

    private void Instance_OnDead(object sender, System.EventArgs e)
    {
        iconImage.sprite = deadIcon;
    }

    private void Instance_OnDamageTaken(object sender, System.EventArgs e)
    {
        UpdateUI();
        StartCoroutine(RedScreenVisual());
    }
    

    // TODO DONT DO IT IN HERE; NOT IN THIS CLASS!
    IEnumerator RedScreenVisual()
    {
        UnityEngine.Rendering.Universal.Vignette vignette;
        if (globalVolume.profile.TryGet(out vignette))
        {
            vignette.active = true;
        }
        yield return new WaitForSeconds(1.2f);
        if (vignette != null)
            vignette.active = false;
        else Debug.LogError("NO VIGNETTE FROM PROFILE");
    }

    private void UpdateUI()
    {
        float healthPercentage = (float)Player.Instance.health / Player.Instance.maxHealth;
        healthBarImage.fillAmount = healthPercentage * maxFillAmount;
    }
}
