using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Breakable : MonoBehaviour
{
    public GameObject DestroyedVersion;

    void OnMouseDown()
    {
        Instantiate(DestroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
        //DestroyImmediate(DestroyedVersion, true);
        // 아이템을 스폰
        //Instantiate(Resources.Load("item_Bandage"), transform.position, transform.rotation);
        //Instantiate(Resources.Load("item_First_Aid_Kit"), transform.position, transform.rotation);
        //SpawnItem(Random.Range(0, 3));
    }

    //public void DestructObject()
    //{
    //    Instantiate(DestroyedVersion, transform.position, transform.rotation);
    //    Destroy(gameObject);
    //}


    // Fracture Object의 rigidbody 전부 제거하고
    // 총알과 충돌한 Fracture만 add rigidbody 해서 부서지도록
}
