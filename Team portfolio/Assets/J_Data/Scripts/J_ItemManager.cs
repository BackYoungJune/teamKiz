using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_ItemManager : MonoBehaviour
{
    [SerializeField]
    J_Item potionInfo;
    [SerializeField]
    J_Item ammoInfo;
    [SerializeField]
    J_Item grenadeInfo;
    
    public int GetPotionAmount() { return potionInfo.amount; }
    public int GetAmmoAmount() { return ammoInfo.amount; }
    public int GetGrenadeAmount() { return grenadeInfo.amount; }



    // potion
    public int remainPotion;

    // ammo
    public int remainAmmo;
    public int magCapacity;
    public int magAmmo;

    // grenade
    public int remainGrenade;

    // amour
    public int remainAmour;


    //AddItem(): 
    // Start is called before the first frame update
    void Start()
    {
        //// 게임 시작시 아이템 개수 셋팅
        //ammoInfo.amount = 100;        // 총알 수
        //potionInfo.amount = 10;     // 포션 수
        //grenadeInfo.amount = 3;     // 수류탄 수 


    }
}
