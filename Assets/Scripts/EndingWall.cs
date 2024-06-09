using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingWall : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            StartCoroutine(LoadEndScene());
        }
    }

    IEnumerator LoadEndScene()
    {
        yield return new WaitForSeconds(0.3f);
        LevelManager.LoadLevel(LevelManager.Level.ThankYou);
    }
}
