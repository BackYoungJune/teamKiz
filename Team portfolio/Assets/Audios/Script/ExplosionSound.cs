using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip ExplodeSound;
    void Start()
    {
        Sound.I.PlayEffectSound(ExplodeSound, GetComponent<AudioSource>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
