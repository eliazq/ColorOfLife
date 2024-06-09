using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAfterTime : MonoBehaviour
{
    [SerializeField] private float time = 8f;
    [SerializeField] private bool showAfterTime = true;
    void Start()
    {
        StartCoroutine(HideAfterTimeCoroutine());
    }

    IEnumerator HideAfterTimeCoroutine()
    {
        yield return new WaitForSeconds(time);
        if (showAfterTime)
            StartCoroutine(ShowAfterTime());
        gameObject.SetActive(false);
    }

    IEnumerator ShowAfterTime()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(true);
    }

}
