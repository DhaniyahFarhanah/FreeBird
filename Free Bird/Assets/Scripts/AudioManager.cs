using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } private set => instance = value; }

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
            
        DontDestroyOnLoad(gameObject);

        musicSource.volume = GameStateManager.GetBGMVolume();
        sfxSource.volume = GameStateManager.GetSFXVolume();

    }

    private void Start()
    {
        PlayMusic("Menu");
    }

    private void Update()
    {
        musicSource.volume = GameStateManager.GetBGMVolume();
        sfxSource.volume = GameStateManager.GetSFXVolume();
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        
        if (s == null)
        {
            Debug.Log("Sound Not Found!");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found!");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
}
