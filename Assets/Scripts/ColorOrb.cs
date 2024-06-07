using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOrb : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            SoundManager.PlaySound(SoundManager.Sound.OrbCollect, transform.position);
            ColorManager.Instance.AddColor();
            Destroy(gameObject);
        }
    }
}
