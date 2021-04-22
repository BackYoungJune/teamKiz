using System.Collections;
using System.Collections.Generic;
using UnityEngine;

        
public class J_ItemPickup : MonoBehaviour, J_IItem
{
    public J_Item item;

    // ItemChangeButtonManager.ChangeState() 내에서 처리
    public void Use(GameObject target)
    {
        if(item.amount != 0)
        {
            if (item.itemType == J_Item.ItemType.Used)
            {
                yPlayerHealth playerHealth = target.GetComponentInParent<yPlayerHealth>();
                playerHealth.RestoreHealth(item.restore);
                //target.GetComponentInParent<yPlayerHealth>().RestoreHealth(item.amount);
                Debug.Log("아이템 사용");
            }

            if (item.itemType == J_Item.ItemType.Ammo)
            {
                //J_ItemManager itemManager = FindObjectOfType<J_ItemManager>();
                //Debug.Log(itemManager.remainAmmo);
                //itemManager.remainAmmo += 30;
                yRiple playerRiple = GameObject.Find("Ak-47").GetComponent<yRiple>();
                Debug.Log("기존 잔여 탄약: " + playerRiple.ammoRemain);
                playerRiple.ammoRemain += item.restore;     // 남은 전체 탄약 추가
                Debug.Log("획득 후 잔여 탄약: " + playerRiple.ammoRemain);
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
