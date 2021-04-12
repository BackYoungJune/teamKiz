using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOnOFF_Button : MonoBehaviour
{
    Text myText;
    Image SoundOFF;
    Image SoundOn;


    private void Awake()
    {
        SoundOFF = GameObject.Find("SOUND_OFF_Button").GetComponent<Image>();
        SoundOn = GameObject.Find("SOUND_ON_Button").GetComponent<Image>();
        //SoundOn.color = new Color(1f, 1f, 1f, 1f);

        myText = GetComponentInChildren<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        Debug.Log("ONClick");
        SoundOn.color = new Color(1f, 1f, 1f,1f);
        SoundOFF.color = new Color(1f, 1f, 1f, 0.5f);
    }
    public void OFFButtonClick()
    {
        Debug.Log("OFFClick");

        SoundOn.color = new Color(1f, 1f, 1f, 0.5f);
        SoundOFF.color = new Color(1f, 1f, 1f, 1f);

    }

}
