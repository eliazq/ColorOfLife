using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastOrb : MonoBehaviour
{
    [SerializeField] private GameObject lastOrbCutSceneObject;

    private void OnTriggerEnter(Collider other)
    {
        lastOrbCutSceneObject.SetActive(true);
    }
}
