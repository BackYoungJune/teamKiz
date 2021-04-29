using System.Collections;
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

        itemData.s_wave = FindObjectOfType<yEnemySpawner>().wave;   // wave 저장

        // spawnTrigger 저장
        GameObject trigger = GameObject.Find("Triggers");
        yTrigger[] spawnTriggers = trigger.GetComponentsInChildren<yTrigger>();
        for (int i = 0; i < spawnTriggers.Length; i++)
            itemData.s_spawnTriggers[i] = spawnTriggers[i].Enabled;


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

        FindObjectOfType<yEnemySpawner>().wave = itemData.s_wave;

        GameObject trigger = GameObject.Find("Triggers");
        yTrigger[] spawnTriggers = trigger.GetComponentsInChildren<yTrigger>();
        for (int i = 0; i < spawnTriggers.Length; i++)
            spawnTriggers[i].Enabled = itemData.s_spawnTriggers[i];



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

    public int s_wave;
    public bool[] s_spawnTriggers;
    // Spawn Trigger List 추가
    // 특정 Spawn Trigger 가 한 번 활성화 되었으면 false로 변경해서 작동하지 않도록?
}
