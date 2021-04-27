using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LFallingObj : MonoBehaviour
{
    public LTrapTrigger parent;
    public VoidDelVoid CollisionEnter;
    public Transform[] components = new Transform[4];
    bool destroy = false;
    float startDestroy=0;
    // Start is called before the first frame update
    void Update()
    {
        if(destroy)
        {
            startDestroy += Time.deltaTime;
        }
        if (startDestroy > 5)
            Destroy(this.gameObject);
    }   
    
    //이 부분에 불릿과 충돌 시 함수가 발동돼야 함. 영준아 도와죠!
    public  void Hit()
    {
        parent.OnTrigger(this.gameObject);
        Debug.Log("Hit");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Boss")
        {
            parent.onCollision?.Invoke();
            this.transform.GetComponent<SphereCollider>().enabled = false;
            for(int i =0; i<4; i++)
            {
                components[i].gameObject.AddComponent<Rigidbody>();
            }
            destroy = true;
        }
    }
}
