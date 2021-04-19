using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yRiple : MonoBehaviour
{
    // 총의 상태를 표현하는데 사용할 타입을 선언한다
    public enum STATE
    {
        READY, // 발사 준비됨
        EMPTY, // 탄창이 빔
        RELOADING // 재장전 중
    }

    public STATE myState = STATE.READY;

    public Transform fireTransform; // 총알이 발사될 위치
    public Transform BulletCaseTransform;   // 탄피가 발사될 위치
    public Transform muzzleFlashTransform;  // 총구 화염 효과가 발사될 위치
    public GameObject muzzleFlashEffect; // 총구 화염 효과

    public int ammoRemain = 100; // 남은 전체 탄약
    public int magCapacity = 25; // 탄창 용량
    public int magAmmo; // 현재 탄창에 남아있는 탄약

    public float timeBetFire = 0.12f; // 총알 발사 간격
    public float reloadTime = 1.8f; // 재장전 소요 시간
    float lastFireTime; // 총을 마지막으로 발사한 시점

    public GameObject bullet;       // Bullet Gameobjet
    public GameObject bulletCase;   // 탄피 Gameobject

    // Start is called before the first frame update
    void OnEnable()
    {
        // 총 상태 초기화
        // 현재 탄창을 가득 채우기

        magAmmo = magCapacity;

        // 총알 채워진거 UI로 가져오기 
        MN_UIManager.Instance.ammo = magAmmo;

        // 총의 현재 상태를 총을 쏠 준비가 된 상태로 변경
        myState = STATE.READY;
        // 마지막으로 총을 쏜 시점을 초기화
        lastFireTime = 0;
    }

    // 발사 시도
    public bool Fire()
    {
        // 현재 상태가 발사 가능한 상태
        // && 마지막 총 발사 시점에서 timeBetFire 이상의 시간이 지남
        if (myState == STATE.READY && Time.time >= lastFireTime + timeBetFire)
        {
            // 마지막 총 발사 시점 갱신
            lastFireTime = Time.time;
            // 실제 발사 처리 실행
            Shot();
            return true;
        }
        return false;
    }

    void Shot()
    {
        // 총알 발사
        GameObject instantBullet = Instantiate(bullet, fireTransform.position, fireTransform.rotation);
        yBullet2 Bullet = instantBullet.GetComponent<yBullet2>();
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = fireTransform.forward * 1000;
        // 탄피 배출
        GameObject intantCase = Instantiate(bulletCase, BulletCaseTransform.position, BulletCaseTransform.rotation);
        Rigidbody CaseRigid = instantBullet.GetComponent<Rigidbody>();
        Vector3 caseVec = BulletCaseTransform.forward * Random.Range(-3, -1) + Vector3.up * Random.Range(1, 3);
        CaseRigid.AddForce(caseVec, ForceMode.Impulse);
        CaseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);

        // 발사 이펙트 재생 시작
        StartCoroutine(ShotEffect(Bullet.hitPosition));
        // 남은 탄알 수를 -1
        magAmmo--;
        MN_UIManager.Instance.ammo = magAmmo;
        if (magAmmo <= 0)
        {
            // 탄창에 남은 탄알이 없다면 총의 현재 상태를 Empty로 갱신
            MN_UIManager.Instance.ammo = 0;

            myState = STATE.EMPTY;

        }
    }

    // 발사 이펙트와 소리를 재생하고 총알 궤적을 그린다
    IEnumerator ShotEffect(Vector3 hitPosition)
    {
        // 총구 화염 
        Instantiate(muzzleFlashEffect, muzzleFlashTransform.position, muzzleFlashTransform.rotation);

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);
    }

    // 재장전 시도
    public bool Reload()
    {
        if (myState == STATE.RELOADING || ammoRemain <= 0 || magAmmo >= magCapacity)
        {
            // 이미 재장전 중이거나 남은 탄알이 없거나
            // 탄창에 탄알이 이미 가득한 경우 재장전할 수 없음
            return false;
        }

        // 재장전 처리 시작

        StartCoroutine(ReloadRoutine());
        return true;
    }

    // 실제 재장전 처리를 진행
    IEnumerator ReloadRoutine()
    {
        // 현재 상태를 재장전 중 상태로 전환
        myState = STATE.RELOADING;

        // 재장전 소요 시간 만큼 처리를 쉬기
        yield return new WaitForSeconds(reloadTime);

        // 탄창에 채울 탄알을 계산
        int ammoToFill = magCapacity - magAmmo;

        // 탄창에 채워야 할 탄알이 남은 탄알보다 많다면
        // 채워야 할 탄알 수를 남은 탄알 수에 맞춰 줄임
        if (ammoRemain < ammoToFill)
        {
            ammoToFill = ammoRemain;
        }

        // 탄창을 채움
        magAmmo += ammoToFill;
        // 남은 탄알에서 탄창에 채운만큼 탄알을 뺌
        ammoRemain -= ammoToFill;

        // 총의 현재 상태를 발사 준비된 상태로 변경
        myState = STATE.READY;
        MN_UIManager.Instance.ammo = magAmmo;

    }
}
