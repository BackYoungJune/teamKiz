﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;    // 습득 가능한 최대 거리

    private bool pickupActivated = false;   // 습득 가능할 시 true

    private RaycastHit hitInfo;     // 충돌체 정보 저장

    // 아이템 레이어에만 반응하도록 레이어 마스크를 설정
    [SerializeField]
    private LayerMask layerMask;

    //필요한 컴포넌트
    [SerializeField]
    private Text actionText;
    

    // Update is called once per frame
    void Update()
    {
        CheckItem();
        Action();
    }

    private void Action()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUP();
        }
    }

    private void CanPickUP()
    {
        if(pickupActivated)
        {
            if(hitInfo.transform != null)
            {
                J_Item hitItem = hitInfo.transform.GetComponent<J_ItemPickup>().item;
                // 아이템 인벤토리에 추가(AddInventory()) or 체력회복
                //GetComponent<yPlayerHealth>().RestoreHealth(hitItem.amount);
                Debug.Log(hitInfo.transform.GetComponent<J_ItemPickup>().item.itemName + " 획득 ");
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
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
    }
}
