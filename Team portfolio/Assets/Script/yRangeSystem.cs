using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void VoidDelVoid();

public class yRangeSystem : MonoBehaviour
{
    public VoidDelVoid battle;  // battle는 함수의 주소를 받을수있다
    public Transform Target = null;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Target = other.transform;
            battle?.Invoke();    // battle이 있는지 없는지 검사한다
        }
    }

    
}
