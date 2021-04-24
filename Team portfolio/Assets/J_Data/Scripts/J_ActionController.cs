using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;                    // 습득 가능한 최대 거리

    private bool pickupActivated = false;   // 습득 가능할 시 true

    private bool ignitionActivated = false; // 폭파 가능할 시 ture

    private bool storeActivated = false;    // 상점을 열 수 있을 시 true
    public bool storeOpened = false;        // 상점이 열리면 true


    private RaycastHit hitInfo;             // 충돌체 정보 저장

    // 아이템 레이어에만 반응하도록 레이어 마스크를 설정
    [SerializeField]
    private LayerMask layerMask;

    //[SerializeField]
    //private Text actionText;

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
            }
        }
        else if(storeActivated)
        {
            // 상점이 열려있지 않을 때
            if(!storeOpened)
            {
                hitInfo.transform.GetComponent<J_VendingMachine>().OpenStore();     // 상점 열기
                InfoDisappear();
                storeOpened = true;
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
                ExplodableInfo();
            }

            if (hitInfo.transform.tag == "VendingMachine" && !storeOpened)
            {
                InteractVMachine();
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
        //actionText.gameObject.SetActive(true);
        //actionText.text = hitInfo.transform.GetComponent<J_ItemPickup>().item.itemName + " 획득 " + "<color=yellow>" + "E" + "</color>";
    }

    private void ExplodableInfo()
    {
        ignitionActivated = true;
        //actionText.gameObject.SetActive(true);
        //actionText.text = "기폭(E)";
    }
    private void InteractVMachine()
    {
        storeActivated = true;
        //actionText.gameObject.SetActive(true);
        //actionText.text = "상점열기(E)";
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        ignitionActivated = false;
        storeActivated = false;
        //actionText.gameObject.SetActive(false);
    }
}
