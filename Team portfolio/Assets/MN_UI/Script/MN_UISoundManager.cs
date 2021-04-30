using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MN_UISoundManager : MonoBehaviour
{
    private static MN_UISoundManager I;
    public static MN_UISoundManager Instance
    {
        get
        {
            if (I == null)
            {
                I = FindObjectOfType<MN_UISoundManager>();
            }

            return I;
        }

    }

    public AudioSource audiosource_backgound;
    public AudioSource audiosource_click;
    public AudioClip ClickSound;
    public AudioClip BackGroundSound;

    private void Awake()
    {
        
    }
    private void Update()
    {
        //audiosource_backgound.Play();
        if (Input.GetMouseButtonDown(0))
        {
            audiosource_click.PlayOneShot(ClickSound);
        }
    }
}
