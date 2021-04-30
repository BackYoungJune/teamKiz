using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yEnemyHead : MonoBehaviour
{
    public yEnemy enemy;
    public GameObject Blood;
    public Transform[] Hair;

    public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        enemy.OnDamage(damage, hitPoint, hitNormal);
        Instantiate(Blood, transform.position, transform.rotation);
        if (enemy.dead)
        {
            Collider[] enemyColliders = GetComponents<Collider>();
            for (int i = 0; i < enemyColliders.Length; i++)
            {
                //유석 추가 
                //J_ItemManager.instance.remainScore += 100;
                J_ItemManager.instance.IsHeadShotKill = true;
                Debug.Log("HeadShot!!!!");

                enemyColliders[i].enabled = false;
            }
            Destroy(gameObject);
            for(int i = 0; i < Hair.Length; i++)
            {
                Destroy(Hair[i].gameObject);
            }

            //Destroy(Hair.gameObject);
            //Destroy(Eyes.gameObject);
        }
    }

}
