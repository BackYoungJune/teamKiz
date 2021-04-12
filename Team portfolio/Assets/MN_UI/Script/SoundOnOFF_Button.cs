using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXOnOFF_Button : MonoBehaviour
{
    Text myText;
    Image SFXON;
    Image SFXOFF;


    private void Awake()
    {
        SFXOFF = GameObject.Find("SFX_OFF_Button").GetComponent<Image>();
        SFXON = GameObject.Find("SFX_ON_Button").GetComponent<Image>();
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
        SFXON.color = new Color(1f, 1f, 1f,1f);
        SFXOFF.color = new Color(1f, 1f, 1f, 0.5f);
    }
    public void OFFButtonClick()
    {
        Debug.Log("OFFClick");

        SFXON.color = new Color(1f, 1f, 1f, 0.5f);
        SFXOFF.color = new Color(1f, 1f, 1f, 1f);

    }

}
