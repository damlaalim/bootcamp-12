using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _Bootcamp.Scripts.Manager
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource GetMusicSource => musicSource;
        public AudioSource GetEffectSource => effectSource;

        [SerializeField] private AudioSource musicSource, effectSource;
        [SerializeField] private SerializedDictionary<AudioType, AudioClip> audioClipDict;

        public void SetMusicValue(float value) => musicSource.volume = value;
        public void SetEffectValue(float value) => effectSource.volume = value;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                PlayEffect(AudioType.MainMenu);
            if (Input.GetKeyDown(KeyCode.A))
                PlayEffect(AudioType.MainMenu);
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
                        effectSource.Stop();
                        effectSource.clip = value;
                        effectSource.Play();
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
    }

    public enum AudioType
    {
        MainMenu
    }
}