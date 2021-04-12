using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yDestroyAfterTimeParticle : MonoBehaviour
{
    public float timeToDestroy = 0.8f;
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

}
