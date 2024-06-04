using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeTester : MonoBehaviour
{

    [NaughtyAttributes.Button]
    public void ChangeToBlackNWhite()
    {
        GlobalVolumeManager.Instance.ChangeSaturation(-100);
    }
    [NaughtyAttributes.Button]
    public void ChangeToDefault()
    {
        GlobalVolumeManager.Instance.ResetProfile();
    }
    [NaughtyAttributes.Button]
    public void ChangeToHighColor()
    {
        GlobalVolumeManager.Instance.ChangeSaturation(100);
    }
}
