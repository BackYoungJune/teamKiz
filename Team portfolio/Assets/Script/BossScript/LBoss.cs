using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
미구현된 항목들.
1. 보스 데미지, 공격, 히트, 죽음
2. 레이지 플래그(히트되서 피가 30%미만이면 발동)
 */
public class LBoss : yLivingEntity
{
    public enum STATE 
    { 
        CREATE, FLEX, APPROACHING, LEAPATTACK, THROWING, ATTACK, CHARGE, ROAR, GROGGY
    }

    //메시 콜라이더 업데이트부 애니메이션 재생에 따라서 콜라이더도 계속 움직여준다.
    public SkinnedMeshRenderer meshRenderer;
    public MeshCollider collider;
    public void UpdateCollider()
    {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);
        collider.sharedMesh = null;
        collider.sharedMesh = colliderMesh;
    }
    public enum FLAG 
    {
        NORMAL, HEAVY, RAGE
    }

    public STATE mySTATE;
    public FLAG myFLAG;

    public Transform Player;

    public NavMeshAgent bossNavAgent;
    public float bossSpeed = 3.5f;
    public Animator bossAnim;
    public LBossAnimEvent bossAnimEvent;
    public LTrapTrigger trapTrigger;

    public Transform[] triggers = new Transform[4];
    public int CurTriggerIndex;
    public Transform CheckTrigger = null;
    public float[] triggerDist = new float[4];

    float StateStartTime;
    float PatternLength;
    Vector3 targetPos;

    //Flags
    bool roarEnd = false;
    public float nearDistance = 8.0f;//ATTACK 패턴으로 넘어갈 거리

    void Awake()
    {
        mySTATE = STATE.CREATE;
        myFLAG = FLAG.NORMAL;
        
        //AnimEvent 할당
        //Roar 애니메이션의 경우 최초 등장컷에서도 사용되므로 플래그 사용.
        bossAnimEvent.RoarEnd += () => { roarEnd = true;  };
        //플래그 스테이트를 HEAVY로 바꾸고 APPROACH 체인지 스테이트 호출
        bossAnimEvent.FlexEnd += () => 
        {
            myFLAG = FLAG.HEAVY;
            ChangeState(STATE.APPROACHING);
        };
        //코루틴을 호출해서 패턴의 움직임을 구현하고, 패턴이 종료되면 APPROACH체인지 스테이트 호출
        bossAnimEvent.LeapStart += () => { StartCoroutine(Leap()); };
        bossAnimEvent.LeapAttackEnd += () => { ChangeState(STATE.APPROACHING); };

        //Throwing
        bossAnimEvent.ThrowingEnd += () => 
        {
            CheckTrigger = null;
            ChangeState(STATE.APPROACHING); 
        };

        //Throwing Trigger Check
        trapTrigger.Trigger += OnTrap;

        void OnTrap(GameObject trig)
        {
            if (CheckTrigger == trig.transform.parent)
            {
                Debug.Log("TriggerOn");
                trig.AddComponent<Rigidbody>();
                ChangeState(STATE.GROGGY);
            }
        }
        trapTrigger.onCollision += () =>
        {
            bossAnim.SetTrigger("Groggy");
            myFLAG = FLAG.NORMAL;
        };
        //Groggy
        bossAnimEvent.GroggyEnd += () =>
        {
            CheckTrigger = triggers[CurTriggerIndex] = null;
            ChangeState(STATE.APPROACHING);
        };

        //Attack
        bossAnimEvent.AttackBranch += () =>
         {
             if (Vector3.Distance(this.transform.position, Player.position) < nearDistance)
             {
                 bossAnim.SetTrigger("Punch");
             }
         };
        bossAnimEvent.AttackEnd += () => {
            if (myFLAG == FLAG.RAGE)
                ChangeState(STATE.LEAPATTACK);
            else
                bossAnim.SetTrigger("Roar");
        };

        bossAnimEvent.PunchEnd += () => {
            if (myFLAG == FLAG.RAGE)
                ChangeState(STATE.LEAPATTACK);
            else
                bossAnim.SetTrigger("Roar");
        };
    }

    void Update()
    {
        if (!dead && !Player.GetComponent<yPlayerHealth>().dead)
        {
            UpdateCollider();
            StateProcess();
        }

    }
    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    //StateProcess
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
                //체인지 스테이트에서 애니메이션을 호출하고, 애니메이션이 끝나면 자동으로 체인지 스테이트 호출됨.
                //추후 동작 애니메이션 재생 중 수류탄 등 폭발공격에 피해를 받으면 동작을 캔슬하고 HEAVY상태로 변하지 않고 패턴을 넘기도록 코딩할 예정.
                break;
            case STATE.APPROACHING:
                /*
                패턴길이가 랜덤으로 정해짐 2초~5초
                패턴이 지속되는 동안 플래이어에게 접근함.
                패턴이 지속되는 동안 거리 안쪽으로 들어오면 근접 공격을 함.
                 */
                StateStartTime += Time.deltaTime;
                bossNavAgent.SetDestination(Player.position);
                bossAnim.SetFloat("Speed", 1f);
                if (Vector3.Distance(this.transform.position,Player.position) < nearDistance)
                {
                    ChangeState(STATE.ATTACK);
                    bossAnim.SetFloat("Speed", 0f);
                }

                if (StateStartTime > PatternLength)
                {
                    bossAnim.SetFloat("Speed", 0f);
                    float SelectPattern = Random.Range(0f, 1.0f);
                    if(myFLAG == FLAG.RAGE)
                    {
                        if (SelectPattern < 0.5f)
                            ChangeState(STATE.CHARGE);
                        else
                            ChangeState(STATE.LEAPATTACK);
                    }
                    else if(myFLAG == FLAG.HEAVY)
                    {
                        if (SelectPattern < 0.3f)
                            //if (SelectPattern < 1f)
                            ChangeState(STATE.THROWING);
                        else
                            ChangeState(STATE.LEAPATTACK);
                    }
                    else if(myFLAG == FLAG.NORMAL)
                    {
                        if (SelectPattern < 0.3f)
                            ChangeState(STATE.FLEX);
                        else
                            ChangeState(STATE.LEAPATTACK);
                    }
                }
                break;
            case STATE.LEAPATTACK:
                //체인지 스테이트에서 트리거 발동.
                //나머지 구현부는 코루틴으로 동작되므로 이 파트에 코드가 없음. IEnumerator Leap()참조.
                break;
            case STATE.THROWING:
                if(roarEnd)
                {
                    roarEnd = false;
                    CheckDistance();
                    bossNavAgent.SetDestination(targetPos);
                    if (bossNavAgent.isStopped)
                        bossNavAgent.isStopped = false;
                    bossAnim.SetFloat("Speed", 1f);
                }
                if(bossNavAgent.remainingDistance < bossNavAgent.stoppingDistance+Mathf.Epsilon)
                {
                    CheckTrigger = triggers[CurTriggerIndex];
                    bossAnim.SetFloat("Speed", 0f);
                    bossNavAgent.speed = 0.1f;
                    bossNavAgent.SetDestination(Player.position);
                    bossAnim.SetTrigger("Throwing");
                }
                break;
            case STATE.ATTACK:
                bossNavAgent.SetDestination(Player.position);
                bossNavAgent.speed = bossSpeed * 0.3f;
                if (roarEnd)
                {
                    roarEnd = false;
                    ChangeState(STATE.APPROACHING);
                    bossNavAgent.speed = bossSpeed;
                }
                break;
            case STATE.CHARGE:
                bossNavAgent.SetDestination(Player.position);
                if (roarEnd)
                {
                    roarEnd = false;
                    targetPos = Player.position;
                    bossAnim.SetBool("Charging", true);
                    StartCoroutine(Charge());
                }
                break;
            case STATE.ROAR:
                break;
            case STATE.GROGGY:
                break;
        }
    }

    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    //ChangeState
    public void ChangeState(STATE s)
    {
        if (mySTATE == s)
            return;
        mySTATE = s;
        switch (mySTATE)
        {
            case STATE.CREATE:
                break;
            case STATE.FLEX:
                bossAnim.SetTrigger("Flex");
                if(!bossNavAgent.isStopped)
                    bossNavAgent.isStopped = true;
                break;
            case STATE.APPROACHING:
                StateStartTime = 0.0f;
                PatternLength = (float)Random.Range(2, 6);
                if (bossNavAgent.isStopped)
                    bossNavAgent.isStopped = false;
                if (myFLAG == FLAG.RAGE)
                    bossNavAgent.speed = bossSpeed * 2;
                else
                    bossNavAgent.speed = bossSpeed;

                break;
            case STATE.LEAPATTACK:
                bossAnim.SetTrigger("LeapAttack");
                targetPos = Player.position;
                if (!bossNavAgent.isStopped)
                    bossNavAgent.isStopped = true;
                break;
            case STATE.THROWING:
                if (!bossNavAgent.isStopped)
                    bossNavAgent.isStopped = true;
                bossAnim.SetTrigger("Roar");

                break;
            case STATE.ATTACK:
                bossAnim.SetTrigger("Attack");
                break;
            case STATE.CHARGE:
                bossNavAgent.speed = 0.1f;
                bossAnim.SetTrigger("Roar");
                break;
            case STATE.ROAR:
                break;
            case STATE.GROGGY:
                bossAnim.SetTrigger("HitCameraOn");
                if(!bossNavAgent.isStopped)
                    bossNavAgent.isStopped = true;
                break;
        }
    }
    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    //Coroutines
    void CheckDistance()
    {
        if (triggers[0])
            triggerDist[0] = Vector3.Distance(this.transform.position, triggers[0].position);
        else
            triggerDist[0] = 10000;
        if (triggers[1])
            triggerDist[1] = Vector3.Distance(this.transform.position, triggers[1].position);
        else
            triggerDist[1] = 10000;
        if (triggers[2])
            triggerDist[2] = Vector3.Distance(this.transform.position, triggers[2].position);
        else
            triggerDist[2] = 10000;
        if (triggers[3])
            triggerDist[3] = Vector3.Distance(this.transform.position, triggers[3].position);
        else
            triggerDist[3] = 10000;

        if (Mathf.Min(triggerDist) == triggerDist[0])
        {
            targetPos = triggers[0].position;
            CurTriggerIndex = 0;
        }
        else if (Mathf.Min(triggerDist) == triggerDist[1])
        {
            targetPos = triggers[1].position;
            CurTriggerIndex = 1;
        }
        else if (Mathf.Min(triggerDist) == triggerDist[2])
        { 
            targetPos = triggers[2].position;
            CurTriggerIndex = 2;
        }
        else if (Mathf.Min(triggerDist) == triggerDist[3])
        {
            targetPos = triggers[3].position;
            CurTriggerIndex = 3;
        }
    }
    IEnumerator Leap()
    {
        StateStartTime = 0f;
        Vector3 OriginalPos = this.transform.position;
        Vector3 pos = Vector3.zero;

        //도약 시작
        while (StateStartTime < 1f + Mathf.Epsilon)
        {
            pos.x = Mathf.Lerp(OriginalPos.x, targetPos.x, StateStartTime);
            pos.z = Mathf.Lerp(OriginalPos.z, targetPos.z, StateStartTime);
            pos.y = Mathf.Lerp(OriginalPos .y+ 3, OriginalPos.y, Mathf.Abs(1 - StateStartTime * 2));
            this.transform.position = pos;
            StateStartTime += Time.deltaTime;
            yield return null;
        }
        this.transform.position = targetPos;
        bossNavAgent.speed = bossSpeed;
        if (!bossNavAgent.isStopped)
            bossNavAgent.isStopped = true;
        //도약 끝
        if (myFLAG != FLAG.RAGE)
        {
            //착지 위치에 데미지를 1회 가하는 장판 구현
        }
        else
        {
            //데미지가 더 높고 범위가 더 넓은 데미지 장판 구현
        }
    }
    IEnumerator Charge()
    {
        StateStartTime = 0f;
        Vector3 OriginalPos = this.transform.position;
        Vector3 pos = Vector3.zero;
        while (StateStartTime < 1f + Mathf.Epsilon)
        {
            if(collider.attachedRigidbody.tag == "Player")
            {
                Debug.Log("Damage");
            }
            pos.x = Mathf.Lerp(OriginalPos.x, targetPos.x, StateStartTime);
            pos.z = Mathf.Lerp(OriginalPos.z, targetPos.z, StateStartTime);
            this.transform.position = pos;
            StateStartTime += Time.deltaTime*2;
            yield return null;
        }
        this.transform.position = targetPos;
        bossAnim.SetBool("Charging", false);
        yield return new WaitForSeconds(0.5f);
        ChangeState(STATE.APPROACHING);
    }
}
