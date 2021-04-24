﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    Text MyMoney;
    Text myArmor;
    Text myPotion;
    Text myGranade;
    Text myBullet;
    Text HowMany;
    Text myMax;
    Text PlusMoney;
    GameObject NoMoney_Text;
    GameObject NoItem_Text;
    GameObject Sell_UI;
    Slider Sell_Slider;

    int GrandePrice = 20;
    int PotionPrice = 20;
    int ArmorPrice = 100;
    int BulletPrice = 50;
    int OneBulletPrice = 1;

    int changePrice = 0;
    float changeValue = 0f;
    bool IsAssecpt = false;
    public enum STATE
    {
        NORAML, ARMOR, POTION, GRANADE, BULLET
    }
    STATE myState = STATE.NORAML;
    private void Awake()
    {
        Sell_Slider = GameObject.Find("Sell_Slider").GetComponent<Slider>();

        MyMoney = GameObject.Find("MyMoney").GetComponent<Text>();
        myArmor = GameObject.Find("Armor_Text").GetComponent<Text>();
        myPotion = GameObject.Find("Potion_Text").GetComponent<Text>();
        myGranade = GameObject.Find("Granade_Text").GetComponent<Text>();
        myBullet = GameObject.Find("Bullet_Text").GetComponent<Text>();
        HowMany = GameObject.Find("HowMany_Text").GetComponent<Text>();
        myMax = GameObject.Find("Max_Text").GetComponent<Text>();
        PlusMoney = GameObject.Find("PlusMoney_Text").GetComponent<Text>();


        Sell_UI = GameObject.Find("Sell_UI");


        MyMoney.text = J_ItemManager.instance.remainMoney.ToString();
        myArmor.text = J_ItemManager.instance.remainArmor.ToString();
        myPotion.text = J_ItemManager.instance.remainPotion.ToString();
        myGranade.text = J_ItemManager.instance.remainGrenade.ToString();
        myBullet.text = J_ItemManager.instance.ammoRemain.ToString();


        NoMoney_Text = GameObject.Find("NoMoney_Text");
        NoItem_Text = GameObject.Find("NoItem_Text");
        NoMoney_Text.SetActive(false);
        NoItem_Text.SetActive(false);
        Sell_UI.SetActive(false);

        // StartCoroutine(NoMoney());
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("I"))
        {
            Debug.Log(1);
            this.gameObject.SetActive(true);
        }
        MyMoney.text = J_ItemManager.instance.remainMoney.ToString();
        myArmor.text = J_ItemManager.instance.remainArmor.ToString();
        myPotion.text = J_ItemManager.instance.remainPotion.ToString();
        myGranade.text = J_ItemManager.instance.remainGrenade.ToString();
        myBullet.text = J_ItemManager.instance.ammoRemain.ToString();
        //HowMany.text = J_ItemManager.instance.ammoRemain.ToString();

        StateProcess();
    }

    void ChangeState(STATE s)
    {
        if (s == myState) return;
        myState = s;

        switch (myState)
        {
            case STATE.NORAML:
                break;
            case STATE.ARMOR:
                changeValue = 1f / J_ItemManager.instance.remainArmor;
                Sell_Slider.value = J_ItemManager.instance.remainArmor * changeValue;


                break;
            case STATE.POTION:
                changeValue = 1f / J_ItemManager.instance.remainPotion;
                Sell_Slider.value = J_ItemManager.instance.remainPotion * changeValue;

                break;
            case STATE.GRANADE:
                changeValue = 1f / J_ItemManager.instance.remainGrenade;
                Sell_Slider.value = J_ItemManager.instance.remainGrenade * changeValue;

                break;
            case STATE.BULLET:
                changeValue = 1f / J_ItemManager.instance.ammoRemain;
                Sell_Slider.value = J_ItemManager.instance.ammoRemain * changeValue;

                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.NORAML:
                break;
            case STATE.ARMOR:
                {
                    float howmany = J_ItemManager.instance.remainArmor * Sell_Slider.value;
                    int changeInt = (int)howmany;

                    HowMany.text = changeInt.ToString();

                    PlusMoney.text = " + " + (changeInt * ArmorPrice * 0.5).ToString();
                    myMax.text = J_ItemManager.instance.remainArmor.ToString();
                    if (IsAssecpt)
                    {
                        float takeMoney = changeInt * ArmorPrice * 0.5f;
                        J_ItemManager.instance.remainArmor -= changeInt;
                        Debug.Log(J_ItemManager.instance.remainArmor);
                        J_ItemManager.instance.remainMoney += (int)takeMoney;
                        Sell_UI.SetActive(false);
                        IsAssecpt = false;

                    }

                }

                break;
            case STATE.POTION:
                {
                    float howmany = J_ItemManager.instance.remainPotion * Sell_Slider.value;
                    int changeInt = (int)howmany;

                    HowMany.text = changeInt.ToString();

                    PlusMoney.text = " + " + (changeInt * PotionPrice * 0.5).ToString();
                    myMax.text = J_ItemManager.instance.remainPotion.ToString();
                    if (IsAssecpt)
                    {
                        float takeMoney = changeInt * PotionPrice * 0.5f;
                        J_ItemManager.instance.remainPotion -= changeInt;
                        Debug.Log(J_ItemManager.instance.remainPotion);
                        J_ItemManager.instance.remainMoney += (int)takeMoney;
                        Sell_UI.SetActive(false);
                        IsAssecpt = false;

                    }

                }

                break;
            case STATE.GRANADE:
                {
                    float howmany = J_ItemManager.instance.remainGrenade * Sell_Slider.value;
                    int changeInt = (int)howmany;

                    HowMany.text = changeInt.ToString();

                    PlusMoney.text = " + " + (changeInt * GrandePrice * 0.5).ToString();
                    myMax.text = J_ItemManager.instance.remainGrenade.ToString();
                    if (IsAssecpt)
                    {
                        float takeMoney = changeInt * GrandePrice * 0.5f;
                        J_ItemManager.instance.remainGrenade -= changeInt;
                        Debug.Log(J_ItemManager.instance.remainGrenade);
                        J_ItemManager.instance.remainMoney += (int)takeMoney;
                        Sell_UI.SetActive(false);
                        IsAssecpt = false;

                    }

                }

                break;
            case STATE.BULLET:
                {
                    float howmany = J_ItemManager.instance.ammoRemain * Sell_Slider.value;
                    int changeInt = (int)howmany;
                    float takeMoney = changeInt * OneBulletPrice * 0.5f;

                    HowMany.text = changeInt.ToString();

                    PlusMoney.text = " + " + ((int)takeMoney).ToString();
                    myMax.text = J_ItemManager.instance.ammoRemain.ToString();
                    if (IsAssecpt)
                    {
                        J_ItemManager.instance.ammoRemain -= changeInt;
                        Debug.Log(J_ItemManager.instance.ammoRemain);
                        J_ItemManager.instance.remainMoney += (int)takeMoney;
                        Sell_UI.SetActive(false);
                        IsAssecpt = false;

                    }

                }

                break;
        }
    }

    //sell Buttons

    public void SellArmorButton()
    {
        if (J_ItemManager.instance.remainArmor == 0)
        {
            StartCoroutine(NoItem());

        }
        else
        {
            Sell_UI.SetActive(true);
            myMax.text = J_ItemManager.instance.remainArmor.ToString();
            ChangeState(STATE.ARMOR);

        }
    }
    public void SellPotionButton()
    {
        if (J_ItemManager.instance.remainPotion == 0)
        {
            StartCoroutine(NoItem());

        }
        else
        {
            Sell_UI.SetActive(true);
            myMax.text = J_ItemManager.instance.remainPotion.ToString();
            ChangeState(STATE.POTION);

        }

    }
    public void SellGranadeButton()
    {
        if (J_ItemManager.instance.remainGrenade == 0)
        {
            StartCoroutine(NoItem());

        }
        else
        {

            Sell_UI.SetActive(true);
            myMax.text = J_ItemManager.instance.remainGrenade.ToString();
            ChangeState(STATE.GRANADE);
        }

    }
    public void SellBulletButton()
    {
        if (J_ItemManager.instance.ammoRemain == 0)
        {
            StartCoroutine(NoItem());

        }
        else
        {
            Sell_UI.SetActive(true);
            myMax.text = J_ItemManager.instance.ammoRemain.ToString();
            ChangeState(STATE.BULLET);

        }

    }
    public void SellAcceptButton()
    {
        IsAssecpt = true;
        //ChangeState(STATE.NORAML);
        //Sell_UI.SetActive(false);

    }
    public void SellCloseButton()
    {
        ChangeState(STATE.NORAML);
        Sell_UI.SetActive(false);

    }

    /// <summary>
    /// ///////////////////////////////////////////////////////////
    /// </summary>
    // buy Buttons
    public void BuyArmorButton()
    {
        if (J_ItemManager.instance.remainMoney >= ArmorPrice)
        {
            J_ItemManager.instance.remainArmor++;
            J_ItemManager.instance.remainMoney -= ArmorPrice;
        }
        else
        {
            StopAllCoroutines();

            StartCoroutine(NoMoney());
        }

    }
    public void BuyPotionButton()
    {
        if (J_ItemManager.instance.remainMoney >= PotionPrice)
        {
            J_ItemManager.instance.remainPotion++;
            J_ItemManager.instance.remainMoney -= PotionPrice;
        }
        else
        {
            StopAllCoroutines();

            StartCoroutine(NoMoney());
        }


    }
    public void BuyGranadeButton()
    {
        if (J_ItemManager.instance.remainMoney >= GrandePrice)
        {
            J_ItemManager.instance.remainGrenade++;
            J_ItemManager.instance.remainMoney -= GrandePrice;
        }
        else
        {
            StopAllCoroutines();

            StartCoroutine(NoMoney());
        }


    }
    public void BuyBulletButton()
    {
        if (J_ItemManager.instance.remainMoney >= BulletPrice)
        {
            J_ItemManager.instance.ammoRemain += 50;
            J_ItemManager.instance.remainMoney -= BulletPrice;
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(NoMoney());
        }


    }
    IEnumerator NoMoney()
    {
        NoMoney_Text.SetActive(true);
        float timePassed = 0f;

        while (timePassed < 1f)
        {
            timePassed += Time.deltaTime;

            yield return null;
        }

        NoMoney_Text.SetActive(false);
    }
    IEnumerator NoItem()
    {
        NoItem_Text.SetActive(true);
        float timePassed = 0f;

        while (timePassed < 1f)
        {
            timePassed += Time.deltaTime;


            yield return null;
        }

        NoItem_Text.SetActive(false);
    }
    public void StoreEXITButton()
    {
        this.gameObject.SetActive(false);
    }
}

