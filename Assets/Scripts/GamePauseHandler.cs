using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GamePauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PauseGameToggle();
    }

    private void PauseGameToggle()
    {
        Time.timeScale = Time.timeScale == 0f ? 1f : Time.timeScale == 1f ? 0f : Time.timeScale;
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
    }
}
