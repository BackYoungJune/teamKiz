using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_VendingMachine : MonoBehaviour
{
    public void OpenStore()
    {
        // 상점 여는 코드 추가
        Debug.Log("Open Store");
    }
    void CloseStore()
    {
        // 상점을 닫을 때 추가할 코드
        //FindObjectOfType<J_ActionController>().storeOpened = false;
    }
}
