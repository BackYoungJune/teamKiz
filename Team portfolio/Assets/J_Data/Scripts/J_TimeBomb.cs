using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_TimeBomb : MonoBehaviour
{
    public GameObject explosionEffect;

    float timer = 10.0f;
    bool IsPlanted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlantBomb();
    }

    void PlantBomb()
    {
        if (IsPlanted) return;

        if(Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(SetTimer());
        }
    }

    IEnumerator SetTimer()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            // 남은 시간 표시 
        }
        else
        {
            Explosion();
        }
        yield return null;
    }

    void Explosion()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

    }
}
