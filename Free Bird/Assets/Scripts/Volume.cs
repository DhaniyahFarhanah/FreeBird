using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider BGMSlider, SFXSlider;
    public AudioSource WarningSource1, WarningSource2, WarningSource3, GuidedGuy;

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
        BGMSlider.value = bgmVolume;
        SFXSlider.value = sfxVolume;
    }

    public void increaseBGMVolume()
    {
        if(bgmVolume < 1.0f)
        {
            BGMSlider.value += 0.1f;
            bgmVolume += 0.1f;
            SetVolumes();
        }
        BGMSlider.value = bgmVolume;
    }

    public void decreaseBGMVolume()
    {
        if(bgmVolume > 0.0f)
        {
            BGMSlider.value -= 0.1f;
            bgmVolume -= 0.1f;
            SetVolumes();
        }

        BGMSlider.value = bgmVolume;
    }

    public void increaseSFXVolume()
    {
        if(sfxVolume < 1.0f)
        {
            SFXSlider.value += 0.1f;
            sfxVolume += 0.1f;
            SetVolumes();
        }
        SFXSlider.value = sfxVolume;
    }

    public void decreaseSFXVolume()
    {
        if(sfxVolume > 0.0f)
        {
            SFXSlider.value -= 0.1f;
            sfxVolume -= 0.1f;
            SetVolumes();
        }
        SFXSlider.value = sfxVolume;
    }

    public void SetVolumes()
    {
        GameStateManager.SetVolumes(bgmVolume,sfxVolume);
        WarningSource1.volume = sfxVolume;
        WarningSource2.volume = sfxVolume;
        WarningSource3.volume = sfxVolume;
        GuidedGuy.volume = sfxVolume;
    }
}