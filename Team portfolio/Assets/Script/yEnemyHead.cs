using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yEnemyHead : yLivingEntity
{
    public yEnemy enemy;



    private void OnEnable()
    {
        startHealth = enemy.startHealth;
        health = enemy.health;
    }

    private void Update()
    {
        if(enemy)
        {
            health = enemy.health;
            Debug.Log(health);
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }


}
