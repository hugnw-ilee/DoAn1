using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] AudioSource SFXSound;

    public AudioClip normalAttack;
    public AudioClip comboAttack;

    public void PlaySFXClip(AudioClip clip)
    {
        SFXSound.PlayOneShot(clip);
    }
}
