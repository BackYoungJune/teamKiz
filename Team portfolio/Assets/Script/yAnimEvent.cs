using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yAnimEvent : MonoBehaviour
{
    public VoidDelVoid Attack1 = null;  // 첫번째 공격할 시 발동할 딜리게이트
    public VoidDelVoid Attack2 = null;  // 두번째 공격할 시 발동할 딜리게이트

    void OnAttack1()
    {
        Attack1?.Invoke();  // 첫번째 공격이 실행되면 화성화
    }

    void OnAttack2()
    {
        Attack2?.Invoke();  // 두번째 공격이 실행되면 화성화
    }
}
