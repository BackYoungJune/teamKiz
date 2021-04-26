using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Barrel : MonoBehaviour
{
    public GameObject explosionEffect;
    //private int hitCount;

    private Rigidbody barrelRigid;

    public float explosionRadius = 10.0f;
    public float damage = 100.0f;

    [SerializeField]
    private LayerMask applyLayer;

    // Start is called before the first frame update
    void Start()
    {
        barrelRigid = GetComponent<Rigidbody>();
    }
    
    public void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        
        barrelRigid.mass = 5.0f;
        barrelRigid.AddForce(Vector3.up * 100.0f);

        IndirectExplosion(transform.position);
    }
    
    void IndirectExplosion(Vector3 pos)
    {
        // applyLayer 에 해당하는 오브젝트의 콜라이더를 얻어와 colls에 저장
        //Collider[] colls = Physics.OverlapSphereNonAlloc(pos, explosionRadius, applyLayer);

        int maxColliders = 30;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(pos, explosionRadius, hitColliders, applyLayer);
        
        for (int i = 0; i < numColliders; i++)
        {
            if(hitColliders[i].GetComponent<Rigidbody>())
            {
                var _rb = hitColliders[i].GetComponent<Rigidbody>();
                _rb.mass = 1.0f;
                _rb.AddExplosionForce(700.0f, pos, explosionRadius, 100.0f);
                //_rb.mass = 10.0f;
                Debug.Log(hitColliders[i]);
            }
            

            if (hitColliders[i].gameObject.tag == "BREAKABLE")
            {
                hitColliders[i].GetComponent<J_Breakable>().Invoke("DestructObject", 1.0f);
            }

            else if (hitColliders[i].gameObject.tag == "Player")
            {
                // 충돌한 상대방으로부터 IDamageable 오브젝트 가져오기 시도
                IDamageable target = hitColliders[i].GetComponent<IDamageable>();
                if (target != null)
                {
                    // 상대방의 OnDamage 함수를 실행시켜 상대방에 데미지 주기
                    target.OnDamage(damage, hitColliders[i].ClosestPoint(transform.position), transform.position - hitColliders[i].transform.position);
                    // damaage - 탄알의 데미지,  hit.point - 레이가 충돌한 위치, hit.normal - 레이가 충돌한 표면의 방향
                }
            }

            else if (hitColliders[i].gameObject.tag == "Enemy")
            {
                // 충돌한 상대방으로부터 IDamageable 오브젝트 가져오기 시도
                yEnemy target = hitColliders[i].gameObject.GetComponent<yEnemy>();
                Debug.Log(target);
                if (target != null)
                {
                    // 상대방의 OnDamage 함수를 실행시켜 상대방에 데미지 주기
                    target.OnDamage(damage, hitColliders[i].ClosestPoint(transform.position), transform.position - hitColliders[i].transform.position);
                    // damaage - 탄알의 데미지,  hit.point - 레이가 충돌한 위치, hit.normal - 레이가 충돌한 표면의 방향
                    target.HitByGrenade(transform.position);
                }
            }
        }

        //수집한 콜라이더에 폭발 효과 적용
        //foreach (var coll in colls)
        //{
        //    var _rb = coll.GetComponent<Rigidbody>();
        //    _rb.mass = 1.0f;
        //    _rb.AddExplosionForce(700.0f, pos, explosionRadius, 100.0f);
        //    _rb.mass = 10.0f;

        //    if (coll.gameObject.tag == "BREAKABLE")
        //    {
        //        coll.GetComponent<J_Breakable>().Invoke("DestructObject", 1.0f);
        //    }

        //    else if (coll.gameObject.tag == "Player")
        //    {
        //        // 충돌한 상대방으로부터 IDamageable 오브젝트 가져오기 시도
        //        IDamageable target = coll.GetComponent<IDamageable>();
        //        if (target != null)
        //        {
        //            // 상대방의 OnDamage 함수를 실행시켜 상대방에 데미지 주기
        //            target.OnDamage(damage, coll.ClosestPoint(transform.position), transform.position - coll.transform.position);
        //            // damaage - 탄알의 데미지,  hit.point - 레이가 충돌한 위치, hit.normal - 레이가 충돌한 표면의 방향
        //        }
        //    }

        //    else if (coll.gameObject.tag == "Enemy")
        //    {
        //        // 충돌한 상대방으로부터 IDamageable 오브젝트 가져오기 시도
        //        IDamageable target = coll.gameObject.GetComponent<IDamageable>();
        //        Debug.Log(target);
        //        if (target != null)
        //        {
        //            // 상대방의 OnDamage 함수를 실행시켜 상대방에 데미지 주기
        //            target.OnDamage(damage, coll.ClosestPoint(transform.position), transform.position - coll.transform.position);
        //            // damaage - 탄알의 데미지,  hit.point - 레이가 충돌한 위치, hit.normal - 레이가 충돌한 표면의 방향
        //        }
        //    }
        //}

    }
}
