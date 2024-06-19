using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Setting : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume(float volume){
        audioMixer.SetFloat("Volume",volume);
    }

    public void SetQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen){
        Screen.fullScreen = isFullScreen;
    }
}
