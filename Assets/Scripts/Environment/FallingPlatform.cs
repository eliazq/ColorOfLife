using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float touchForce = 300f;
    [SerializeField] private float lifeLenghtAfterTouch = 8f;
    [SerializeField] private ForceMode forceMode = ForceMode.Impulse;
    [SerializeField] private float movingBackTime = 4f;
    bool hasFell = false;
    private Vector3 startPosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        startPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player) && !hasFell)
        {
            hasFell = true;
            rb.isKinematic = false;
            rb.AddForce(Vector3.down * touchForce, forceMode);
            StartCoroutine(ResetPlatformAfterTime(lifeLenghtAfterTouch));
        }
    }

    IEnumerator ResetPlatformAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        hasFell = false;

        while (transform.position != startPosition)
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * movingBackTime);
            yield return null;
        }
        transform.position = startPosition;
    }

}
