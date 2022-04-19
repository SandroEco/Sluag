using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource footstepSource;
    [SerializeField] List<AudioClip> footstepClips = new List<AudioClip>();

    public static AudioManager instance;

    public const string MusicKey = "musicVolume";
    public const string SFXKey = "sfxVolume";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadVolume();
    }

    public void FootstepSFX()
    {
        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Count)];

        footstepSource.PlayOneShot(clip);
    }

    void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MusicKey, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFXKey, 1f);

        mixer.SetFloat(VolumeSettings.MixerMusic, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MixerSFX, Mathf.Log10(sfxVolume) * 20);

    }
}
