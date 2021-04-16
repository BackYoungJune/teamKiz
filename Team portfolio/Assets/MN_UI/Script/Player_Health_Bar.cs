using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health_Bar : MonoBehaviour
{
   // MN_UIManager UIManager;
    Slider mySlider;

    public enum STATE
    {
        NORMAL,PLUS,MINUS
    }
    public STATE myState = STATE.NORMAL;
    private void Awake()
    {
        mySlider = GetComponent<Slider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(UIManager.CurrentHealth);
        mySlider.value = MN_UIManager.Instance.CurrentHealth * 0.005f;
        // UIManager.

        StateProcess();

    }
    void StateChange(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case STATE.NORMAL:
                break;
            case STATE.PLUS:
               // StartCoroutine(ChangeHealth(true));
                break;
            case STATE.MINUS:
               // StartCoroutine(ChangeHealth(false));

                break;

        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.NORMAL:
                break;
            case STATE.PLUS:
                break;
            case STATE.MINUS:
                break;

        }
    }
    //입력된 bool값에 따라 체력바를 부드럽게 차거나 줄어들게 하는 역할
    //IEnumerator ChangeHealth(bool IsPlus)
    //{
    //    float
    //}
}
