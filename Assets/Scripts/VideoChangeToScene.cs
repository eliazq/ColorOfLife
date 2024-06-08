using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoChangeToScene : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Get the VideoPlayer component attached to the same GameObject
        videoPlayer = GetComponent<VideoPlayer>();

        // Add a listener to the loopPointReached event, which is triggered when the video ends
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // This method will be called when the video ends
    void OnVideoEnd(VideoPlayer vp)
    {
        // Load the next scene 
        SceneManager.LoadScene("MainMenu");
    }
}
