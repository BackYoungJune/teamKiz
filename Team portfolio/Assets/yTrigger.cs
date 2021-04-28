using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public yEnemySpawner mySpawner;
    void Awake()
    {
        mySpawner = this.GetComponentInParent<yEnemySpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Trigger Enter");
            mySpawner.SpawnWave();
            Destroy(this);
        }
    }
}
