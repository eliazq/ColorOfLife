using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingWall : MonoBehaviour
{
    [SerializeField] private GameObject needToCollectOrbsUI;
    bool hasShownUI = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (OrbManager.Instance.IsCollected)
                StartCoroutine(LoadEndScene());
            else if (!hasShownUI)
            {
                StartCoroutine(NeedToCollectUIShow());
            }
        }
    }

    IEnumerator LoadEndScene()
    {
        yield return new WaitForSeconds(0.3f);
        LevelManager.LoadLevel(LevelManager.Level.ThankYou);
    }

    IEnumerator NeedToCollectUIShow()
    {
        needToCollectOrbsUI.SetActive(true);
        hasShownUI = true;
        yield return new WaitForSeconds(6f);
        needToCollectOrbsUI.SetActive(false);
    }
}
