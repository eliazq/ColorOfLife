using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float touchForce = 300f;
    [SerializeField] private float lifeLenghtAfterTouch = 8f;
    bool hasFell = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player) && !hasFell)
        {
            hasFell = true;
            rb.isKinematic = false;
            rb.AddForce(Vector3.down * touchForce);
            Destroy(gameObject, lifeLenghtAfterTouch);
        }
    }

}
