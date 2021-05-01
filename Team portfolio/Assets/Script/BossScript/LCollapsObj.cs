using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCollapsObj : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Boss;
    public Animator bossAnim;
    public AudioSource myAudio;
    public AudioClip CrashSound;
    bool collapsFlag;
    void Awake()
    {
        collapsFlag = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RigidOn()
    {
        Rigidbody myRigid = this.gameObject.AddComponent<Rigidbody>();

        myRigid.AddForce(Boss.position - this.transform.position,ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Boss" && collapsFlag)
        {
            Sound.I.PlayEffectSound(CrashSound, myAudio);
            bossAnim.SetTrigger("Groggy");
            Boss.GetComponent<LBoss>().OnDamage(30000f, Vector3.zero, Vector3.zero);
            Destroy(this.gameObject);
        }
    }

}
