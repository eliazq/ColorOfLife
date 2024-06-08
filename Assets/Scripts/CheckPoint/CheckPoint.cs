using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject notCollected;
    [SerializeField] private GameObject collected;

    private void OnTriggerEnter(Collider other)
    {
        Player.Instance.spawnPosition = transform.position;
        notCollected.SetActive(false);
        collected.SetActive(true);
    }
}
