using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LBoss : yLivingEntity
{
    public enum STATE 
    { 
        CREATE, FLEX, APPROACHING, LEAPATTACK, THROWING, ATTACK, CHARGE, ROAR, GROGGY
    }

    public enum FLAG 
    {
        NORMAL, HEAVY, RAGE
    }

    public STATE mySTATE;
    public FLAG myFLAG;

    public Transform Player;

    public NavMeshAgent bossNavAgent;
    public Animator bossAnim;
    LBossAnimEvent bossAnimEvent;
    float StateStartTime;
    float PatternLength;

    //Flags
    bool roarEnd = false;
    bool flexEnd = false;
    public float nearDistance = 8.0f;//ATTACK 패턴으로 넘어갈 거리

    void Awake()
    {
        mySTATE = STATE.CREATE;
        myFLAG = FLAG.NORMAL;

        //AnimEvent 할당
        bossAnimEvent.RoarEnd += () => { roarEnd = true; };
        bossAnimEvent.FlexEnd += () => { flexEnd = true; };

    }

    void Update()
    {
        if(!dead) StateProcess();
    }

    void StateProcess()
    {
        switch (mySTATE)
        {
            case STATE.CREATE:
                if (roarEnd)
                {
                    roarEnd = false;
                    ChangeState(STATE.FLEX);
                }
                break;
            case STATE.FLEX:
                if (flexEnd)
                {
                    flexEnd = false;
                    myFLAG = FLAG.HEAVY;

                    ChangeState(STATE.APPROACHING);
                }
                break;
            case STATE.APPROACHING:
                /*
                패턴길이가 랜덤으로 정해짐 2초~5초
                패턴이 지속되는 동안 플래이어에게 접근함.
                패턴이 지속되는 동안 거리 안쪽으로 들어오면 근접 공격을 함.

                 */
                StateStartTime += Time.deltaTime;
                bossNavAgent.SetDestination(Player.position);

                if (bossNavAgent.remainingDistance < nearDistance)
                    ChangeState(STATE.ATTACK);

                if (StateStartTime > PatternLength)
                {
                    float temp = Random.Range(0f, 1.0f);
                    if(myFLAG == FLAG.RAGE)
                    {
                        if (temp < 0.4f)
                            ChangeState(STATE.THROWING);
                        else if(temp < 0.7f)
                            ChangeState(STATE.CHARGE);
                        else
                            ChangeState(STATE.LEAPATTACK);
                    }
                    else if(myFLAG == FLAG.HEAVY)
                    {
                        if (temp < 0.8f)
                            ChangeState(STATE.THROWING);
                        else
                            ChangeState(STATE.LEAPATTACK);
                    }
                    else if(myFLAG == FLAG.NORMAL)
                    {
                        if (temp < 0.4f)
                            ChangeState(STATE.THROWING);
                        else if (temp < 0.7f)
                            ChangeState(STATE.FLEX);
                        else
                            ChangeState(STATE.LEAPATTACK);
                    }
                }
                break;
            case STATE.LEAPATTACK:
                break;
            case STATE.THROWING:
                break;
            case STATE.ATTACK:
                break;
            case STATE.CHARGE:
                break;
            case STATE.ROAR:
                break;
            case STATE.GROGGY:
                break;
        }
    }

    public void ChangeState(STATE s)
    {
        if (mySTATE == s)
            return;
        mySTATE = s;
        switch (mySTATE)
        {
            case STATE.CREATE:
                bossAnim.SetTrigger("Roar");
                break;
            case STATE.FLEX:
                bossAnim.SetTrigger("Flex");
                break;
            case STATE.APPROACHING:
                StateStartTime = 0.0f;
                PatternLength = (float)Random.Range(2, 6);
                break;
            case STATE.LEAPATTACK:
                break;
            case STATE.THROWING:
                break;
            case STATE.ATTACK:
                break;
            case STATE.CHARGE:
                break;
            case STATE.ROAR:
                break;
            case STATE.GROGGY:
                break;
        }
    }
}
