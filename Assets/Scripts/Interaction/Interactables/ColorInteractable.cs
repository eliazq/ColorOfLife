using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform interactUIHolder;

    public string GetInteractText()
    {
        return "Take Color";
    }

    public Transform GetInteractUITransform()
    {
        return interactUIHolder;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransform)
    {
        ColorManager.Instance.AddColor();
        Destroy(gameObject);
    }

}
