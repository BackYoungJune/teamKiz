using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Breakable : MonoBehaviour
{
    public GameObject DestroyedVersion;
    GameObject newDestroyedVersion;
    
    [SerializeField]
    GameObject[] itemList;

    void OnMouseDown()
    {
        newDestroyedVersion = Instantiate(DestroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
        DestroyDebris();
        // 아이템을 스폰
        SpawnItem(Random.Range(0, 2));
    }

    public void DestructObject()
    {
        newDestroyedVersion = Instantiate(DestroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
        DestroyDebris();
        SpawnItem(Random.Range(0, 2));
    }
    void DestroyDebris()
    {
    }

    void SpawnItem(int random)
    {
        switch (random)
        {
            case 0:
                Instantiate(itemList[0], transform.position, transform.rotation);
                break;
            case 1:
                Instantiate(itemList[1], transform.position, transform.rotation);
                break;
        }

    }

    // Fracture Object의 rigidbody 전부 제거하고
    // 총알과 충돌한 Fracture만 add rigidbody 해서 부서지도록
}
