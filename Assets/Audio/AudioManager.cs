using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] musicSounds, sfxSounds;

    public static AudioManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound s in musicSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Ulstermen");    
    }
    public void Play(string name)
    {
        Sound s = Array.Find(musicSounds, sound => sound.name == name);
        if(s.source == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            s.source.Play();
        }

    }
}
