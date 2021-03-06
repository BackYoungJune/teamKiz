using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yBullet2 : MonoBehaviour
{
    public float maxDistance = 1000.0f; // 최대 사정거리
    public GameObject decalHitWall;     // 총알이 Wall에 박혔을 때 Effect
    public GameObject bloodEffect;      // 총알이 Enemy에 박혔을 때 Effect

    public float damage = 25.0f;        // 총알 데미지
    public LayerMask ignoreLayer;       // 해당 레이어를 제외하고 Raycast 할예정

    public Vector3 hitPosition = Vector3.zero; // 탄알이 맞은 곳을 저장할 변수
    public bool bulletAlive = true;     // bullet이 파괴되었는지 알아보는 bool값

    void FixedUpdate()
    {
        RaycastHit[] hits; //Raycast데이터 저장용 배열 설정

        hits = Physics.RaycastAll(transform.position, transform.forward, maxDistance, ~ignoreLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            // 레이캐스트에 의한 충돌 정보를 저장하는 컨테이너
            RaycastHit hit = hits[i];

            // 첫번째 Enemy와 충돌한 경우
            if (hit.transform.gameObject.tag == "Enemy")
            {
                // 레이가 어떤 물체와 충돌한 경우
                // bolldEffect를 생성시키고 파괴
                Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));

                // 충돌한 상대방으로부터 IDamageable 오브젝트 가져오기 시도
                IDamageable target = hit.collider.GetComponent<IDamageable>();

                // 상대방으로부터 IDamageable 오브젝트를 가져오는 데 성공했다면
                if (target != null)
                {
                    // 상대방의 OnDamage 함수를 실행시켜 상대방에 데미지 주기
                    target.OnDamage(damage, hit.point, hit.normal);
                    // damaage - 탄알의 데미지,  hit.point - 레이가 충돌한 위치, hit.normal - 레이가 충돌한 표면의 방향

                }

                // 레이가 충돌한 위치 저장
                hitPosition = hit.point;
                // 파괴
                Destroy(gameObject);
                bulletAlive = false;
            }

            // 두번째 벽이나 바닥과 충돌한 경우
            else if (hit.transform.tag == "Floor" || hit.transform.tag == "Wall")
            {
                Instantiate(decalHitWall, hit.point, Quaternion.LookRotation(hit.normal));
                // 레이가 충돌한 위치 저장
                hitPosition = hit.point;
                // 파괴
                Destroy(gameObject);
                bulletAlive = false;
            }
        }

        // 나머지와 충돌하거나 아무것도 충돌 안할경우
        if (bulletAlive)
        {
            // 레이가 다른 물체와 충돌하지 않았다면
            // 탄알이 최대 사정거리까지 날아갔을 때의 위치를 충돌 위치로 사용
            hitPosition = transform.position + transform.forward * maxDistance;
            // 파괴
            Destroy(gameObject, 1.0f);
        }
    }
 }
