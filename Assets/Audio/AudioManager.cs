using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace SG
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] musicSounds, sfxSounds;
        public AudioSource musicSource, sfxSource;

        public static AudioManager Instance;

        public IEnumerator playMusicSequence;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            foreach (Sound m in musicSounds)
            {
                m.source = gameObject.AddComponent<AudioSource>();
                m.source.clip = m.clip;

                m.source.volume = m.volume;
                m.source.loop = m.loop;
            }
            foreach (Sound s in sfxSounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
            }

            }

        void Start()
        {
            PlayMusic("Ulstermen");
        }

        public void PlayMusic(string name)
        {
            Sound s = Array.Find(musicSounds, s => s.name == name);
            if(s.source == null)
            {
                Debug.Log("Sound Not Found");
            }
            else
            {    
                musicSource.clip = s.source.clip;
                musicSource.Play();

                musicSource.loop = s.loop;
            }
        }

        public IEnumerator PlayMusicSequence(string trackNameOne, string trackNameTwo)
        {
            Sound track1 = Array.Find(musicSounds, track1 => track1.name == trackNameOne);
            Sound track2 = Array.Find(musicSounds, track2 => track2.name == trackNameTwo);
            
            if(track1.source && track2.source != null)
            {
                musicSource.clip = track1.source.clip;
                musicSource.Play();

                yield return new WaitForSeconds(track1.clip.length);

                if (musicSource.clip.name == track1.clip.name)
                {
                    musicSource.clip = track2.source.clip;
                    musicSource.Play();
                    musicSource.loop = track2.loop;
                }
                //else
                //{
                //    yield break;
                //}

            }
        }

        public void FadeOutMusic(String name, float fadeTime, float targetVolume)
        {
            Sound s = Array.Find(musicSounds, s => s.name == name);
            float currentVolume = s.source.volume;
            float currentTime = 0;
            float start = s.source.volume;

            if(s.source == null)
            {
                Debug.Log("Could not find music to fade out");
            }
            else
            {
                while(s.source.volume > 0)
                {
                    currentTime += Time.deltaTime;
                    s.source.volume = Mathf.Lerp(start, targetVolume, currentTime / fadeTime);
                    //s.source.volume -= currentVolume * Time.deltaTime / fadeTime;
                }
                s.source.Stop();
                s.source.volume = currentVolume;
            }
        }

        public void FadeInMusic(String name, float fadeTime)
        {
            Sound s = Array.Find(musicSounds, s => s.name == name);
            float currentVolume = s.source.volume;

            if (s.source == null)
            {
                Debug.Log("Could not find music to fade in");
            }
            else
            {
                while (s.source.volume <= 0)
                {
                    s.source.volume += currentVolume * Time.deltaTime / fadeTime;
                }
                s.source.loop = s.loop;
            }
        }

        public void PlaySFX(string name)
        {
            Sound s = Array.Find(sfxSounds, s => s.name == name);
            if (s.source == null)
            {
                Debug.Log("Sound Not Found");
            }
            else
            {
                sfxSource.clip = s.source.clip;
                sfxSource.PlayOneShot(s.clip);
            }
        }

        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
        }

        public void ToggleSFX()
        {
            sfxSource.mute = !sfxSource.mute;
        }

        public void MusicVolume(float volume)
        {
            musicSource.volume = volume;
        }

        public void SfxVolume(float volume)
        {
            sfxSource.volume = volume;
        }
    }
}