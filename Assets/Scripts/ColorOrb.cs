using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOrb : MonoBehaviour
{
    bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.GetComponent<Player>() != null)
        {
            hasTriggered = true;
            GlobalVolumeManager.Instance.ChangeContrast(100f, 300);
            SoundManager.PlaySound(SoundManager.Sound.OrbCollect,0.5f );
            ColorManager.Instance.AddColor();
            GetComponent<ParticleSystem>().Stop();
            StartCoroutine(ChangeContrastBack());
        }
    }

    IEnumerator ChangeContrastBack()
    {
        while (true)
        {
            if (!GlobalVolumeManager.Instance.IsChangingVolume)
            {
                GlobalVolumeManager.Instance.ChangeContrast(0f, 300);
                break;
            }
            yield return null;
        }
        Destroy(gameObject);
    }
}
