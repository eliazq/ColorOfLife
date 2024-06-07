using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDelay : MonoBehaviour
{
    public GameObject objectToActivate;  // The object you want to activate
    public float delay = 5f;             // Delay in seconds

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to activate the object after the delay
        StartCoroutine(ActivateObjectAfterDelay());
    }

    IEnumerator ActivateObjectAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Activate the object
        objectToActivate.SetActive(true);
    }
}
