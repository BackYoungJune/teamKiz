﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yGrenade : MonoBehaviour
{
    public float damage = 100.0f;        // 수류탄 데미지
    public GameObject ExplosionEffect;      // 수류탄이 터졌을 때 이펙트

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explosion());
    }

    // Update is called once per frame
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3.0f);

        // SphereCastAll - 구체 모양의 레이캐스팅(모든 오브젝트)
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 10, Vector3.up, 0, LayerMask.GetMask("Enemy"));

        foreach (RaycastHit hit in rayHits)
        {
            // 첫번째 Enemy와 충돌한 경우
            if (hit.transform.gameObject.tag == "Enemy")
            {
                Debug.Log("enemy");
                // 레이가 어떤 물체와 충돌한 경우

                // 충돌한 상대방으로부터 IDamageable 오브젝트 가져오기 시도
                IDamageable target = hit.collider.GetComponent<IDamageable>();

                // Enemy가 튕겨나오게 하는 함수
                hit.transform.GetComponent<yEnemy>().HitByGrenade(transform.position);

                // 상대방으로부터 IDamageable 오브젝트를 가져오는 데 성공했다면
                if (target != null)
                {
                    // 상대방의 OnDamage 함수를 실행시켜 상대방에 데미지 주기
                    target.OnDamage(damage, hit.point, hit.normal);
                    // damaage - 탄알의 데미지,  hit.point - 레이가 충돌한 위치, hit.normal - 레이가 충돌한 표면의 방향
                }
            }
            
        }
        GameObject obj =  Instantiate(ExplosionEffect, transform.position, transform.rotation);
        Debug.Log(obj);
    }
}