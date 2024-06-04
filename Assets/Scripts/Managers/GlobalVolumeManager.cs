using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolumeManager : MonoBehaviour
{
    public static GlobalVolumeManager Instance;

    [SerializeField] private VolumeProfile volumeProfile;
    private VolumeProfile defaultProfile;
    public bool IsChangingVolume {  get; private set; }
    float volumeSaturation
    {
        get
        {
            if (volumeProfile.TryGet(out ColorAdjustments colorAdjustments))
            {
                return colorAdjustments.saturation.value;
            }
            Debug.LogError($"volume {volumeProfile.name} does not have ColorAdjustments Component");
            return -1f;
        }
        set {
            if (volumeProfile.TryGet(out ColorAdjustments colorAdjustments))
            {
                colorAdjustments.saturation.value = value;
            }
            else
                Debug.LogError($"volume {volumeProfile.name} does not have ColorAdjustments Component");
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        if (volumeProfile == null)
            volumeProfile = FindObjectOfType<Volume>().profile;
        
        defaultProfile = ScriptableObject.CreateInstance<VolumeProfile>();

        foreach (var component in volumeProfile.components)
        {
            // Create a new instance of each component
            var newComponent = Instantiate(component);
            defaultProfile.components.Add(newComponent);
        }
    }


    public void ChangeSaturation(float targetValue, float speed = 35)
    {
        StartCoroutine(ChangeSaturationSmooth(targetValue, speed));
    }

    IEnumerator ChangeSaturationSmooth(float targetSaturation, float speed)
    {
        IsChangingVolume = true;

        while (volumeSaturation != targetSaturation)
        {
            volumeSaturation = Mathf.MoveTowards(volumeSaturation, targetSaturation, speed * Time.deltaTime);
            yield return null;
        }

        IsChangingVolume = false;

    }

    public void ResetProfile()
    {
        volumeProfile.components.Clear();

        foreach (var component in defaultProfile.components)
        {
            // Create a new instance of each component
            var newComponent = Instantiate(component);
            volumeProfile.components.Add(newComponent);
        }
        
    }


}
