using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    private void Start()
    {
        if (resolutionDropdown == null)
        {
            Debug.LogError("Resolution Dropdown is not set.");
            return;
        }

        resolutionDropdown.ClearOptions();

        resolutions = Screen.resolutions;

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + (int)resolutions[i].refreshRateRatio.value + "hz";
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height &&
                Mathf.Approximately((float)resolutions[i].refreshRateRatio.value, (float)Screen.currentResolution.refreshRateRatio.value))
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume(float newVolume)
    {
        if (audioMixer == null)
        {
            Debug.LogError("Audio Mixer is not set.");
            return;
        }

        audioMixer.SetFloat("volume", newVolume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutions == null || resolutions.Length == 0)
        {
            Debug.LogError("Resolutions array is not initialized.");
            return;
        }

        if (resolutionIndex < 0 || resolutionIndex >= resolutions.Length)
        {
            Debug.LogError("Resolution index is out of range.");
            return;
        }

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}