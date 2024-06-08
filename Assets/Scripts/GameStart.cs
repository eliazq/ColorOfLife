using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameStart : MonoBehaviour
{

    private void Start(){
        GlobalVolumeManager.Instance.ChangeSaturation(-100);
    }
}
