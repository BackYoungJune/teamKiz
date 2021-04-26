﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class J_DataManager : MonoBehaviour
{
    public ItemData itemData;


    // 현재 아이템 정보를 Json 파일로 저장
    [ContextMenu("Save ItemData")]
    void SaveItemDataToJson()
    {
        itemData.s_remainPotion = J_ItemManager.instance.remainPotion;
        itemData.s_ammoRemain = J_ItemManager.instance.ammoRemain;
        itemData.s_magCapacity = J_ItemManager.instance.magCapacity;
        itemData.s_magAmmo = J_ItemManager.instance.magAmmo;
        itemData.s_remainGrenade = J_ItemManager.instance.remainGrenade;
        itemData.s_remainArmor = J_ItemManager.instance.remainArmor;
        itemData.s_remainMoney = J_ItemManager.instance.remainMoney;

        string jsonData = JsonUtility.ToJson(itemData, true);
        string path = Path.Combine(Application.dataPath, "J_Data/itemData.json");
        File.WriteAllText(path, jsonData);
    }

    // 아이템 정보를 불러옴
    [ContextMenu("Load ItemData")]
    void LoadItemDataToJson()
    {
        J_ItemManager.instance.remainPotion =itemData.s_remainPotion;
        J_ItemManager.instance.ammoRemain = itemData.s_ammoRemain;
        J_ItemManager.instance.magCapacity = itemData.s_magCapacity;
        J_ItemManager.instance.magAmmo = itemData.s_magAmmo;
        J_ItemManager.instance.remainGrenade = itemData.s_remainGrenade;
        J_ItemManager.instance.remainArmor = itemData.s_remainArmor;
        J_ItemManager.instance.remainMoney = itemData.s_remainMoney;

        string path = Path.Combine(Application.dataPath, "J_Data/itemData.json");
        string jsonData = File.ReadAllText(path);
        itemData = JsonUtility.FromJson<ItemData>(jsonData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class ItemData
{
    public int s_remainPotion;
    public int s_ammoRemain;
    public int s_magCapacity;
    public int s_magAmmo;
    public int s_remainGrenade;
    public int s_remainArmor;
    public int s_remainMoney;
}