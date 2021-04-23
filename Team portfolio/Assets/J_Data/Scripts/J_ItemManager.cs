using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_ItemManager : MonoBehaviour, J_IItem
{
    #region
    [SerializeField]
    J_Item potionInfo;
    [SerializeField]
    J_Item ammoInfo;
    [SerializeField]
    J_Item grenadeInfo;

    public int GetPotionAmount() { return potionInfo.amount; }
    public int GetAmmoAmount() { return ammoInfo.amount; }
    public int GetGrenadeAmount() { return grenadeInfo.amount; }
    #endregion


    Text Granade_Text;
    Text Potion_Text;

    private static J_ItemManager m_instance;
    public static J_ItemManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<J_ItemManager>();
            }
            return m_instance;
        }
    }

    // potion
    public int remainPotion { get; set; }

    // ammo, yRife.cs
    public int ammoRemain { get; set; }     // 남은 전체 탄약
    public int magCapacity { get; set; }    // 탄창 용량
    public int magAmmo { get; set; }        // 현재 탄창에 남아있는 탄약

    // grenade, yGrenade.cs
    public int remainGrenade { get; set; }

    // amour(shield), yPlayerHealth.cs
    public int remainAmour { get; set; }

    // money
    public int remainMoney { get; set; }


    public void Use(GameObject target)
    {
        if(remainPotion > 0)
        {
            yPlayerHealth playerHealth = target.GetComponentInParent<yPlayerHealth>();
            playerHealth.RestoreHealth(potionInfo.restore);
           // playerHealth.RestoreHealth(20f);
            remainPotion--;
            potionInfo.amount--;
            Debug.Log("포션 사용");
            Debug.Log("잔여 포션: " + remainPotion);
        }
        else
        {
            Debug.Log("아이템 부족");
            Debug.Log("잔여 포션: " + remainPotion);
        }
    }


    // Start is called before the first frame update
    private void Awake()
    {
        Granade_Text = GameObject.Find("Granade_Text").GetComponent<Text>();
        Potion_Text = GameObject.Find("Potion_Text").GetComponent<Text>();

        // 게임 시작시 아이템 개수 셋팅
        ammoRemain = 200;
        magCapacity = 25;
        magAmmo = magCapacity;
        remainGrenade = 2;
        remainPotion = 5;
        potionInfo.amount = 5;
        remainAmour = 3;

        Granade_Text.text = remainGrenade.ToString();
        Potion_Text.text = remainPotion.ToString();

        //ammoInfo.amount = 100;        // 총알 수
        //potionInfo.amount = 10;     // 포션 수
        //grenadeInfo.amount = 3;     // 수류탄 수 
        //remainPotion = 5;

    }

    private void Update()
    {
        remainPotion = potionInfo.amount;
    }
}
