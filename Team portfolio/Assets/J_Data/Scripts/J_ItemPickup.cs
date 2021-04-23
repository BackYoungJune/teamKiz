using System.Collections;
using System.Collections.Generic;
using UnityEngine;

        
public class J_ItemPickup : MonoBehaviour, J_IItem
{
    public J_Item item;

    public void Use(GameObject target)
    {
        if(item.amount != 0)
        {
            // 포션은 J_ItemManager 에서 따로 처리
            //if (item.itemType == J_Item.ItemType.Used)
            //{
            //    yPlayerHealth playerHealth = target.GetComponentInParent<yPlayerHealth>();
            //    playerHealth.RestoreHealth(item.restore);
            //    //target.GetComponentInParent<yPlayerHealth>().RestoreHealth(item.amount);
            //    Debug.Log("아이템 사용");
            //}

            if (item.itemType == J_Item.ItemType.Ammo)
            {
                J_ItemManager itemManager = FindObjectOfType<J_ItemManager>();
                itemManager.ammoRemain += item.restore;     // 남은 전체 탄약 추가
                MN_UIManager.Instance.UpdateAmmos(itemManager.ammoRemain, itemManager.magAmmo);     // 아이템 획득 시 UI 갱신
            }

            if (item.itemType == J_Item.ItemType.Equiment)
            {
                // 아머 수치 증가
            }

            if(item.itemType == J_Item.ItemType.Grenade)
            {
                // 폭탄 사용..?
            }

            item.amount--;
        }
        else
        {
            Debug.Log("아이템 없음");
        }

        Debug.Log("잔여 개수" + item.amount);
        //Destroy(gameObject);
    }

}
