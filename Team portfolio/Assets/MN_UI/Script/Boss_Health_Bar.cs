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
        START,NORMAL,PLAY
    }
    public STATE myState = STATE.START;


    private void Awake()
    {


        Debug.Log("BossHealthBar");
        Boss_Slider = GetComponent<Slider>();

        Boss_Slider.value = MN_UIManager.Instance.Boss_MaxHealth * 0.00003f;
        IsBoxHit = false;
        Boss_fill_Image = GameObject.Find("Boss_Fill").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        Boss_Slider.value = MN_UIManager.Instance.Boss_CurrentHealth * 0.00003333f;

        if (IsBoxHit)
        {
            ChangeState(STATE.PLAY);

        }
    }

    void ChangeState(STATE s)
    {
        if (s == myState) return;
        myState = s;

        switch (myState)
        {
            case STATE.START:
                StartCoroutine(ChargingBar());
                break;
            case STATE.NORMAL:
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
                break;
            case STATE.PLAY:
                break;
        }
    }
    IEnumerator ChargingBar()
    {
        float value = Boss_Slider.value;

        while(value < 1f)
        {
            value += MN_UIManager.Instance.Boss_CurrentHealth * 0.00003333f;
            Boss_Slider.value = value;

            yield return null;
        }
        //ChangeState(STATE.NORMAL);
        Value = Boss_Slider.value;
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
        //ChangeState(STATE.NORMAL);
        MN_UIManager.Instance.IsHitBox = false;
        Debug.Log(MN_UIManager.Instance.IsHit);
    }
}
