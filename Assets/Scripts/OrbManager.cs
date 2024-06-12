using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
    public static OrbManager Instance;
    [SerializeField] private GameObject[] orbObjects;
    public bool IsCollected
    {
        get
        {
            bool isCollected = true;
            foreach (GameObject obj in orbObjects)
            {
                if (obj.activeSelf)
                {
                    isCollected = false;
                    break;
                }
            }
            return isCollected;
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (Player.Instance != null)
            Player.Instance.OnPlayerLivesDead += Instance_OnPlayerLivesDead;
        else StartCoroutine(SetPlayerEvent());
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

    private void Instance_OnPlayerLivesDead(object sender, System.EventArgs e)
    {
        
    }
}
