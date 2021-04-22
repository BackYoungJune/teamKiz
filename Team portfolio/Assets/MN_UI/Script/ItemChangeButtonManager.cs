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

    J_SwtichWeapon swtichWeapon;

    bool IsRifle = true;
    private void Awake()
    {
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

        //수류탄 개수에 사용할 변수
        Granade_Text.text = MN_UIManager.Instance.Granade.ToString();

        //포션 개수에 사용할 변수
        Potion_Text.text = "";

        ItemButtons.SetActive(false);

        // 무기 스위칭에 사용할 변수
        swtichWeapon = FindObjectOfType<J_SwtichWeapon>();
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
        //yPlayerAxe axe = FindObjectOfType<yPlayerAxe>();
        //yPlayerShooter rifle = FindObjectOfType<yPlayerShooter>();
        //yPlayerGrenade granade = FindObjectOfType<yPlayerGrenade>();
        //yPlayerMovement playerMovement = FindObjectOfType<yPlayerMovement>();
        //yPlayerInput playerInput = FindObjectOfType<yPlayerInput>();

        switch (myState)
        {
            case STATE.HAND:
                NowWeaponImage.sprite = ItemImages[3].GetComponent<Image>().sprite;
                swtichWeapon.ChangeState(J_SwtichWeapon.HOLDING_WEAPON.FIST);
                //axe.enabled = false;
                //rifle.enabled = false;
                //granade.enabled = false;

                //if (playerInput.swap0)
                //{
                //}
                //playerMovement.swap0();
                //NowWeaponImage.sprite = ItemImages[3].GetComponent<Image>().sprite;
                break;
            case STATE.RIFLE:
                NowWeaponImage.sprite = ItemImages[0].GetComponent<Image>().sprite;
                swtichWeapon.ChangeState(J_SwtichWeapon.HOLDING_WEAPON.GUN);
                //axe.enabled = false;
                //rifle.enabled = true;
                //granade.enabled = false;
                // AmmoText.text = MN_UIManager.Instance.ammo.ToString() + " / " + MN_UIManager.Instance.MaxAmmo.ToString();

                //if (playerInput.swap2)
                //{
                //}
                //playerMovement.swap1();
                //NowWeaponImage.sprite = ItemImages[0].GetComponent<Image>().sprite;
                break;
            case STATE.AXE:
                NowWeaponImage.sprite = ItemImages[1].GetComponent<Image>().sprite;
                swtichWeapon.ChangeState(J_SwtichWeapon.HOLDING_WEAPON.AXE);

                //AmmoText.text = "";

                //axe.enabled = true;
                //rifle.enabled = false;
                //granade.enabled = false;

                //if (playerInput.swap1)
                //{
                //}
                //playerMovement.swap2();
                //NowWeaponImage.sprite = ItemImages[1].GetComponent<Image>().sprite;
                break;
            case STATE.GRENADE:
                NowWeaponImage.sprite = ItemImages[2].GetComponent<Image>().sprite;
                swtichWeapon.ChangeState(J_SwtichWeapon.HOLDING_WEAPON.GRENADE);
                //AmmoText.text = Granade_Text.text = MN_UIManager.Instance.Granade.ToString();

                //axe.enabled = false;
                //rifle.enabled = false;
                //granade.enabled = true;

                //if (playerInput.swap3)
                //{
                //}
                //playerMovement.swap3();
                //NowWeaponImage.sprite = ItemImages[2].GetComponent<Image>().sprite;
                //AmmoText.text = "";
                break;
            case STATE.POTION:
                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.HAND:
                AmmoText.text = "";

                break;
            case STATE.RIFLE:
                AmmoText.text = MN_UIManager.Instance.ammo.ToString() + " / " + MN_UIManager.Instance.MaxAmmo.ToString();

                //Debug.Log("RIFLE");
                break;
            case STATE.AXE:
                //Debug.Log("AXE");
                AmmoText.text = "";

                break;
            case STATE.GRENADE:
                //Debug.Log("GRENADE");
                AmmoText.text = Granade_Text.text = MN_UIManager.Instance.Granade.ToString();

                break;
            case STATE.POTION:
                AmmoText.text = "";

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
