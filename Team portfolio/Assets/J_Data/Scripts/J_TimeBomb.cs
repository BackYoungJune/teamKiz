using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_TimeBomb : MonoBehaviour
{
    public GameObject explosionEffect;

    float timer = 5.0f;
    bool IsPlanted = false;
    bool soundOn = false;
    public void SetPlanted(bool b) { this.IsPlanted = b; }

    public AudioSource audioSource;
    public AudioClip timerSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlanted)
        {
            PlantBomb();
        }
    }

    void PlantBomb()
    {
        if(!soundOn)
        {
            PlayTimeBombSound();
            soundOn = true;
        }
        StartCoroutine(SetTimer());
    }

    IEnumerator SetTimer()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            // 남은 시간 표시 
        }
        else
        {
            Explosion();
            IsPlanted = false;
            soundOn = false;
        }
        yield return null;
    }

    void Explosion()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        GetComponent<J_Explode>().IndirectExplosion(transform.position);
        Destroy(gameObject);
    }

    void PlayTimeBombSound()
    {
        Sound.I.PlayEffectSound(timerSound, audioSource);
    }
}
