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
    public VoidDelVoid ThrowObj;
    public VoidDelVoid ThrowingEnd;
    public VoidDelVoid GroggyEnd;
    public VoidDelVoid Groggy;
    public AudioSource myAudio;
    public AudioClip roarSound;
    public AudioClip attackSound1;
    public AudioClip attackSound2;
    public AudioClip leapSound;
    public AudioClip leapAttackSound;
    public AudioClip throwSound;
    public AudioClip groggySound;
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
    public void OnThrowObj()
    {
        ThrowObj?.Invoke();
    }    
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
    public void OnGroggy()
    {
        Groggy?.Invoke();
    }

    public void RoarSound()
    {
        Sound.I.PlayEffectSound(roarSound, myAudio);
    }
    public void FlexSound()
    {
        Sound.I.PlayEffectSound(roarSound, myAudio,0.5f);
    }
    public void AttackSound1()
    {
        Sound.I.PlayEffectSound(attackSound1, myAudio);
    }
    public void AttackSound2()
    {
        Sound.I.PlayEffectSound(attackSound2, myAudio);
    }
    public void LeapSound()
    {
        Sound.I.PlayEffectSound(leapSound, myAudio);
    }
    public void LeapAttackSound()
    {
        Sound.I.PlayEffectSound(leapAttackSound, myAudio);
    }
    public void ThrowSound()
    {
        Sound.I.PlayEffectSound(throwSound, myAudio);
    }
    public void GroggySound()
    {
        Sound.I.PlayEffectSound(groggySound, myAudio);
    }
}
