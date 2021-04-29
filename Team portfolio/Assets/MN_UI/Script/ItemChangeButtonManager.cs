using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemChangeButtonManager : MonoBehaviour
{
    public enum STATE
    {
        HAND,RIFLE, AXE, GRENADE, POTION
    }
    //처음에 캐릭터가 들고 있는 무기
    public STATE myState = STATE.HAND;

    GameObject ItemButtons;
    Image NowWeaponImage;
    Text AmmoText;

    [SerializeField]
    List<GameObject> ItemImages;

    Text Granade_Text;
    Text Potion_Text;
    Text Armor_Text;

    J_SwtichWeapon swtichWeapon;
    J_ItemManager itemManager;

    int MyMoney;
    int myArmor;
    int myPotion;
    int myGranade;
    int myBullet;


    bool IsRifle = true;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        // ItemImage를 resources에서 불러오기
        ItemImages.Add(Resources.Load("Rifle_Image") as GameObject);
        ItemImages.Add(Resources.Load("AXE_Image") as GameObject);
        ItemImages.Add(Resources.Load("Granade_Image") as GameObject);
        ItemImages.Add(Resources.Load("Fist_Image") as GameObject);

        //GameObject.Find("NowWeapon_Image").GetComponent<Image>().sprite = ItemImages[3].GetComponent<Image>().sprite;

        ItemButtons = GameObject.Find("ItemButtons");
        NowWeaponImage = GameObject.Find("NowWeapon_Image").GetComponent<Image>();
        AmmoText = GameObject.Find("AmmoText").GetComponent<Text>();

        //if (myState == STATE.AXE)
        //    AmmoText.text = "";
        //else
        //    AmmoText.text = MN_UIManager.Instance.ammo.ToString() + " / " + MN_UIManager.Instance.MaxAmmo.ToString();

        Granade_Text = GameObject.Find("Granade_Text").GetComponent<Text>();
        Potion_Text = GameObject.Find("Potion_Text").GetComponent<Text>();
        Armor_Text = GameObject.Find("Armor_Text").GetComponent<Text>();

        //수류탄 개수에 사용할 변수
        Granade_Text.text = MN_UIManager.Instance.Granade.ToString();
        //아머 개수에 사용할 변수
        //Armor_Text.text = "aaaaaaaaa";
        //포션 개수에 사용할 변수
        Potion_Text.text = "";
        Armor_Text.text = "X " + J_ItemManager.instance.remainArmor.ToString();

        ItemButtons.SetActive(false);

        // 무기 스위칭에 사용할 변수
        swtichWeapon = FindObjectOfType<J_SwtichWeapon>();
        //itemManager = FindObjectOfType<J_ItemManager>();
    }
    // Update is called once per frame
    void Update()
    {
        Armor_Text.text = "X "+ J_ItemManager.instance.remainArmor.ToString();


        if (Input.GetKey(KeyCode.Tab))
        {
           // Debug.Log(Armor_Text.text);

            MyMoney = J_ItemManager.instance.remainMoney;
            myArmor = J_ItemManager.instance.remainArmor;
            myPotion = J_ItemManager.instance.remainPotion;
            myGranade = J_ItemManager.instance.remainGrenade;
            myBullet = J_ItemManager.instance.ammoRemain;
            Potion_Text.text = J_ItemManager.instance.remainPotion.ToString();
            Granade_Text.text = J_ItemManager.instance.remainGrenade.ToString();


            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;


            ItemButtons.SetActive(true);
            Time.timeScale = 0.2f;

        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;


            Time.timeScale = 1f;

            ItemButtons.SetActive(false);

        }


        StateProcess();
        
    }
    void ChangeState(STATE s)
    {
        if (s == myState) return;
        myState = s;
        switch (myState)
        {
            case STATE.HAND:
                NowWeaponImage.sprite = ItemImages[3].GetComponent<Image>().sprite;
                swtichWeapon.ChangeState(J_SwtichWeapon.HOLDING_WEAPON.FIST);

                break;
            case STATE.RIFLE:
                NowWeaponImage.sprite = ItemImages[0].GetComponent<Image>().sprite;
                swtichWeapon.ChangeState(J_SwtichWeapon.HOLDING_WEAPON.GUN);

                break;
            case STATE.AXE:
                NowWeaponImage.sprite = ItemImages[1].GetComponent<Image>().sprite;
                swtichWeapon.ChangeState(J_SwtichWeapon.HOLDING_WEAPON.AXE);
                Debug.Log(swtichWeapon.myWeapon);


                break;
            case STATE.GRENADE:
                NowWeaponImage.sprite = ItemImages[2].GetComponent<Image>().sprite;
                swtichWeapon.ChangeState(J_SwtichWeapon.HOLDING_WEAPON.GRENADE);

                break;

        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.HAND:

                AmmoText.text = "";
                //Debug.Log("Hand");

                break;
            case STATE.RIFLE:
                AmmoText.text = J_ItemManager.instance.magAmmo.ToString() + " / " + J_ItemManager.instance.ammoRemain.ToString();

                //Debug.Log("RIFLE");
                break;
            case STATE.AXE:
                //Debug.Log("AXE");
                AmmoText.text = ""; 

                break;
            case STATE.GRENADE:
                //Debug.Log("GRENADE");
                AmmoText.text = J_ItemManager.instance.remainGrenade.ToString();

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
        //Debug.Log("Drink Potion!!");
        
        if(J_ItemManager.instance.remainPotion >0)
        {
            MN_UIManager.Instance.UsePotion(20f);
            J_ItemManager.instance.remainPotion--; 
        }
        else
            Debug.Log("포션부족");
        

    }
}
