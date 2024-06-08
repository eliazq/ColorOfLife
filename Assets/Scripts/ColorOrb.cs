using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOrb : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            SoundManager.PlaySound(SoundManager.Sound.OrbCollect,0.5f );
            ColorManager.Instance.AddColor();
            Destroy(gameObject);
        }
    }
}
