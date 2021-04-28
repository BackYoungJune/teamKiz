using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;                    // 상호작용 가능한 최대 거리
    
    [SerializeField]
    private LayerMask layerMask;            // 아이템 레이어에만 반응하도록 레이어 마스크를 설정

    [SerializeField]
    private Text actionText;                // info text

    private RaycastHit hitInfo;             // 충돌체 정보 저장

    private bool pickupActivated = false;   // 습득 가능할 시 true
    private bool ignitionActivated = false; // 폭파 가능할 시 ture
    private bool storeActivated = false;    // 상점을 열 수 있을 시 true
    public bool storeOpened = false;        // 상점이 열리면 true
    private bool holdActivated = false;     // 들 수 있을 때 true
    private bool isHolding = false;         // 들고 있으면 true
    private bool bombSet = false;

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
            CanHolding();
            SetTimeBomb();
        }
    }

    private void CanPickUP()
    {
        if(pickupActivated)
        {
            if(hitInfo.transform != null)
            {
                J_Item hitItem = hitInfo.transform.GetComponent<J_ItemPickup>().item;
                //hitItem.amount++;       // 아이템 갯수 증가
                // pick potion
                if(hitItem.itemType == J_Item.ItemType.Used)
                {
                    J_ItemManager.instance.remainPotion++;
                }
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
                // pick grenade
                if (hitItem.itemType == J_Item.ItemType.Grenade)
                {
                    J_ItemManager.instance.remainGrenade++;
                }
                // pick money
                if (hitItem.itemType == J_Item.ItemType.Etc)
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

    private void CanHolding()
    {
        if(hitInfo.transform != null && holdActivated)
        {
            Transform parent = GameObject.Find("holdPos").GetComponent<Transform>();
            GameObject child = hitInfo.transform.gameObject;
            Rigidbody rb = hitInfo.transform.GetComponent<Rigidbody>();
            //rb.useGravity = false;
            rb.isKinematic = true;  
            child.transform.parent = parent;
            child.transform.position = parent.position;
            rb.freezeRotation = true;
            isHolding = true;
        }
    }

    private void SetTimeBomb()
    {
        if(ignitionActivated && hitInfo.transform.tag == "Wall")
        {
            Transform parent = GameObject.Find("tntPos").GetComponent<Transform>();
            GameObject child = GameObject.Find("TimeBomb");
            child.transform.parent = parent;
            child.transform.position = parent.transform.position;
            child.transform.rotation = Quaternion.Euler(0.0f, 206.0f, 0.0f);
            child.GetComponent<J_TimeBomb>().SetPlanted(true);
            isHolding = false;
            bombSet = true;

            InfoDisappear();
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

            if(hitInfo.transform.tag == "Explodable" && !isHolding && !bombSet)
            {
                HoldInfo();
            }

            if (hitInfo.transform.tag == "VendingMachine" && !storeOpened)
            {
                InteractVMachine();
            }

            if(hitInfo.transform.tag == "Wall" && isHolding)
            {
                SetTimeBombInfo();
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

    private void HoldInfo()
    {
        holdActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = "Hold(E)";
    }

    private void SetTimeBombInfo()
    {
        ignitionActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = "폭탄설치(E)";
    }

    private void InteractVMachine()
    {
        storeActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = "상점열기(E)";
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        holdActivated = false;
        ignitionActivated = false;
        storeActivated = false;
        actionText.gameObject.SetActive(false);
    }
}
