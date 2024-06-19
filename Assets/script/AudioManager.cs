using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource BackGroundMusic;
    

    [Header("Audio Clip")]
    public AudioClip backGround;

    private void Start()
    {
        BackGroundMusic.clip = backGround;
        BackGroundMusic.Play();
    }

}
