using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Health_Bar : MonoBehaviour
{
    GameObject Boss_Fill;
    Image Boss_fill_Image;
    Slider Boss_Slider;

    bool IsBoxHit;
    public float Value;
    public enum STATE
    {
        FIRST,START,NORMAL,PLAY
    }
    public STATE myState = STATE.FIRST;


    private void Awake()
    {


        Debug.Log("BossHealthBar");
        Boss_Slider = GetComponent<Slider>();
        Boss_Slider.value = 0f;

        //Boss_Slider.value = MN_UIManager.Instance.Boss_MaxHealth * 0.00003f;
        IsBoxHit = false;
        Boss_fill_Image = GameObject.Find("Boss_Fill").GetComponent<Image>();

        ChangeState(STATE.START);
    }

    // Update is called once per frame
    void Update()
    {
        //Boss_Slider.value = MN_UIManager.Instance.Boss_CurrentHealth * 0.00003333f;

        if (IsBoxHit)
        {
            ChangeState(STATE.PLAY);

        }
        StateProcess();
    }

    void ChangeState(STATE s)
    {
        if (s == myState) return;
        myState = s;

        switch (myState)
        {
            case STATE.FIRST:
                break;

            case STATE.START:

                StartCoroutine(ChargingBar());
                break;
            case STATE.NORMAL:
                Boss_Slider.value = MN_UIManager.Instance.Boss_CurrentHealth * 0.00003333f;

                break;
            case STATE.PLAY:
                StartCoroutine(ChangeAlphaFirst());
                break;

        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.START:
                break;
            case STATE.NORMAL:
                Boss_Slider.value = MN_UIManager.Instance.Boss_CurrentHealth * 0.00003333f;

                break;
            case STATE.PLAY:
                Boss_Slider.value = MN_UIManager.Instance.Boss_CurrentHealth * 0.00003333f;

                break;
        }
    }

    IEnumerator ChargingBar()
    {
        float value = Boss_Slider.value;

        while(value < 1f)
        {
            value += Time.deltaTime * 0.5f;
            Boss_Slider.value = value;

            yield return null;
        }
        ChangeState(STATE.NORMAL);
    }

    IEnumerator ChangeAlphaFirst()
    {
        float alpha = 0.2f;

        while (alpha < 0.8f)
        {
            alpha += Time.deltaTime;
            Boss_fill_Image.color = new Color(1f, 0f, 0f, alpha);

            yield return null;

        }
        StartCoroutine(ChangeAlphaChangeSecond());

    }
    IEnumerator ChangeAlphaChangeSecond()
    {
        float alpha = 0.8f;
        while (alpha > 0.2f)
        {
            alpha -= Time.deltaTime;
            Boss_fill_Image.color = new Color(1f, 0f, 0f, alpha);

            yield return null;

        }
        MN_UIManager.Instance.IsHitBox = false;
        Debug.Log(MN_UIManager.Instance.IsHit);
        ChangeState(STATE.NORMAL);
    }
}
