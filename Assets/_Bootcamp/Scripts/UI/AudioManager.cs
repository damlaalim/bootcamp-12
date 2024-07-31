using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public Slider musicSlider, sfxSlider;
    public Slider genelSesSlider;
    public AudioSource GetMusicSource => musicSource;
    public AudioSource GetEffectSource => sfxSource;

    [SerializeField] private SerializedDictionary<AudioType, AudioClip> audioClipDict;

    public void SetMusicValue(float value) => musicSource.volume = value;
    public void SetEffectValue(float value) => sfxSource.volume = value;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        musicSlider.value = 0.15f;
        musicSlider.value = musicSource.volume;
        genelSesSlider.value = AudioListener.volume;
        genelSesSlider.onValueChanged.AddListener(VolumeChanged);
    }

    private void InitializeSliders()
    {
        musicSlider = GameObject.Find("MusicSlider")?.GetComponent<Slider>();
        sfxSlider = GameObject.Find("SFXSlider")?.GetComponent<Slider>();
        genelSesSlider = GameObject.Find("GenelSesSlider")?.GetComponent<Slider>();

        if (musicSlider != null)
        {
            musicSlider.value = musicSource.volume;
            musicSlider.onValueChanged.AddListener(SetMusicValue);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxSource.volume;
            sfxSlider.onValueChanged.AddListener(SetEffectValue);
        }

        if (genelSesSlider != null)
        {
            genelSesSlider.value = AudioListener.volume;
            genelSesSlider.onValueChanged.AddListener(VolumeChanged);
        }
    }
    public void MusicVolume()
    {
        musicSource.volume = musicSlider.value;
    }

    public void SFXVolume()
    {
        sfxSource.volume = sfxSlider.value;
    }
    public void VolumeChanged(float value)
    {
        AudioListener.volume = value;
    }
    public enum AudioType
    {
        MainMenu
    }
    public void ChangeMusic(AudioType type)
    {
        ChangeSound(type, false);
    }

    public void PlayEffect(AudioType type)
    {
        ChangeSound(type, true);
    }

    private void ChangeSound(AudioType type, bool isEffect)
    {
        foreach (var (key, value) in audioClipDict)
        {
            if (key == type)
            {
                if (isEffect)
                {
                    sfxSource.Stop();
                    sfxSource.clip = value;
                    sfxSource.Play();
                }
                else
                {
                    musicSource.Stop();
                    musicSource.clip = value;
                    musicSource.Play();
                }

                break;
            }
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        InitializeSliders();
    }
}
