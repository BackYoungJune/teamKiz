using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yPlayerGrenade : MonoBehaviour
{
    public GameObject Grenade; // 사용할 수류탄
    public Transform leftHandMount;     // 수류탄의 오른쪽 손잡이, 오른손이 위치할 지점
    public Transform rightHandMount;    // 수류탄의 오른쪽 손잡이, 오른손이 위치할 지점

    yPlayerInput playerInput; // 플레이어의 입력
    public Animator playerAnimator; // 애니메이터 컴포넌트

    void Awake()
    {
        // 사용할 컴포넌트들을 가져오기
        playerInput = GetComponentInParent<yPlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // 수류탄 Rigidbody를 끈다
        Grenade.GetComponent<Rigidbody>().Sleep();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.fire2)
        {
            
            // 수류탄 생성
            GameObject instantGrenade = Instantiate(Grenade, rightHandMount.position, rightHandMount.rotation);
            Rigidbody GrenadeRigid = instantGrenade.GetComponent<Rigidbody>();
            // 수류탄 Rigidbody를 켠다
            GrenadeRigid.WakeUp();
            // 수류탄을 발사한다
            GrenadeRigid.AddForce(Vector3.forward * 10.0f + Vector3.up * 3.0f, ForceMode.Impulse);

            // 수류탄 던지는 애니메이션 실행
            playerAnimator.SetTrigger("Throw");
            //Destroy(instantGrenade.gameObject, 5f);
        }
    }

    // 애니메이터의 IK 갱신
    void OnAnimatorIK(int layerIndex)
    {
        // IK를 사용하여 왼손의 위치와 회전을 수류탄의 왼쪽 손잡이에 맞춤
        //playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);    // 가중치 1.0f(100%)
        //playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        //playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        //playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

        // IK를 사용하여 왼손의 위치와 회전을 수류탄의 왼쪽 손잡이에 맞춤
        //playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);    // 가중치 1.0f(100%)
        //playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        //playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        //playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    }
}
