using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerTest : MonoBehaviour
{
    private void Start()
    {
        SoundManager.PlaySound(SoundManager.Sound.TestSound, 0.4f);
    }
}
