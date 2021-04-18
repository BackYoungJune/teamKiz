using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBossAnimEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public VoidDelVoid RoarEnd;
    public VoidDelVoid FlexEnd;
    public VoidDelVoid LeapStart;
    public VoidDelVoid LeapAttack;
    public VoidDelVoid LeapAttackEnd;    
    public VoidDelVoid AttackColliderOn;
    public VoidDelVoid AttackColliderOff;
    public VoidDelVoid AttackBranch;
    public VoidDelVoid AttackEnd;
    public VoidDelVoid PunchColliderOn;
    public VoidDelVoid PunchColliderOff;
    public VoidDelVoid PunchEnd;
    public VoidDelVoid ThrowingEnd;
    public VoidDelVoid GroggyEnd;

    //Roar
    public void OnRoarEnd()
    {
        RoarEnd?.Invoke();
    }

    //Flex
    public void OnFlexEnd()
    {
        FlexEnd?.Invoke();
    }


    //LeapAttack
    public void OnLeapStart()
    {
        LeapStart?.Invoke();
    }

    public void OnLeapAttack()
    {
        LeapAttack?.Invoke();
    }

    public void OnLeapAttackEnd()
    {
        LeapAttackEnd?.Invoke();
    }

    //Throwing
    public void OnThrowingEnd()
    {
        ThrowingEnd?.Invoke();
    }

    //Attack
    public void OnAttackColliderOn()
    {
        AttackColliderOn?.Invoke();
    }

    public void OnAttackColliderOff()
    {
        AttackColliderOff?.Invoke();
    }

    public void OnAttackBranch()
    {
        AttackBranch?.Invoke();
    }

    public void OnAttackEnd()
    {
        AttackEnd?.Invoke();
    }
    public void OnPunchColliderOn()
    {
        PunchColliderOn?.Invoke();
    }
    public void OnPunchColliderOff()
    {
        PunchColliderOff?.Invoke();
    }
    public void OnPunchEnd()
    {
        PunchEnd?.Invoke();
    }

    public void OnGroggyEnd()
    {
        GroggyEnd?.Invoke();
    }
}
