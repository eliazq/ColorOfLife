using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    private Transform spawnTransform;

    private void Start()
    {
        spawnTransform = transform.GetChild(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            GameObject spawnedObj = Instantiate(prefabToSpawn, spawnTransform.position, Quaternion.identity);
            Destroy(spawnedObj, 8f);
        }
    }

    private void OnDrawGizmos()
    {
        if (spawnTransform == null)
            spawnTransform = transform.GetChild(0);
        Gizmos.color = Color.grey;
        Gizmos.DrawSphere(spawnTransform.position, .5f);
    }
}
