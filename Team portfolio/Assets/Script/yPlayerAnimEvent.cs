using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void VoidDelShoot();

public class yPlayerAnimEvent : MonoBehaviour
{
    public VoidDelShoot shoot;  // shoot는 함수의 주소를 받을수있다

    void OnShot()
    {
        shoot?.Invoke();
    }
}
