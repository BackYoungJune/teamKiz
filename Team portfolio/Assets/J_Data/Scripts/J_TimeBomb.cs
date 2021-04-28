using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_TimeBomb : MonoBehaviour
{
    public GameObject explosionEffect;

    float timer = 5.0f;
    bool IsPlanted = false;
    public void SetPlanted(bool b) { this.IsPlanted = b; }

    // Update is called once per frame
    void Update()
    {
        if(IsPlanted)
        {
            PlantBomb();
        }
    }

    void PlantBomb()
    {
        StartCoroutine(SetTimer());
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
            IsPlanted = false;
        }
        yield return null;
    }

    void Explosion()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        GetComponent<J_Explode>().IndirectExplosion(transform.position);
        Destroy(gameObject);

        //

    }
}
