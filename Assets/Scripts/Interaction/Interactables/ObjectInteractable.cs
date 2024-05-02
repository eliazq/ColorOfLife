using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform interactTextHolder;
    public string GetInteractText()
    {
        return "Pick Up";
    }

    public Transform GetInteractUITransform()
    {
        return interactTextHolder;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransform)
    {
        Debug.Log("Picked Up");
    }

}
