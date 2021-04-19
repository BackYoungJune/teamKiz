using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBodyHit : MonoBehaviour
{
    public Animator myAnim;
    public float Damage = 20f;

    // Update is called once per frame
    void Update()
    {
        if (myAnim.GetBool("Charging"))
        {
            this.GetComponent<SphereCollider>().enabled = true;
        }
        else
        {
            this.GetComponent<SphereCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<yPlayerHealth>().OnDamage(Damage, other.ClosestPoint(transform.position), transform.position - other.transform.position);
            other.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.position - other.transform.position) * 5, ForceMode.Impulse);
            Debug.Log(other.GetComponent<Rigidbody>());
        }
    }
}