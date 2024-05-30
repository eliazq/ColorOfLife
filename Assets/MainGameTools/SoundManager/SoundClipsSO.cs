using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Empty Sound Clips Holder")]
public class SoundClipsSO : ScriptableObject
{
    public SoundManager.SoundAudioClip[] soundAudioClips;
}
