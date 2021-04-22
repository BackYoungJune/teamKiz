using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int remainAmmo { get; set; }
    public int magCapacity { get; set; }
    public int magAmmo { get; set; }

    // grenade, yGrenade.cs
    public int remainGrenade { get; set; }

    // amour(shield), yPlayerHealth.cx
    public int remainAmour { get; set; }


    //AddItem(): 
    // Start is called before the first frame update
    private void Start()
    {
        // 게임 시작시 아이템 개수 셋팅
        //ammoInfo.amount = 100;        // 총알 수
        //potionInfo.amount = 10;     // 포션 수
        //grenadeInfo.amount = 3;     // 수류탄 수 
        //remainPotion = 5;
    }

    private void Update()
    {
        //remainPotion = potionInfo.amount;
    }
}
