using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LFallingObj : MonoBehaviour
{
    public LTrapTrigger parent;
    public VoidDelVoid CollisionEnter;
    // Start is called before the first frame update
    void Update()
    {

    }   
    
    //이 부분에 불릿과 충돌 시 함수가 발동돼야 함. 영준아 도와죠!
    public  void Hit()
    {
        parent.OnTrigger(this.gameObject);
        Debug.Log("Hit");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            parent.onCollision?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
