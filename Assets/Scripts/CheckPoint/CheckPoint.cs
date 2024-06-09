using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject notCollected;
    [SerializeField] private GameObject collected;
    
    private void Start()
    {
        if (Player.Instance != null)
            Player.Instance.OnPlayerLivesDead += Instance_OnPlayerLivesDead;
        else StartCoroutine(SetPlayerEvent());
    }

    private void Instance_OnPlayerLivesDead(object sender, EventArgs e)
    {
        notCollected.SetActive(true);
        collected.SetActive(false);       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Player.Instance.spawnPosition = transform.position;
            Player.Instance.transform.rotation = Quaternion.identity;
            notCollected.SetActive(false);
            collected.SetActive(true);
            SoundManager.PlaySound(SoundManager.Sound.CheckPoint);
        }
    }

    IEnumerator SetPlayerEvent()
    {
        while (true)
        {
            if (Player.Instance != null)
            {
                Player.Instance.OnPlayerLivesDead += Instance_OnPlayerLivesDead;
                break;
            }
            yield return null;
        }
    }
    
}
