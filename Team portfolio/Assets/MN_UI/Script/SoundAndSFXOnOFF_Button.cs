using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundAndSFXOnOFF_Button : MonoBehaviour
{
    Text myText;
    Image SoundON;
    Image SoundOFF;
    Image SFXOFF;
    Image SFXOn;

    private void Awake()
    {
        SoundOFF = GameObject.Find("SOUND_OFF_Button").GetComponent<Image>();
        SoundON = GameObject.Find("SOUND_ON_Button").GetComponent<Image>();
        SFXOFF = GameObject.Find("SFX_OFF_Button").GetComponent<Image>();
        SFXOn = GameObject.Find("SFX_ON_Button").GetComponent<Image>();

        myText = GetComponentInChildren<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSOUNDButtonClick()
    {
        MN_UISoundManager.Instance.audiosource_backgound.volume = 1f;
       //Debug.Log("ONClick");
        SoundON.color = new Color(1f, 1f, 1f,1f);
        SoundOFF.color = new Color(1f, 1f, 1f, 0.5f);
    }
    public void OFFSOUNDButtonClick()
    {
        // Debug.Log("OFFClick");
        MN_UISoundManager.Instance.audiosource_backgound.volume = 0f;

        SoundON.color = new Color(1f, 1f, 1f, 0.5f);
        SoundOFF.color = new Color(1f, 1f, 1f, 1f);

    }

    public void OnSFXButtonClick()
    {
        MN_UISoundManager.Instance.audiosource_click.volume = 1f;

        // Debug.Log("ONClick");
        SFXOn.color = new Color(1f, 1f, 1f, 1f);
        SFXOFF.color = new Color(1f, 1f, 1f, 0.5f);
    }
    public void OFFSFXButtonClick()
    {
        MN_UISoundManager.Instance.audiosource_click.volume = 0f;

        //Debug.Log("OFFClick");

        SFXOFF.color = new Color(1f, 1f, 1f, 1f);
        SFXOn.color = new Color(1f, 1f, 1f, 0.5f);

    }

}
