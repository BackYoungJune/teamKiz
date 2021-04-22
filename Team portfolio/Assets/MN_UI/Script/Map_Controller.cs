using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_Controller : MonoBehaviour
{

    GameObject Map_Background;
    bool isOn;
    enum STATE
    {
        NORMAL,ON,OFF
    }

    [SerializeField]
    STATE myState = STATE.NORMAL;

    private void Awake()
    {
        Map_Background = GameObject.Find("Map_Background");
        Map_Background.SetActive(false);
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.M))
        {
            Map_Background.SetActive(true);

        }
        else
        {
            Map_Background.SetActive(false);


        }
        StateProcess();

    }
    void ChangeState(STATE s)
    {
        if (s == myState) return;

        switch(myState)
        {
            case STATE.NORMAL:

                break;

            case STATE.ON:
                Map_Background.SetActive(true);
                break;

            case STATE.OFF:
                Map_Background.SetActive(true);
                ChangeState(STATE.NORMAL);
                break;

        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.NORMAL:

                break;
            case STATE.ON:
                if (myState == STATE.NORMAL && Input.GetKeyDown(KeyCode.M))
                {
                    ChangeState(STATE.OFF);
                }

                break;

            case STATE.OFF:

                break;

        }
    }
}
