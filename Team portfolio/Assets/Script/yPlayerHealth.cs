using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 코드

public class yPlayerHealth : yLivingEntity
{
    public Animator myAnim;     // 애니메이션 컴포넌트
    public yPlayerMovement playerMovement; // 플레이어 움직임 컴포넌트
    public yPlayerShooter playerShooter; // 플레이어 슈터 컴포넌트

    public int startShield = 3;     // 시작 보호막 갯수
    public int shield { get; protected set; } // 현재 보호막 갯수

    /* -유석- 체력 UI 받기*/
    /* -유석- 보호막 UI 받기*/
    // Start is called before the first frame update
    void Start()
    {
        // 사용할 컴포넌트를 가져오기
        myAnim = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<yPlayerMovement>();
        playerShooter = GetComponentInChildren<yPlayerShooter>();
        /* 체력 UI 컴포넌트 가져오기*/
        MN_UIManager.Instance.UpdatePlayerHealth(startHealth);

    }

    protected override void OnEnable()
    {
        // LivingEntity의 OnEnable() 실행 (상태 초기화)
        base.OnEnable();

        // 보호막을 시작 보호막으로 초기화
        shield = startShield;

        /* 체력 UI 최대 HP = startingHealth, 현재 HP = health로 받기 */
        /* 보호막 UI 최대 Shield = startingShield, 현재 HP = shield로 받기 */

        //Debug.Log(startHealth);
    }

    // 체력 회복
    public override void RestoreHealth(float newHealth)
    {
        // LivingEntity의 RestoreHealth() 실행 (체력 증가)
        base.RestoreHealth(newHealth);
        Debug.Log("base.health "+ base.health);
        /* 체력 UI갱신 */
        //UpManage.Updat()
    }

    // 보호막 갯수 증가시키는 기능
    public void RestoreShield(int newShield)
    {
        // 이미 사망한 경우 리턴한다
        if (dead) return;

        // 보호막 갯수 증가
        shield += newShield;
    }

    // 데미지 처리
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        // 쉴드가 존재하면
        if (shield > 0)
        {
            // 데미지의 반만큼 체력 감소
            health -= damage / 2;
            // 보호막 -1
            shield--;
        }
        // 쉴드가 존재하지 않으면
        else
        {
            // 데미지만큼 체력 감소
            health -= damage;
        }

        // LivingEntity의 OnDamage() 실행(데미지 적용)
        base.OnDamage(damage, hitPoint, hitDirection);
        //MN_UIManager.Instance.IsHit = true;

        // 애니메이터의 Hit 트리거를 발동시켜 Hit 애니메이션 재생
        myAnim.SetTrigger("Hit");

        /* 체력 UI갱신 */ //  + potion먹으면
        /* 보호막 UI갱신 */
        MN_UIManager.Instance.UpdatePlayerHealth(-damage);
        //RestoreHealth(MN_UIManager.Instance.CurrentHealth);
    }

    // 사망 처리
    public override void Die()
    {
        // LivingEntity의 Die() 실행(사망 적용)
        base.Die();

        /* 체력 UI비활성화 */
        /* 보호막 UI비활성화 */

        // 애니메이터의 Die 트리거를 발동시켜 사망 애니메이션 재생
        myAnim.SetTrigger("Die");

        // 플레이어 조작을 받는 컴포넌트 비활성화
        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }
}
