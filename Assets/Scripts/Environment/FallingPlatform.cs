using System;
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
    bool isReseting = false;
    private Vector3 startPosition;
    public event EventHandler OnPlatformFall;
    public event EventHandler OnPlatformReset;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        startPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player) && !hasFell && !isReseting)
        {
            hasFell = true;
            rb.isKinematic = false;
            rb.AddForce(Vector3.down * touchForce, forceMode);
            StartCoroutine(ResetPlatformAfterTime(lifeLenghtAfterTouch));
            OnPlatformFall?.Invoke(this, EventArgs.Empty);
        }
    }

    IEnumerator ResetPlatformAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        hasFell = false;
        isReseting = true;
        Vector3.Distance(transform.position, startPosition);
        while (Vector3.Distance(transform.position, startPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * movingBackTime);
            yield return null;
        }
        isReseting = false;
        transform.position = startPosition;
        OnPlatformReset?.Invoke(this, EventArgs.Empty);
    }

}
