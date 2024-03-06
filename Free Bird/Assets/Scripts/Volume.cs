using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public AudioSource BGMSource, SFXSource;
    public Slider BGMSlider, SFXSlider;
    public AudioSource WarningSource1, WarningSource2, WarningSource3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BGMSlider.value = BGMSource.volume;
        SFXSlider.value = SFXSource.volume;
    }

    public void increaseBGMVolume()
    {
        BGMSlider.value += 0.1f;
        BGMSource.volume += 0.1f;
    }

    public void decreaseBGMVolume()
    {
        BGMSlider.value -= 0.1f;
        BGMSource.volume -= 0.1f;
    }

    public void increaseSFXVolume()
    {
        SFXSlider.value += 0.1f;
        SFXSource.volume += 0.1f;
        WarningSource1.volume += 0.1f;
        WarningSource2.volume += 0.1f;
        WarningSource3.volume += 0.1f;
    }

    public void decreaseSFXVolume()
    {
        SFXSlider.value -= 0.1f;
        SFXSource.volume -= 0.1f;
        WarningSource1.volume -= 0.1f;
        WarningSource2.volume -= 0.1f;
        WarningSource3.volume -= 0.1f;
    }
}