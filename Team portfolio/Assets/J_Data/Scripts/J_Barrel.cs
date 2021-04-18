using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Barrel : MonoBehaviour
{
    public GameObject explosionEffect;
    //private int hitCount;

    private Rigidbody barrelRigid;

    public float explosionRadius = 10.0f;

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
        // 8번 레이어(BREAKABLE)에 해당하는 오브젝트의 콜라이더를 얻어와 colls에 저장
        Collider[] colls = Physics.OverlapSphere(pos, explosionRadius, 1<<11);

        // 수집한 콜라이더에 폭발 효과 적용
        foreach(var coll in colls)
        {
            var _rb = coll.GetComponent<Rigidbody>();
            _rb.mass = 1.0f;
            _rb.AddExplosionForce(700.0f, pos, explosionRadius, 100.0f);
            _rb.mass = 20.0f;
            if (coll.gameObject.tag == "BREAKABLE")
            {
                coll.GetComponent<J_Breakable>().Invoke("DestructObject", 1.0f);
            }
        }
    }
}
