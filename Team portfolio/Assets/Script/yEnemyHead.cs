using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yEnemyHead : MonoBehaviour
{
    public yEnemy enemy;

    public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        enemy.OnDamage(damage, hitPoint, hitNormal);

        if (enemy.dead)
        {
            Collider[] enemyColliders = GetComponents<Collider>();
            for (int i = 0; i < enemyColliders.Length; i++)
            {
                enemyColliders[i].enabled = false;
            }
        }
    }

}
