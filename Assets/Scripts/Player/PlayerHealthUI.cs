using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthBarImage; // 0.625 max fill amount
    [SerializeField] private Image iconImage;
    [SerializeField] private Sprite deadIcon;
    private Sprite iconSprite;
    private float maxFillAmount = 0.625f;

    private void Start()
    {
        iconSprite = iconImage.sprite;
        Player.Instance.OnDamageTaken += Instance_OnDamageTaken;
        Player.Instance.OnDead += Instance_OnDead;
        Player.Instance.OnDamageGiven += Instance_OnDamageGiven;
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
    }

    private void UpdateUI()
    {
        float healthPercentage = (float)Player.Instance.health / Player.Instance.maxHealth;
        healthBarImage.fillAmount = healthPercentage * maxFillAmount;
    }
}
