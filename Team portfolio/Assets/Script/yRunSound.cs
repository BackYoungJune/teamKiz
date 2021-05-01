using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yRunSound : MonoBehaviour
{
    yPlayerMovement playerMovement;
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInParent<yPlayerMovement>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMovement.MoveVec != Vector3.zero)
        {
            //myAudioSource.volume = 1.0f;
            myAudioSource.loop = false;
        }
        else
        {
            myAudioSource.Play();
            myAudioSource.loop = true;
            //myAudioSource.volume = 1.0f;
            
        }
    }
}
