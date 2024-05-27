using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontReceiveShadows : MonoBehaviour
{
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.receiveShadows = false;
        }
    }
}