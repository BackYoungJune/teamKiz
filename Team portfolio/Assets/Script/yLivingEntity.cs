using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class yLivingEntity : MonoBehaviour, IDamageable
{
    public float startHealth = 200f;    // 시작 체력
    public float health { get; protected set; } // 현재 체력
    public bool dead { get; protected set; }    // 사망 상태
    public event Action onDeath;    // 사망 시 발동할 이벤트

    //public float startShield = 100f;     // 시작 보호막
    //public float shield { get; protected set; } // 현재 보호막

    // 생명체가 활성화될때 상태를 리셋
    protected virtual void OnEnable()
    {
        // 사망하지 않은 상태로 시작
        dead = false;
        // 체력을 시작 체력으로 초기화
        health = startHealth;
    }

    // 데미지를 입는 기능
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        // 데미지만큼 체력 감소
        health -= damage;

        /* 나중에 쉴드 여기에 추가처리 */

        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    // 체력을 회복하는 기능
    public virtual void RestoreHealth(float newHealth)
    {
        // 이미 사망한 경우 리턴한다
        if (dead) return;

        // 체력 추가
        //health += newHealth;

        // 유석_수정 : ui manager의 current health를 불러옴
        health = newHealth;
    }

    // 사망 처리
    public virtual void Die()
    {
        // onDeath 이벤트 등록된 메서드가 있다면 실행
        if(onDeath != null)
        {
            onDeath();
        }

        // 사망상태로 변경
        dead = true;
    }

}
