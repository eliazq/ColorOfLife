using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthBarImage; // 0.625 max fill amount
    private float maxFillAmount = 0.625f;

    private void Update()
    {
        float healthPercentage = (float)Player.Instance.health / Player.Instance.maxHealth;
        healthBarImage.fillAmount = healthPercentage * maxFillAmount;
    }
}
