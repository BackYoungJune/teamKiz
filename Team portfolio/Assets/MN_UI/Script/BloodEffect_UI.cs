using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodEffect_UI : MonoBehaviour
{
    Image myImage;
    public enum STATE
    {
        NORMAL,START,STOP
    }

    public STATE myState = STATE.NORMAL;
   // GameObject panel;
    GameObject BloodPanel;
    private void Awake()
    {
        BloodPanel = GameObject.Find("BloodPanel");

        myImage = GetComponent<Image>();
        myImage.color = new Color(0f, 0f, 0f, 0f);
        //panel = GameObject.Find("BloodEffect");
        //panel.SetActive(false);

        BloodPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(MN_UIManager.Instance.IsHit)
        {
            ChangeState(STATE.START);
            //Debug.Log("STATE START");
        }
        StateProcess();

    }
    void ChangeState(STATE s)
    {
        if (s == myState) return;
        myState = s;
        switch(myState)
        {
            case STATE.NORMAL:
                break;
            case STATE.START:
                StartCoroutine(ChangeAlphaFirst());
                //panel.SetActive(true);
                break;
            case STATE.STOP:
                MN_UIManager.Instance.IsHit = false;

                //panel.SetActive(false);

                break;

        }
    }
    void StateProcess()
    {
        switch(myState)
        {
            case STATE.NORMAL:
                break;
            case STATE.START:
                break;
            case STATE.STOP:
                break;
        }
    }
    IEnumerator ChangeAlphaFirst()
    {
        BloodPanel.SetActive(true);
        float alpha = 0f;

        while (alpha < 0.8f)
        {
            alpha += Time.deltaTime;
            myImage.color = new Color(1f, 0f, 0f, alpha);

            yield return null;

        }
        StartCoroutine(ChangeAlphaChangeSecond());

    }
    IEnumerator ChangeAlphaChangeSecond()
    {
        float alpha = 0.8f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime;
            myImage.color = new Color(1f, 0f, 0f, alpha);


            if(alpha <= 0.4f)
                BloodPanel.SetActive(false);

            yield return null;
        }
        ChangeState(STATE.STOP);
        Debug.Log(MN_UIManager.Instance.IsHit);
    }
}
