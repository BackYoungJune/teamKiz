using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCollapsObj : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Boss;
    public Animator bossAnim;
    bool collapsFlag;
    void Awake()
    {
        collapsFlag = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RigidOn()
    {
        this.gameObject.AddComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Boss")
        {
            bossAnim.SetTrigger("Groggy");
            Boss.parent.GetComponent<LBoss>().OnDamage(30000f, Vector3.zero, Vector3.zero);
        }


    }

}
