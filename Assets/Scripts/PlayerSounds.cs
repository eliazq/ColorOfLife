using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    vThirdPersonController vThirdPersonController;

    private void Start()
    {
        vThirdPersonController = GetComponent<vThirdPersonController>();
        vThirdPersonController.OnLandedGround += VThirdPersonController_OnLandedGround;
    }

    private void VThirdPersonController_OnLandedGround(object sender, vThirdPersonMotor.YVelocityEventArgs e)
    {
        SoundManager.Sound[] soundList = new SoundManager.Sound[] { SoundManager.Sound.LandToGround1, SoundManager.Sound.LandToGround2, SoundManager.Sound.LandToGround3, };
        int ranNum = Random.Range(0, soundList.Length);
        SoundManager.PlaySound(soundList[ranNum], Player.Instance.transform.position);
    }
}
