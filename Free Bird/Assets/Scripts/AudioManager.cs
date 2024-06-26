using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } private set => instance = value; }

    public Sound[] musicSounds, sfxSounds, snakeSounds, raccoonSounds;
    public AudioSource musicSource, sfxSource;
    public AudioSource snakeSource, raccoonSource;
    public AudioLowPassFilter filter;

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
        filter = musicSource.GetComponent<AudioLowPassFilter>();
    }

    private void Update()
    {
        musicSource.volume = GameStateManager.GetBGMVolume();
        sfxSource.volume = GameStateManager.GetSFXVolume();

        if(!GameStateManager.GetEnd() && !GameStateManager.GetGameStatus() && !GameStateManager.GetWin())
        {
            StartCoroutine(Activate());
        }
    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(0.5f);
        sfxSource.enabled = true;
    }
    public void StopMusic()
    {
        musicSource.Stop();
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
            Debug.Log("Now playing: " + name);
        }
    }

    public void Muffle()
    {
        filter.cutoffFrequency = 2500; //Muffle
    }

    public void Normalize()
    {
        filter.cutoffFrequency = 22000; //Normal
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

    public void PlaySnake(string name)
    {
        snakeSource.Stop();
        Sound s = Array.Find(snakeSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found!");
        }

        else
        {
            snakeSource.PlayOneShot(s.clip);
        }
    }

    public void PlayRaccoon(string name)
    {
        Sound s = Array.Find(raccoonSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found!");
        }

        else
        {
            raccoonSource.PlayOneShot(s.clip);
        }
    }

    public void Transition()
    {
        StartCoroutine(FadeOutAndIn());
    }

    IEnumerator FadeOutAndIn()
    {
        float initialVolume = musicSource.volume;
        float fadeOutDuration = 3.0f;
        float fadeInDuration = 5.0f;

        // Fade out
        float time = 0f;
        while (time < fadeOutDuration)
        {
            time += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(initialVolume, 0f, time / fadeOutDuration);
            yield return null;
        }
        musicSource.volume = 0f;
        Normalize();

        // Fade in
        time = 0f;
        while (time < fadeInDuration)
        {
            time += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0f, initialVolume, time / fadeInDuration);
            yield return null;
        }

        musicSource.volume = initialVolume;
    }
}
