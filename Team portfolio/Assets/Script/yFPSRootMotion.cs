using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yFPSRootMotion : MonoBehaviour
{
    Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        //transform.parent.Translate(myAnim.deltaPosition, Space.World);
        Rigidbody rigid = transform.parent.GetComponent<Rigidbody>();
        rigid.MovePosition(rigid.position + myAnim.deltaPosition);
    }
}
