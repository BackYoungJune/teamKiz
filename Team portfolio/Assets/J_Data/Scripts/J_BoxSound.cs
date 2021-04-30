using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_BoxSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip destorySound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        PlayDestroySound();
    }
    void PlayDestroySound()
    {
        Sound.I.PlayEffectSound(destorySound, audioSource);
    }

}
