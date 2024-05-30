using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    private static readonly object _lock = new object();

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        SoundManager prefab = Resources.Load<SoundManager>("MainGameTools");

                        if (prefab == null)
                        {
                            Debug.LogError("MainGameTools prefab not found in Resources!");
                        }
                        else
                        {
                            _instance = Instantiate(prefab);
                            DontDestroyOnLoad(_instance.gameObject);
                        }
                    }
                }
            }
            return _instance;
        }
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public Sound sound;
        public AudioClip audioClip;
    }

    public SoundClipsSO soundAudioClipsSO;

    public enum Sound
    {
        TestSound,
        MaxPlayable,
        // Add more sounds here
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize(); // Ensure Initialize is called
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        foreach (Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            soundTimerDictionary[sound] = 0f;
        }
    }

    public static void PlaySound(Sound sound, Vector3 position)
    {
        Debug.Log($"Attempting to play sound: {sound} at position: {position}");
        if (CanPlaySound(sound))
        {
            AudioClip audioClip = GetAudioClip(sound);
            if (audioClip != null)
            {
                Debug.Log($"Playing sound: {sound} at position: {position}");
                AudioSource.PlayClipAtPoint(audioClip, position, 1f);
            }
            else
            {
                Debug.LogError($"AudioClip for sound: {sound} is null");
            }
        }
        else
        {
            Debug.Log($"Cannot play sound: {sound} due to timing restrictions");
        }
    }

    public static void PlaySound(Sound sound)
    {
        Debug.Log($"Attempting to play sound: {sound} as one-shot");
        if (CanPlaySound(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                DontDestroyOnLoad(oneShotGameObject);
            }
            AudioClip audioClip = GetAudioClip(sound);
            if (audioClip != null)
            {
                Debug.Log($"Playing sound: {sound} as one-shot");
                oneShotAudioSource.PlayOneShot(audioClip);
            }
            else
            {
                Debug.LogError($"AudioClip for sound: {sound} is null");
            }
        }
        else
        {
            Debug.Log($"Cannot play sound: {sound} due to timing restrictions");
        }
    }
    public static void PlaySound(Sound sound, float volume)
    {
        Debug.Log($"Attempting to play sound: {sound} as one-shot");
        if (CanPlaySound(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                oneShotAudioSource.volume = volume;
                DontDestroyOnLoad(oneShotGameObject);
            }
            AudioClip audioClip = GetAudioClip(sound);
            if (audioClip != null)
            {
                Debug.Log($"Playing sound: {sound} as one-shot");
                oneShotAudioSource.PlayOneShot(audioClip);
            }
            else
            {
                Debug.LogError($"AudioClip for sound: {sound} is null");
            }
        }
        else
        {
            Debug.Log($"Cannot play sound: {sound} due to timing restrictions");
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        if (soundTimerDictionary == null)
        {
            Initialize(); // Ensure the dictionary is initialized
        }

        if (soundTimerDictionary.ContainsKey(sound) && sound == Sound.MaxPlayable)
        {
            float lastTimePlayed = soundTimerDictionary[sound];
            float cooldownTime = 0.15f; // Adjust this value as needed
            float timeSinceLastPlayed = Time.time - lastTimePlayed;
            Debug.Log($"Time since last played {sound}: {timeSinceLastPlayed}s, Cooldown time: {cooldownTime}s");

            if (timeSinceLastPlayed >= cooldownTime)
            {
                soundTimerDictionary[sound] = Time.time;
                Debug.Log($"Sound {sound} can be played. Updating last played time.");
                return true;
            }
            else
            {
                Debug.Log($"Sound {sound} cannot be played. Cooldown active.");
                return false;
            }
        }
        else
        {
            Debug.Log($"Sound {sound} not found in dictionary. Adding to dictionary and allowing play.");
            soundTimerDictionary[sound] = Time.time;
            return true;
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAudioClip soundAudioClip in Instance.soundAudioClipsSO.soundAudioClips)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
}