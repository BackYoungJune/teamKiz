using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_ItemManager : MonoBehaviour
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

    [SerializeField]
    Text Granade_Text;
    [SerializeField]
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
    public int remainArmor { get; set; }

    // money
    public int remainMoney { get; set; }

    // 유석 추가 
    // score
    public int remainScore { get; set; }
    public bool IsHeadShotKill { get; set; }
    //public int PlusScore { get; set; }
    public void Use(yPlayerHealth target)
    {
        if(remainPotion > 0)
        {
            yPlayerHealth playerHealth = target.GetComponentInParent<yPlayerHealth>();
            playerHealth.RestoreHealth(potionInfo.restore);
           // playerHealth.RestoreHealth(20f);
           //J_ItemManager.instance.
            remainPotion--;
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
    private void Start()
    {
        // 게임 시작시 아이템 개수 셋팅
        ammoRemain = 200;
        magCapacity = 25;
        magAmmo = magCapacity;
        remainGrenade = 2;
        remainPotion = 5;
        remainArmor = 3;
        //유석 추가
        //PlusScore = 100;
        remainMoney = 1000;
        remainScore = 0;
        IsHeadShotKill = false;

        if (Potion_Text != null && Granade_Text != null)
        {
            Potion_Text.text = remainPotion.ToString();
            Granade_Text.text = remainGrenade.ToString();
        }

        J_DataManager.instance.SaveItemDataToJson();

    }

    private void Update()
    {

    }
}
