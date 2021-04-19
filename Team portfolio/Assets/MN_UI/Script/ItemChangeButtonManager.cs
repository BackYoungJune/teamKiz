using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemChangeButtonManager : MonoBehaviour
{
    public enum STATE
    {
        RIFLE, AXE, GRENADE, POTION
    }
    //처음에 캐릭터가 들고 있는 무기
    public STATE myState = STATE.AXE;

    GameObject ItemButtons;
    Image NowWeaponImage;
    Text AmmoText;

    [SerializeField]
    List<GameObject> ItemImages;

    bool IsRifle = true;
    private void Awake()
    {
        // ItemImage를 resources에서 불러오기
        ItemImages.Add(Resources.Load("Rifle_Image") as GameObject);
        ItemImages.Add(Resources.Load("AXE_Image") as GameObject);
        ItemImages.Add(Resources.Load("Granade_Image") as GameObject);


        ItemButtons = GameObject.Find("ItemButtons");
        NowWeaponImage = GameObject.Find("NowWeapon_Image").GetComponent<Image>();
        AmmoText = GameObject.Find("AmmoText").GetComponent<Text>();
        if (myState == STATE.AXE)
            AmmoText.text = "";
        else
            AmmoText.text = MN_UIManager.Instance.ammo.ToString()+" / 25";

        ItemButtons.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            ItemButtons.SetActive(true);
            Time.timeScale = 0.2f;

        }
        else
        {
            Time.timeScale = 1f;

            ItemButtons.SetActive(false);

        }


        StateProcess();
        
    }
    void ChangeState(STATE s)
    {
        if (s == myState) return;
        myState = s;
        yPlayerAxe axe = FindObjectOfType<yPlayerAxe>();
        yPlayerShooter rifle = FindObjectOfType<yPlayerShooter>();
        switch (myState)
        {
                
            case STATE.RIFLE:
                NowWeaponImage.sprite = ItemImages[0].GetComponent<Image>().sprite;
                IsRifle = true;
                rifle.enabled = true;
                axe.enabled = false;
                break;
            case STATE.AXE:
                NowWeaponImage.sprite = ItemImages[1].GetComponent<Image>().sprite;

                IsRifle = false;
                axe.enabled = true;
                rifle.enabled = false;
                break;
            case STATE.GRENADE:
                NowWeaponImage.sprite = ItemImages[2].GetComponent<Image>().sprite;
                AmmoText.text = "";

                break;
            case STATE.POTION:
                MN_UIManager.Instance.UpdatePlayerHealth(20f);
                if (IsRifle)
                    ChangeState(STATE.RIFLE);
                else
                    ChangeState(STATE.AXE);
                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.RIFLE:
                AmmoText.text = MN_UIManager.Instance.ammo.ToString() + " / 25";

                //Debug.Log("RIFLE");
                break;
            case STATE.AXE:
                AmmoText.text = "";
                //Debug.Log("AXE");

                break;
            case STATE.GRENADE:
                //Debug.Log("GRENADE");

                break;
            case STATE.POTION:
               // Debug.Log("Potion");
                break;
        }
    }
    public void Rifle_Button()
    {
        ChangeState(STATE.RIFLE);
    }
    public void AXE_Button()
    {
        ChangeState(STATE.AXE);
    }
    public void GRANADE_Button()
    {
        ChangeState(STATE.GRENADE);
    }
    public void POTION_Button()
    {

        ChangeState(STATE.POTION);

        //Debug.Log("Drink Potion!!");
    }
}
