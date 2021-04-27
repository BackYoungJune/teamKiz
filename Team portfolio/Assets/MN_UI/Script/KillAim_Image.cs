using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillAim_Image : MonoBehaviour
{
    GameObject killAim;
    Image killAim_Image;
    Image Aim;

    Image startImage;
    enum STATE
    {
        NORMAL,START,STOP
    }
    STATE myState = STATE.NORMAL;
    private void Awake()
    {
        Aim = GetComponent<Image>();
        killAim = GameObject.Find("Kill_Aim");
        killAim_Image = killAim.GetComponent<Image>();

        startImage = Aim;
        killAim_Image.color = new Color(killAim_Image.color.r, killAim_Image.color.g, killAim_Image.color.b, 0f);
        //killAim_Image.color.a = 0f;

        killAim.SetActive(false);


    }
    // Update is called once per frame
    void Update()
    {
        if(MN_UIManager.Instance.IsZombieKill)
        {
            ChangeState(STATE.START);
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
                killAim.SetActive(true);
                Aim.color = new Color(1f, 0f, 0f);
                StartCoroutine(ChangeAlphaFirst());
                break;
            case STATE.STOP:
                Aim.color = new Color(0f, 1f, 0f);
                break;
        }

    }
    void StateProcess()
    {
        switch (myState)
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
        float alpha = 0f;

        while (alpha < 0.8f)
        {
            alpha += Time.deltaTime * 5f;
            killAim_Image.color = new Color(1f, 0f, 0f, alpha);

            yield return null;

        }
        StartCoroutine(ChangeAlphaChangeSecond());

    }
    IEnumerator ChangeAlphaChangeSecond()
    {
        float alpha = 0.8f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * 5f;
            killAim_Image.color = new Color(1f, 0f, 0f, alpha);

            yield return null;

        }
        ChangeState(STATE.STOP);
        MN_UIManager.Instance.IsZombieKill = false;
        Aim = startImage;
        killAim.SetActive(false);

        //Debug.Log(MN_UIManager.Instance.IsHit);
    }
}
