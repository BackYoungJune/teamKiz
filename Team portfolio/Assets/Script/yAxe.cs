using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yAxe : MonoBehaviour
{
    public float damage = 200.0f;        // 도끼 데미지
    public GameObject bloodEffect;      // 도끼가 Enemy에 박혔을 때 Effect
    public Vector3 hitPosition = Vector3.zero;  // 도끼를 맞앗을 때 맞은위치

    public float timeBetAttack = 1.5f; // 공격 간격
    private float lastAttackTime = 0f; // 마지막 공격 시점
    public yPlayerAxe playerAxe;


    private void OnTriggerStay(Collider other)
    {
        // 최근 공격 시점에서 timeBetAttack 이상 시간이 지났고 gameObject.Tag가 Enemy이고 플레이어가 마우스 왼쪽버튼을 눌렀다면 실행한다
        if (Time.time >= lastAttackTime + timeBetAttack && other.gameObject.tag == "Enemy" && playerAxe.Attack)
        {
            Debug.Log("enemy");
            // 충돌한 상대방으로부터 IDamageable 오브젝트 가져오기 시도
            IDamageable target = other.GetComponent<IDamageable>();
            // 상대방으로부터 IDamageable 오브젝트를 가져오는 데 성공했다면
            if (target != null)
            {
                // 최근 공격 시간 갱신
                lastAttackTime = Time.time;

                // 맞은 위치와 맞은 표면을 구한다
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;

                // bloodEffect를 생성하고 파괴시킨다
                Instantiate(bloodEffect, hitPoint, Quaternion.LookRotation(hitNormal));

                // 상대방의 OnDamage 함수를 실행시켜 상대방에 데미지 주기
                target.OnDamage(damage, hitPoint, hitNormal);
                // damaage - 도끼의 데미지,  hitPoint - 도끼가 맞은 표면의 근접한 위치, hitNormal - 도끼가 맞은 표면
                playerAxe.Attack = false;
            }
        }

        // 도끼로 상자를 부술 수 있도록
        if(other.tag == "BREAKABLE")
        {
            other.GetComponent<J_Breakable>().DestructObject();
        }
    }
}
