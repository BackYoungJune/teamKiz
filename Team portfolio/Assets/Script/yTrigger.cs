using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public yEnemySpawner mySpawner;
    public int RemainZombies;
    bool TriggerOn = false;
    public bool Enabled = true;
    void Awake()
    {
        if(Enabled == false)
        {
            this.GetComponent<BoxCollider>().enabled = false;
        }
        mySpawner = this.GetComponentInParent<yEnemySpawner>();
    }

    private void Update()
    {
        if(TriggerOn)
        {
            RemainZombies = mySpawner.enemies.Count;
            if (RemainZombies <= 0)
            {
                Enabled = false;
                TriggerOn = false;
                // 돈 생성
                SpawnMoney();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Trigger Enter");
            mySpawner.SpawnWave();
            //Destroy(this);
            this.GetComponent<BoxCollider>().enabled = false;
            TriggerOn = true;
        }
    }

    private void SpawnMoney()
    {
        Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        // 플레이어의 정면에 생성
        Vector3 pos = playerTransform.forward * 7 + playerTransform.position;
        GameObject money = Instantiate(Resources.Load("Prefabs/item_Money"), pos, Quaternion.Euler(new Vector3(-90.0f, 0f, 0f))) as GameObject;
        money.GetComponent<Rigidbody>().AddForce(Vector3.up * 30);
    }
}
