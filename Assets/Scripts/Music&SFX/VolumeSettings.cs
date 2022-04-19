using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    public const string MixerMusic = "MusicVolume";
    public const string MixerSFX = "SFXVolume";


    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MusicKey, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFXKey, 1f);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MusicKey, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFXKey, sfxSlider.value);
    }

    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MixerMusic, Mathf.Log10(value) * 20);

        SaveManager.instance.Save();
    }

    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MixerSFX, Mathf.Log10(value) * 20);
    }
}
