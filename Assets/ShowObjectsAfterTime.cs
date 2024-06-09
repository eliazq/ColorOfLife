using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObjectsAfterTime : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToShow;
    [SerializeField] private float timeInSeconds = 36.3f;

    private void Start()
    {
        StartCoroutine(ShowAllObjects());
    }

    IEnumerator ShowAllObjects()
    {
        yield return new WaitForSeconds(timeInSeconds);
        foreach (GameObject obj in objectsToShow)
        {
            obj.SetActive(true);
        }
    }
}
