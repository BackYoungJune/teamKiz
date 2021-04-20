using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yPlayerAxe : MonoBehaviour
{
    public yAxe Axe; // 사용할 도끼
    public Transform leftHandMount; // 도끼의 왼쪽 손잡이,  왼손이 위치할 지점

    yPlayerInput playerInput;   // 플레이어의 입력
    public Animator playerAnimator;     // 애니메이터 컴포넌트
    public bool Attack = false; // 공격 여부확인

    void Awake()
    {
        // 사용할 컴포넌트들을 가져오기
        playerInput = GetComponentInParent<yPlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        // 슈터가 활성화될 때 총도 함께 활성화
        Axe.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        // 슈터가 비활성화될 때 총도 함께 비활성화
        Axe.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.fire2)
        {
            // 도끼를 휘두르는 애니메이션 실행
            playerAnimator.SetTrigger("Attack");
            Attack = true;
        }
    }

    // 애니메이터의 IK 갱신
    void OnAnimatorIK(int layerIndex)
    {
        // IK를 사용하여 왼손의 위치와 회전을 총의 오른쪽 손잡이에 맞춤
        //playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        //playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        //playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        //playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);
    }
}
