using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Money : MonoBehaviour
{
    public J_Item item;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            J_ItemManager.instance.remainMoney += item.restore;
            Destroy(gameObject);
        }
    }
}
