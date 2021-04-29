using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class J_DataManager : MonoBehaviour
{
    public ItemData itemData;
    public PlayData playData;


    private static J_DataManager m_instance;
    public static J_DataManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<J_DataManager>();
            }
            return m_instance;
        }
    }

    // 현재 아이템 정보를 Json 파일로 저장
    [ContextMenu("Save ItemData")]
    public void SaveItemDataToJson()
    {
        // 아이템 개수 저장
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
    public void LoadItemDataFromJson()
    {
        string path = Path.Combine(Application.dataPath, "J_Data/itemData.json");
        string jsonData = File.ReadAllText(path);
        itemData = JsonUtility.FromJson<ItemData>(jsonData);

        J_ItemManager.instance.remainPotion = itemData.s_remainPotion;
        J_ItemManager.instance.ammoRemain = itemData.s_ammoRemain;
        J_ItemManager.instance.magCapacity = itemData.s_magCapacity;
        J_ItemManager.instance.magAmmo = itemData.s_magAmmo;
        J_ItemManager.instance.remainGrenade = itemData.s_remainGrenade;
        J_ItemManager.instance.remainArmor = itemData.s_remainArmor;
        J_ItemManager.instance.remainMoney = itemData.s_remainMoney;

    }


    // 플레이 데이터를 Json으로 저장
    public void SavePlayDataToJson()
    {
        // 현재 웨이브 저장
        playData.s_wave = FindObjectOfType<yEnemySpawner>().wave;   // wave 저장

        // spawnTrigger 저장
        GameObject trigger = GameObject.Find("Triggers");
        yTrigger[] spawnTriggers = trigger.GetComponentsInChildren<yTrigger>();
        for (int i = 0; i < spawnTriggers.Length; i++)
            playData.s_spawnTriggers[i] = spawnTriggers[i].Enabled;

        // 현재 위치 저장
        playData.s_pos = GameObject.Find("Player").GetComponent<Transform>().position;


        string jsonData = JsonUtility.ToJson(playData, true);
        string path = Path.Combine(Application.dataPath, "J_Data/playData.json");
        File.WriteAllText(path, jsonData);
    }

    // 플레이 데이터를 불러옴
    public void LoadPlayDataFromJson()
    {
        string path = Path.Combine(Application.dataPath, "J_Data/playData.json");
        string jsonData = File.ReadAllText(path);
        playData = JsonUtility.FromJson<PlayData>(jsonData);


        FindObjectOfType<yEnemySpawner>().wave = playData.s_wave;

        GameObject trigger = GameObject.Find("Triggers");
        yTrigger[] spawnTriggers = trigger.GetComponentsInChildren<yTrigger>();
        for (int i = 0; i < spawnTriggers.Length; i++)
            spawnTriggers[i].Enabled = playData.s_spawnTriggers[i];

        GameObject.Find("Player").GetComponent<Transform>().position = playData.s_pos;
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

public class PlayData
{
    public int s_wave;              // 웨이브 정보
    public bool[] s_spawnTriggers;  // 스폰트리거 정보

    public Vector3 s_pos;           // 플레이어 위치 정보
}
