using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_VendingMachine : MonoBehaviour
{
    public void OpenStore()
    {
        // 상점 여는 코드 추가
        Debug.Log("Open Store");
        StoreController store = FindObjectOfType<StoreController>();
        store.ChangeState(StoreController.STATE.NORAML);
    }

    public void DestroyMachine()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;     // 로테이션, 포지션 고정 해제
        rb.AddForce(Vector3.forward * 120.0f);
    }
}
