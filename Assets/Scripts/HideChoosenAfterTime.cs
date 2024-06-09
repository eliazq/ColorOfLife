using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideChoosenAfterTime : MonoBehaviour
{
    [SerializeField] private GameObject choosenObject;
    [SerializeField] private float time = 8f;
    [SerializeField] private bool onlyHide = false;
    void Start()
    {
        StartCoroutine(HideAfterTimeCoroutine());
    }

    IEnumerator HideAfterTimeCoroutine()
    {
        yield return new WaitForSeconds(time);
        if (!onlyHide)
            StartCoroutine(ShowAfterTime());
        choosenObject.SetActive(false);
    }

    IEnumerator ShowAfterTime()
    {
        yield return new WaitForSeconds(time);
        choosenObject.SetActive(true);
    }
}
