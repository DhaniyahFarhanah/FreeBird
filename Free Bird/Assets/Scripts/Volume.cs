using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider BGMSlider, SFXSlider;
    public AudioSource WarningSource1, WarningSource2, WarningSource3;

    float bgmVolume;
    float sfxVolume;

    private void Awake()
    {
        bgmVolume = GameStateManager.GetBGMVolume();
        sfxVolume = GameStateManager.GetSFXVolume();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BGMSlider.value = bgmVolume;
        SFXSlider.value = sfxVolume;
        Debug.Log("bgmVolume: " + bgmVolume);
        Debug.Log("sfxVolume: " + sfxVolume);
    }

    public void increaseBGMVolume()
    {
        if(bgmVolume < 1.0f)
        {
            BGMSlider.value += 0.1f;
            bgmVolume += 0.1f;
            SetVolumes();
        }
    }

    public void decreaseBGMVolume()
    {
        if(bgmVolume > 0.0f)
        {
            BGMSlider.value -= 0.1f;
            bgmVolume -= 0.1f;
            SetVolumes();
        }
        
    }

    public void increaseSFXVolume()
    {
        if(sfxVolume < 1.0f)
        {
            SFXSlider.value += 0.1f;
            sfxVolume += 0.1f;
            SetVolumes();
        }
        
    }

    public void decreaseSFXVolume()
    {
        if(sfxVolume > 0.0f)
        {
            SFXSlider.value -= 0.1f;
            sfxVolume -= 0.1f;
            SetVolumes();
        }
        
    }

    public void SetVolumes()
    {
        GameStateManager.SetVolumes(bgmVolume,sfxVolume);
        WarningSource1.volume = sfxVolume;
        WarningSource2.volume = sfxVolume;
        WarningSource3.volume = sfxVolume;
    }
}