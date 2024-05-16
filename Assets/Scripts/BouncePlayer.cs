using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlayer : MonoBehaviour
{
    [SerializeField] private float bounceForce = 50f;
    [SerializeField] private int damage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            player.Damage(damage);
            player.GetComponent<vThirdPersonController>().Jump();
            rb.AddForce(Vector3.up * 10f);
            rb.AddExplosionForce(bounceForce, transform.position, 10f);
        }
    }

}
