using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActiveAfterTimeline : MonoBehaviour
{
    PlayableDirector playableDir;
    [SerializeField] private GameObject player;

    private void Start()
    {
        playableDir = GetComponent<PlayableDirector>();

        playableDir.paused += PlayableDir_stopped;
        playableDir.stopped += PlayableDir_stopped;
        
    }

    private void PlayableDir_stopped(PlayableDirector obj)
    {
        Debug.Log("I set it active here");
        player.SetActive(true);
        player.gameObject.SetActive(true);
    }
}
