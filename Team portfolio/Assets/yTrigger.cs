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
}
