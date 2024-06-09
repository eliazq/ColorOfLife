using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCocaine : MonoBehaviour
{

    [NaughtyAttributes.Button]
    public void TakeCoke()
    {
        Time.timeScale *= 2;
    }

    [NaughtyAttributes.Button]
    public void SipLean()
    {
        Time.timeScale /= 2;
    }
}
