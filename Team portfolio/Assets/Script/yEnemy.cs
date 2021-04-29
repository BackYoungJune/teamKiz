using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class yEnemy : yLivingEntity
{
    public enum STATE
    {
        NORMAL,     // 시작상태
        ROAMING,    // 플레이어를 발견하기 전 Patrol 상태
        SEARCHING,  // 좀비가 총알을 맞고 플레이어를 발견하지 못하는 상태
        BATTLE,     // 플레이어를 발견한 상태
        ESCAPE      // 탈출
    }
    public STATE myState = STATE.NORMAL;

    public LayerMask whatIsTarget; // 추적 대상 레이어
    [SerializeField]
    yLivingEntity targetEntity;     // 추적할 대상

    public Animator myAnim;     // 애니메이터 컴포넌트
    public NavMeshAgent myNavAgent;     // 경로계산 AI
    public yRangeSystem myRangeSys = null;  // 플레이어 탐지 컴포넌트
    public yAnimEvent myAnimEvent = null;   // 애니메이션 이벤트 

    Vector3 RadiusPoint = Vector3.zero;     // 원의 표면상의 임의의 점
    public float RadiusLength = 4.0f;       // 원의 길이를 조절하는 변수
    public float Playtime = 0f;             // 플레이어 NORMAL ROAMING으로 가는 시간

    public float damage = 20.0f;            // 공격력
    public float AttackTime = 0.5f;         // 공격 딜레이 시간

    Vector3 hitPoint = Vector3.zero;
    Vector3 hitNormal = Vector3.zero;

    //public ParticleSystem hitEffect; // 피격시 재생할 파티클 효과

    float NavSpeed;

    void Awake()
    {
        // 게임 오브젝트로부터 사용할 컴퍼넌트 가져오기;
        myNavAgent = GetComponent<NavMeshAgent>();
        myAnim = GetComponentInChildren<Animator>();
        myAnimEvent = GetComponentInChildren<yAnimEvent>();
        myRangeSys = GetComponentInChildren<yRangeSystem>();
        //rigid = GetComponent<Rigidbody>();
        // RangeSystem Sphere 범위 안에 들어오면 OnBattle 실행
        myRangeSys.battle = OnBattle;
        // 공격시 이벤트 실행
        myAnimEvent.Attack1 += OnAttackTarget;
        myAnimEvent.Attack2 += OnAttackTarget;


    }

    // 적 AI의 초기 스펙을 결정하는 셋업 메서드
    public void Setup(float newHealth, float newDamage, float newSpeed)
    {
        // 최대체력, 체력, 데미지, 스피드를 초기화한다
        startHealth = newHealth;
        health = newHealth;
        damage = newDamage;
        myNavAgent.speed = newSpeed;
        NavSpeed = newSpeed;
    }

    void Update()
    {
        if (!dead)
        {
            StateProcess();
        }
    }

    void OnBattle()
    {
        // RangeSystem의 범위에 플레이어가 들어가면 BATTLE 실행
        ChangeState(STATE.BATTLE);
    }

    public void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch (myState)
        {
            case STATE.NORMAL:
                // 초기화
                if(!dead)
                {
                    myAnim.SetFloat("Speed", 0f);
                    Playtime = 0f;
                    myNavAgent.stoppingDistance = 0.5f;
                    myNavAgent.isStopped = true;
                }
                break;
            case STATE.ROAMING:
                if (!dead)
                {
                    RadiusPoint = Random.onUnitSphere;  // 반경1을 갖는 구의 표면상의 임의의 지점을 반환한다
                    RadiusPoint.y = 0.0f;
                    Vector3 ZombiePosition = transform.position;    // 몬스터 자신의 포지션값을 가진다
                    Vector3 desPos = ZombiePosition + RadiusPoint * RadiusLength;   // 자신의 포지션값으로부터 RadiusLength의 반경을 가진 임의의 원의값을 가져온다
                    myNavAgent.isStopped = false;
                    myNavAgent.SetDestination(desPos);  // 목표지점으로 이동시킨다

                    StartCoroutine(Searching());
                } 
                break;
            case STATE.SEARCHING:
                if (!dead)
                {
                    StartCoroutine(TargetEntity());
                    Vector3 dir1 = targetEntity.transform.position - transform.position;
                    dir1.y = 0;  // 평면상으로만 이동하려고 y = 0 했다
                    dir1.Normalize();
                    // Enemy를 플레이어쪽으로 부드럽게 회전하도록 한다
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir1), 1.0f);
                    myAnim.SetTrigger("Roar");
                    if (myNavAgent.isStopped)
                    {
                        myNavAgent.isStopped = false;
                    }
                } 
                break;
            case STATE.BATTLE:
                if (!dead)
                {
                    StartCoroutine(TargetEntity());
                    // Enemy 스탑거리 조절
                    if (myNavAgent.isStopped)
                    {
                        myNavAgent.isStopped = false;
                    }
                    myNavAgent.stoppingDistance = 1.2f;
                } 
                break;
            case STATE.ESCAPE:
                break;
        }
    }

    public void StateProcess()
    {
        switch (myState)
        {
            case STATE.NORMAL:
                if (!dead)
                {
                    // 2초후 ROAMING 실행
                    Playtime += Time.deltaTime;
                    if (Playtime > 2.0f)
                    {
                        ChangeState(STATE.ROAMING);
                    }
                }
                break;
            case STATE.ROAMING:
                if (!dead)
                {
                    // myNavAgent.speed를 나눈이유는 0 ~ 1.0으로 값을 맞추기 위해 나눴다
                    myAnim.SetFloat("Speed", myNavAgent.velocity.magnitude / myNavAgent.speed);
                    // 애니메이션 스피드 맞춰준다
                    myAnim.speed = NavSpeed / 3;
                    // 이동을 거의다 시켰으면 STATE.NORMAL로 바꿔준다
                    if (myNavAgent.remainingDistance < myNavAgent.stoppingDistance && myState == STATE.ROAMING)
                    {
                        ChangeState(STATE.NORMAL);
                    }
                }
                break;
            case STATE.SEARCHING:
                if (!dead)
                {
                    myAnim.SetFloat("Speed", myNavAgent.velocity.magnitude / myNavAgent.speed);
                    // 애니메이션 스피드 맞춰준다
                    myAnim.speed = NavSpeed / 3;
                    // 플레이어쪽으로 이동
                    myNavAgent.SetDestination(targetEntity.transform.position);
                    //myNavAgent.destination = targetEntity.transform.position;
                }
                break;
            case STATE.BATTLE:
                if(!dead)
                {
                    Vector3 dir = myRangeSys.Target.position - transform.position;
                    dir.y = 0;  // 평면상으로만 이동하려고 y = 0 했다
                    dir.Normalize();
                    // Enemy를 플레이어쪽으로 부드럽게 회전하도록 한다
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.smoothDeltaTime * 3.0f);
                    myAnim.SetFloat("Speed", myNavAgent.velocity.magnitude / myNavAgent.speed);
                    
                    // 플레이어쪽으로 이동
                    myNavAgent.SetDestination(myRangeSys.Target.position);

                    // 어택 딜레이를 만든다
                    if (AttackTime > Mathf.Epsilon)
                    {
                        AttackTime -= Time.deltaTime;
                    }
                    else
                    {
                        OnAttack();
                    }
                }
                break;
            case STATE.ESCAPE:
                break;
        }
    }

    void OnAttack()
    {
        // 거리가 짧아지면 실행
        if (myNavAgent.remainingDistance <= myNavAgent.stoppingDistance && !dead && myState == STATE.BATTLE)
        {
            // 확률에따라 Attack1, Attack2 실행
            if (Random.Range(0, 10) > 3)
            {
                myAnim.SetTrigger("Attack1");

            }
            else
            {
                myAnim.SetTrigger("Attack2");
            }
            AttackTime = 1f;
        }
    }
    void OnAttackTarget()
    {
        // Player와 Enemy의 거리를 구함
        float distance = Vector3.Distance(myRangeSys.Target.position, transform.position);
        // Player가 공격을 받을 시 일정 거리 이상 멀어지면 공격을 받지 않는다
        if (distance < 2.6f)
            myRangeSys.Target.GetComponent<yLivingEntity>()?.OnDamage(damage, hitPoint, hitNormal);
    }

    IEnumerator TargetEntity()
    {
        if (targetEntity) yield break;

        while (targetEntity == null)
        {
            // 5유닛의 반지름을 가진 가상의 구를 그렸을 때 구와 겹치는 모든 콜라이더를 가져옴
            // 단, whatIsTarget 레이어를 가진 콜라이더만 가져오도록 필터링 (LayerMask 사용)
            Collider[] colliders = Physics.OverlapSphere(transform.position, 200f, whatIsTarget);

            // 모든 콜라이더를 순회하면서 살아있는 LivingEntity 찾기
            for (int i = 0; i < colliders.Length; i++)
            {
                // 콜라이더로부터 LivingEntity 컴포넌트 가져오기
                yLivingEntity livingEntity = colliders[i].GetComponent<yLivingEntity>();

                // LivingEntity 컴포넌트가 존재하며, 해당 LivingEntity가 살아 있다면
                if (livingEntity != null && !livingEntity.dead)
                {
                    // 추적 대상을 해당 LivingEntity로 설정
                    targetEntity = livingEntity;

                    // for 문 루프 즉시 정지
                    break;
                }
            }
            yield return null;
        }
    }

    public override void Die()
    {
        base.Die();

        // 다른 AI를 방해하지 않도록 자신의 모든 콜라이더를 비활성화
        Collider[] enemyColliders = GetComponents<Collider>();
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            enemyColliders[i].enabled = false;
        }

        // AI 추적을 중지하고 내비메시 컴포넌트 비활성화
        myNavAgent.isStopped = true;
        myNavAgent.enabled = false;

        // 애니메이션 스피드 초기화
        myAnim.speed = 1;
        // 사망 애니메이션 재생
        myAnim.SetTrigger("Die");
        // 스테이트 변경
        ChangeState(STATE.ESCAPE);

        MN_UIManager.Instance.IsZombieKill = true;
        Debug.Log("Zombie Kill");
        
        // 좀비가 죽을 경우 10초후에 사라진다.
        Destroy(gameObject, 10.0f);
    }

    // 데미지를 입었을때 실행할 처리
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!dead)
        {
            // 공격받은 지점과 방향으로 파티클 효과 재생
            //hitEffect.transform.position = hitPoint;
            //hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            //hitEffect.Play();
            myAnim.SetTrigger("Damage"); // 공격 받을 시 애니메이션 재생
        }

        if (!dead && !targetEntity)
        {
            ChangeState(STATE.SEARCHING);
        }

        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public void HitByGrenade(Vector3 explosionPos)
    {
        // 수류탄이 터진위치와 나의 위치를 빼서 react방향을 정한다
        Vector3 reactVec = transform.position - explosionPos;
        StartCoroutine(OnReact(reactVec, true));
    }

    IEnumerator OnReact(Vector3 reactVec, bool isGrenade)
    {
        yield return new WaitForSeconds(0.1f);

        if (isGrenade && !dead)
        {
            // reactVec의 크기를 정한다
            reactVec = reactVec.normalized;
            reactVec += Vector3.up * 10;

            // Rigidbody를 추가한다
            Rigidbody rigid = gameObject.AddComponent<Rigidbody>();

            // 리지드바디를 킨다
            //rigid.WakeUp();

            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;

            // rigidbody를 이용해 좀비를 날려보낸다
            rigid.freezeRotation = false;
            rigid.mass = 1.0f;
            rigid.AddForce(reactVec * 10, ForceMode.Impulse);
            rigid.AddTorque(reactVec * 25, ForceMode.Impulse);

            yield return new WaitForSeconds(0.5f);

            // 버그 방지를 위해 물리영향을 받지않도록했다가 다시 돌린다
            rigid.isKinematic = true;

            yield return new WaitForSeconds(0.5f);
            rigid.isKinematic = false;

            // 리지드바디를 다시 끈다
            //rigid.Sleep();
            // 리지드바디를 제거한다
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }

    IEnumerator Searching()
    {
        // SphereCastAll - 구체 모양의 레이캐스팅(모든 오브젝트)
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 10, Vector3.up, 0, LayerMask.GetMask("Enemy"));

        foreach (RaycastHit hit in rayHits)
        {
            // 첫번째 Enemy와 충돌한 경우
            if (hit.transform.gameObject.tag == "Enemy")
            {
                // 충돌한 상대방으로부터 IDamageable 오브젝트 가져오기 시도
                yEnemy target = hit.collider.GetComponent<yEnemy>();

                if (target.myState == STATE.SEARCHING || target.myState == STATE.BATTLE || target.myState == STATE.ESCAPE)
                {
                    ChangeState(STATE.SEARCHING);
                }
            }
        }

        yield return new WaitForSeconds(1.0f);
    }
}
