using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;     // 플레이어 움직임 속도
    public float walkSpeed = 5.0f;     // 플레이어 walk 변환속도
    public float swapSpeed = 5.0f;     // 플레이어 swap 변환속도
    public float JumpPower = 10.0f;    // 플레이어 점프 파워
    public int jumpCount = 0;          // 플레이어 점프 횟수
    public int dodgeCount = 0;         // 플레이어 닷지 횟수

    yPlayerInput playerInput;   // 플레이어 입력 감지 컴포넌트
    Rigidbody rigid;            // 플레이어 리지드바디
    Animator myAnim;            // 플레이어 애니메이션

    public Transform myHips;    // 플레이어 상체
    public ySpringArm myArm;    // 카메라 스프링 암

    public LayerMask Layer;
        
    Vector3 MoveVec;      // 플레이어 프레임당 이동거리
    Vector3 dodgeVec;     // 플레이어 닷지 이동거리
    public bool isAir { get; private set; }
    public bool isDodge { get; private set; }
    public bool isBorder { get; private set; }


    public bool Swap0 = true;
    public bool Swap1 = false;
    public bool Swap2 = false;
    public bool Swap3 = false;

    [SerializeField]
    J_SwtichWeapon SwtichWeapon;

    void Awake()
    {
        // 사용할 컴포넌트들을 받아온다
        playerInput = GetComponent<yPlayerInput>();
        rigid = GetComponent<Rigidbody>();
        myAnim = GetComponentInChildren<Animator>();
        SwtichWeapon = GetComponentInChildren<J_SwtichWeapon>();
    }

    void FixedUpdate()
    {
        // 움직임을 구현하는 void함수
        Move();
        Walk();
        Jump();
        Dodge();
        //Swap();
        //StopToWall();
        // 입력값에 따라 애니메이터의 Move 파라미터값 변경
        AnimationMove();
        
        myAnim.SetBool("isAir", isAir);

        Debug.Log(isBorder);
    }


    private void LateUpdate()
    {
        if(!isDodge)
        {
            // 닷지 상태가 아닐 시 카메라의 방향에 따라 상체가 움직인다
            Vector3 rot = myHips.rotation.eulerAngles;
            rot.x = myArm.transform.rotation.eulerAngles.x;
            myHips.rotation = Quaternion.Euler(rot);
        }
    }

    void AnimationMove()
    {
        // 무기가 FIST일 때
        if (SwtichWeapon.GetWeapon() == J_SwtichWeapon.HOLDING_WEAPON.FIST)
        {
            myAnim.SetBool("Fist", true);
            myAnim.SetBool("Rifle", false);
            myAnim.SetBool("Axe", false);
            myAnim.SetBool("Grenade", false);

            // 주먹상태의 move파라미터 값을 나타낸다
            myAnim.SetFloat("x", playerInput.xMove);
            myAnim.SetFloat("y", playerInput.yMove);
        }

        // 무기가 AXE일 때
        else if (SwtichWeapon.GetWeapon() == J_SwtichWeapon.HOLDING_WEAPON.AXE)
        {
            myAnim.SetBool("Fist", false);
            myAnim.SetBool("Rifle", false);
            myAnim.SetBool("Axe", true);
            myAnim.SetBool("Grenade", false);

            // 도끼를 든 상태의 move파라미터 값을 나타낸다
            myAnim.SetFloat("xAxe", playerInput.xMove);
            myAnim.SetFloat("yAxe", playerInput.yMove);
        }

        // 무기가 RIFLE일 때
        else if (SwtichWeapon.GetWeapon() == J_SwtichWeapon.HOLDING_WEAPON.GUN)
        {

            myAnim.SetBool("Fist", false);
            myAnim.SetBool("Rifle", true);
            myAnim.SetBool("Axe", false);
            myAnim.SetBool("Grenade", false);

            // 총을 든 상태의 move파라미터 값을 나타낸다
            myAnim.SetFloat("xRiple", playerInput.xMove);
            myAnim.SetFloat("yRiple", playerInput.yMove);
        }

        // 무기가 GRENADE일 때
        else if (SwtichWeapon.GetWeapon() == J_SwtichWeapon.HOLDING_WEAPON.GRENADE)
        {
            myAnim.SetBool("Fist", false);
            myAnim.SetBool("Rifle", false);
            myAnim.SetBool("Axe", false);
            myAnim.SetBool("Grenade", true);

            // 수류탄을 든 상태의 move파라미터 값을 나타낸다
            myAnim.SetFloat("xGrenade", playerInput.xMove);
            myAnim.SetFloat("yGrenade", playerInput.yMove);
        }
    }

    void Move()
    {
        // x, y(z)축 상대적으로 움직이는 거리 계산
        Vector3 MoveXDistance = playerInput.xMove * transform.right * moveSpeed * Time.fixedDeltaTime;
        Vector3 MoveYDistance = playerInput.yMove * transform.forward * moveSpeed * Time.fixedDeltaTime;

        // x y 움직인 Vector를 하나에 합친다
        MoveVec = MoveXDistance + MoveYDistance;

        // Dodge시에는 dodgeVec로 움직인다
        if (isDodge)
            MoveVec = dodgeVec;

        if (isBorder)
            MoveVec = Vector3.zero;

        // 리지드바디를 이용해 게임 오브젝트 위치 변경
        rigid.MovePosition(rigid.position + MoveVec);
    }

    void Walk()
    {
        if(!isDodge)
        {
            if (playerInput.walk)
            {
                // 걸을때 이동속도 제한
                moveSpeed = 2.0f;
                // Run에서 Walk로 변경할 때 부드럽게 이동하기 위해 만든 로직
                myAnim.SetLayerWeight(1, Mathf.Lerp(myAnim.GetLayerWeight(1), 1f, walkSpeed * Time.fixedDeltaTime));
            }
            else
            {
                // 뛸때 이동속도 변경
                moveSpeed = 5.0f;
                // Walk로 Run으로 변경할 때 부드럽게 이동하기 위해 만든 로직
                myAnim.SetLayerWeight(1, Mathf.Lerp(myAnim.GetLayerWeight(1), 0f, walkSpeed * Time.fixedDeltaTime));
            }
        }
    }

    void Jump()
    {
        // 점프시 로직
        if (playerInput.jump && jumpCount < 1)
        {
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            jumpCount++;
            isAir = true;
        }
    }

    void Dodge()
    {
        // 점프키가 눌리고 플레이어가 움직이면 닷지를 실행한다
        if (playerInput.dodge && dodgeCount < 1)
        {
            // 닷지시 로직
            dodgeVec = MoveVec;
            moveSpeed *= 4;
            //transform.LookAt(transform.position + MoveVec);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward), Time.smoothDeltaTime * 5.0f);

            myAnim.SetTrigger("Dodge");
            isDodge = true;
            dodgeCount++;

            Invoke("DodgeOut", 1.3f);
        }
    }

    void DodgeOut()
    {
        moveSpeed *= 0.25f;
        isDodge = false;
        dodgeCount--;
    }

    void StopToWall()
    {
        isBorder = Physics.Raycast(transform.position, Camera.main.transform.forward, 5, LayerMask.GetMask("Wall"));
    }

    void Swap()
    {
        if(playerInput.swap0)
        {
            Swap0 = true;
            Swap1 = false;
            Swap2 = false;
            Swap3 = false;
        }
        if(playerInput.swap1)
        {
            Swap0 = false;
            Swap1 = true;
            Swap2 = false;
            Swap3 = false;
        }

        else if(playerInput.swap2)
        {
            Swap0 = false;
            Swap1 = false;
            Swap2 = true;
            Swap3 = false;
        }
        
        else if(playerInput.swap3)
        {
            Swap0 = false;
            Swap1 = false;
            Swap2 = false;
            Swap3 = true;
        }
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어가 바닥에 닿을경우 
        // collision.contacts[0] - 두 물체 사이의 여러 충돌 지점에서 첫 번째 충돌 지점의 정보
        // normal.y가 1이면 위 0 이면 오른쪽 -1 이면 아래를 바라보는 방향, 0.7이면 방향은 위쪽이며 경사가 너무 급하지는 않는지 검사한다
        if (collision.gameObject.tag == "Floor" && collision.contacts[0].normal.y > 0.7f)
        {
            // 점프 false, 점프 카운트 초기화
            isAir = false;
            jumpCount = 0;
        }
    }
}
