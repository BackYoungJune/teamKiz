using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;    // 습득 가능한 최대 거리

    private bool pickupActivated = false;   // 습득 가능할 시 true

    private bool ignitionActivated = false;

    private RaycastHit hitInfo;     // 충돌체 정보 저장

    // 아이템 레이어에만 반응하도록 레이어 마스크를 설정
    [SerializeField]
    private LayerMask layerMask;

    //필요한 컴포넌트
    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Text explodeText;

    yPlayerInput playerInput;

    J_ItemManager itemManager;

    private void Start()
    {
        playerInput = GetComponentInParent<yPlayerInput>();
        itemManager = FindObjectOfType<J_ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckItem();
        Action();
    }

    private void Action()
    {
        if(playerInput.interact)
        {
            CheckItem();
            CanPickUP();
            CanExplodable();
        }
    }

    private void CanPickUP()
    {
        if(pickupActivated)
        {
            if(hitInfo.transform != null)
            {
                J_Item hitItem = hitInfo.transform.GetComponent<J_ItemPickup>().item;
                hitItem.amount++;       // 아이템 갯수 증가
                // pick ammo
                if(hitItem.itemType == J_Item.ItemType.Ammo)
                {
                    hitInfo.transform.GetComponent<J_ItemPickup>().Use(this.gameObject);
                }
                // pick armour
                if(hitItem.itemType == J_Item.ItemType.Equiment)
                {
                    GetComponentInParent<yPlayerHealth>().RestoreShield(1);
                }
                // pick money
                if(hitItem.itemType == J_Item.ItemType.Etc)
                {
                    J_ItemManager.instance.remainMoney += hitItem.restore;                
                }

                Debug.Log(hitInfo.transform.GetComponent<J_ItemPickup>().item.itemName + " 획득 ");
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();

                //Debug.Log("포션: " + itemManager.GetPotionAmount());
                //Debug.Log("총알: " + itemManager.GetAmmoAmount());
                //Debug.Log("수류탄: " + itemManager.GetGrenadeAmount());
            }
        }
    }
    private void CanExplodable()
    {
        if(ignitionActivated)
        {
            Debug.Log("set bool");
            hitInfo.transform.GetComponent<J_TimeBomb>().SetPlanted(true);
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }

            if(hitInfo.transform.tag == "Explodable")
            {
                explodeText.gameObject.SetActive(true);
                explodeText.text = "E를 눌러 기폭";
            }
        }
        else
        {
            InfoDisappear();
        }
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<J_ItemPickup>().item.itemName + " 획득 " + "<color=yellow>" + "E" + "</color>";
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
        explodeText.gameObject.SetActive(false);
    }
}
