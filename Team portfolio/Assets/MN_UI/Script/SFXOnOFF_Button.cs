using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOnOFF_Button : MonoBehaviour
{
    Text myText;



    private void Awake()
    {

        //SoundOn.color = new Color(1f, 1f, 1f, 1f);

        myText = GetComponentInChildren<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
