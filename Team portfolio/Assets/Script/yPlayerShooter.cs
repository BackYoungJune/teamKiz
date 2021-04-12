using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yPlayerShooter : MonoBehaviour
{
    public yRiple Riple; // 사용할 총
    public Transform leftHandMount; // 총의 오른쪽 손잡이, 오른손이 위치할 지점

    yPlayerInput playerInput; // 플레이어의 입력
    public Animator playerAnimator; // 애니메이터 컴포넌트

    // Start is called before the first frame update
    void Awake()
    {
        // 사용할 컴포넌트들을 가져오기
        playerInput = GetComponentInParent<yPlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        // 슈터가 활성화될 때 총도 함께 활성화
        Riple.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        // 슈터가 비활성화될 때 총도 함께 비활성화
        Riple.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //입력을 감지하고 총 발사하거나 재장전
        //입력을 감지하고 총을 발사하거나 재장전
        if (playerInput.fire)
        {
            // 발사 입력 감지 시 총 발사
            if(Riple.Fire())
            {
                // 발사 성공 시에만 발사 애니메이션 재생
                playerAnimator.SetTrigger("Fire");
            }
        }
        else if (playerInput.reload)
        {
            // 재장전 입력 감지 시 재장전
            if (Riple.Reload())
            {
                // 재장전 성공 시에만 재장전 애니메이션 재생
                playerAnimator.SetTrigger("Reload");
            }
        }

        /* 유석 - 남은 탄알 UI 갱신하기 
         ex) yRiple.magAmmo, yRiple.ammoRemain
         */
    }

    // 애니메이터의 IK 갱신
    void OnAnimatorIK(int layerIndex)
    {
        // IK를 사용하여 왼손의 위치와 회전을 총의 오른쪽 손잡이에 맞춤
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);



        //// 총알 쏠 때 IK의 Weight 조절
        //if (playerInput.fire)
        //{
        //    if (Riple.Fire())
        //    {
        //        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.0f);
        //        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.0f);
        //    }
        //}

        //// 장전 시 쏠 때 IK의 Weight 조절
        //else if (playerInput.reload)
        //{
        //    if (Riple.Reload())
        //    {
        //        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.0f);
        //        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.0f);
        //    }
        //}
    }
}


