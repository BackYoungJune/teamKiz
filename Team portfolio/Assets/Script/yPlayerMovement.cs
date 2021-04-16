using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;     // 플레이어 움직임 속도
    public float walkSpeed = 5.0f;     // 플레이어 walk 변환속도
    public float JumpPower = 10.0f;    // 플레이어 점프 파워
    public int jumpCount = 0;          // 플레이어 점프 횟수
    public int dodgeCount = 0;         // 플레이어 닷지 횟수

    yPlayerInput playerInput;   // 플레이어 입력 감지 컴포넌트
    Rigidbody rigid;            // 플레이어 리지드바디
    Animator myAnim;            // 플레이어 애니메이션

    public Transform myHips;
    public ySpringArm myArm;

    Vector3 MoveVec;      // 플레이어 프레임당 이동거리
    Vector3 dodgeVec;     // 플레이어 닷지 이동거리
    public bool isAir { get; private set; }
    public bool isDodge;

    void Awake()
    {
        // 사용할 컴포넌트들을 받아온다
        playerInput = GetComponent<yPlayerInput>();
        rigid = GetComponent<Rigidbody>();
        myAnim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        // 움직임을 구현하는 void함수
        Move();
        Walk();
        Jump();
        Dodge();

        // 입력값에 따라 애니메이터의 Move 파라미터값 변경
        myAnim.SetFloat("x", playerInput.xMove);
        myAnim.SetFloat("y", playerInput.yMove);
        myAnim.SetBool("isAir", isAir);
    }

   

    private void LateUpdate()
    {
        Vector3 rot = myHips.rotation.eulerAngles;
        rot.x = myArm.transform.rotation.eulerAngles.x;
        myHips.rotation = Quaternion.Euler(rot);
    }

    void Move()
    {
        // x, y(z)축 상대적으로 움직이는 거리 계산
        Vector3 MoveXDistance = playerInput.xMove * transform.right * moveSpeed * Time.fixedDeltaTime;
        Vector3 MoveYDistance = playerInput.yMove * transform.forward * moveSpeed * Time.fixedDeltaTime;

        // x y 움직인 Vector를 하나에 합친다
        MoveVec = MoveXDistance + MoveYDistance;
       // Debug.Log(playerInput.xMove);
       // Debug.Log(playerInput.yMove);
        if (isDodge)
            MoveVec = dodgeVec;

        // 리지드바디를 이용해 게임 오브젝트 위치 변경
        rigid.MovePosition(rigid.position + MoveVec);

    }

    void Walk()
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

    void Jump()
    {
        // 점프시 로직
        if (playerInput.jump && jumpCount < 1 && MoveVec == Vector3.zero)
        {
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            jumpCount++;
            isAir = true;
        }
    }

    void Dodge()
    {
        // 점프키가 눌리고 플레이어가 움직이면 닷지를 실행한다
        if (playerInput.dodge && MoveVec != Vector3.zero && dodgeCount < 1)
        {
            // 닷지시 로직
            dodgeVec = MoveVec;
            moveSpeed *= 2;
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
        moveSpeed *= 0.5f;
        isDodge = false;
        dodgeCount--;
    }


    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어가 바닥에 닿을경우 
        // collision.contacts[0] - 두 물체 사이의 여러 충돌 지점에서 첫 번째 충돌 지점의 정보
        // normal.y가 1이면 위 0 이면 오른쪽 -1 이면 아래를 바라보는 방향, 0.7이면 방향은 위쪽이며 경사가 너무 급하지는 않는지 검사한다
        if (collision.gameObject.tag == "Wall" && collision.contacts[0].normal.y > 0.7f)
        {
            // 점프 false, 점프 카운트 초기화
            isAir = false;
            jumpCount = 0;
        }
    }
}
