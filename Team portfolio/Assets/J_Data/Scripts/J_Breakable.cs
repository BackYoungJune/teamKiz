using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Breakable : MonoBehaviour
{
    public GameObject DestroyedVersion;
    GameObject newDestroyedVersion;
    
    [SerializeField]
    GameObject[] itemList;

    public void DestructObject()
    {
        newDestroyedVersion = Instantiate(DestroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
        DestroyDebris();
        SpawnItem(Random.Range(0, 2));
    }
    void DestroyDebris()
    {
        Transform[] debirs = newDestroyedVersion.GetComponentsInChildren<Transform>();
        for (int i = 1; i < debirs.Length; i++)
        {
            Destroy(debirs[i].gameObject, 3.0f);
        }
    }

    void SpawnItem(int random)
    {
        if (itemList.Length == 0) return;

        Instantiate(itemList[Random.Range(0, 2)], transform.position, transform.rotation);
        Debug.Log("SpawnItem!!");
    }

    // Fracture Object의 rigidbody 전부 제거하고
    // 총알과 충돌한 Fracture만 add rigidbody 해서 부서지도록
}
