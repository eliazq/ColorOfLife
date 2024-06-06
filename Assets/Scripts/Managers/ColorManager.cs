using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance;
    public event EventHandler OnColorsAchieved;
    [SerializeField] VolumeProfile volumeProfile;
    private ColorAdjustments colorAdjustment;
    private int colorAddValue = 50;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    private void Start()
    {
        if (volumeProfile == null) volumeProfile = FindObjectOfType<Volume>().profile;

        volumeProfile.TryGet<ColorAdjustments>(out ColorAdjustments colorAdjustments);
        this.colorAdjustment = colorAdjustments;
    }

    public void AddColor()
    {
        colorAdjustment.saturation.value += colorAddValue;
        if (colorAdjustment.saturation.value >= 100)
        {
            OnColorsAchieved?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnApplicationQuit()
    {
        colorAdjustment.saturation.value = 0;
    }
    
}
