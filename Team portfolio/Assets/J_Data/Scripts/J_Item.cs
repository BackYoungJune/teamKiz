using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class J_Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public GameObject itemPrefab;
    public int amount;

    public string weaponType;

    public ItemType itemType;
    public enum ItemType
    {
        Used,
        Equiment,
        Etc
    }
}
