﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRightHand : MonoBehaviour
{
    public LBossAnimEvent myAnimEvent;
    public float Damage = 10.0f;
    private void Awake()
    {
        myAnimEvent.PunchColliderOn += () =>
        {
            this.GetComponent<CapsuleCollider>().enabled = true;
        };
        myAnimEvent.PunchColliderOff += () =>
        {
            this.GetComponent<CapsuleCollider>().enabled = false;
        };
    }
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<yPlayerHealth>().OnDamage(Damage, other.ClosestPoint(transform.position), transform.position - other.transform.position);
        }
    }
}